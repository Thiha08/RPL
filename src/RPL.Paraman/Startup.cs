using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RPL.Core;
using RPL.Core.Settings.Identity;
using RPL.Core.Settings.Swagger;
using RPL.Infrastructure;
using RPL.Infrastructure.Data;
using System;
using System.IO;
using System.Reflection;

namespace RPL.Paraman
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string identityDatabaseConnectionString = Configuration.GetConnectionString("IdentityDatabaseConnection");
            string mainDatabaseConnectionString = Configuration.GetConnectionString("MainDatabaseConnectionString");

            var identitySettings = new IdentitySettings();
            var identitySettingsSection = Configuration.GetSection("IdentitySettings");
            identitySettingsSection.Bind(identitySettings);
            services.Configure<IdentitySettings>(identitySettingsSection);

            var swaggerSettings = new SwaggerSettings();
            Configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Set the comments path for the Swagger JSON and UI.
            swaggerSettings.XmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddIdentityDbContext(identityDatabaseConnectionString);
            services.AddMainDbContext(mainDatabaseConnectionString);

            services.AddIdentitySystemConfigurations();
            services.AddIdentityAuthenticationConfigurations(identitySettings);
            services.AddIdentityGlobalAuthorizationConfigurations(identitySettings.Scope);
            services.AddAutoMapperConfigurations();
            services.AddSwaggerConfigurations(swaggerSettings);
            services.AddLocalizationConfigurations();
            services.AddMvc().AddNewtonsoftJson();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultCoreModule());
            builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == "Development"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.EnsureMigrationOfContext<MainDbContext>();
            }

            app.ConfigureGlobalExceptionMiddleware();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paraman APIs");
                c.RoutePrefix = String.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
