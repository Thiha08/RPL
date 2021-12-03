using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RPL.Infrastructure;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RPL.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string identityDatabaseConnectionString = Configuration.GetConnectionString("IdentityDatabaseConnection");
            // var certificate = new X509Certificate2(WebHostEnvironment.ContentRootPath + Configuration["AppSettings:CertificatePath"], Configuration["AppSettings:ExportPassword"]);

            // Looking for a Self-Signed Certificate for Identity Server and Azure 
            //
            // Reference: https://benjii.me/2017/06/creating-self-signed-certificate-identity-server-azure/
            // Reference: https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-view-certificates-with-the-mmc-snap-in
            // Reference: https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-retrieve-the-thumbprint-of-a-certificate
            X509Certificate2 certificate = null;
            //using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            //{
            //    certStore.Open(OpenFlags.ReadOnly);
            //    X509Certificate2Collection certCollection = certStore.Certificates.Find(
            //        X509FindType.FindByThumbprint,
            //        Configuration["AppSettings:CertificateThumbprint"],
            //        false);
            //    // Get the first cert with the thumbprint
            //    if (certCollection.Count > 0)
            //    {
            //        certificate = certCollection[0];
            //    }
            //}

            if (WebHostEnvironment.IsDevelopment())
            {
                certificate = new X509Certificate2(Path.Combine(WebHostEnvironment.ContentRootPath, Configuration["IdentitySettings:CertificatePath"]), Configuration["IdentitySettings:CertificatePassword"]);
            }
            else
            {
                certificate = new X509Certificate2(Configuration["IdentitySettings:CertificatePath"], Configuration["IdentitySettings:CertificatePassword"]);
            }

            services.AddIdentityDbContext(identityDatabaseConnectionString);
            services.AddIdentity();
            services.AddIdentityServer(certificate, identityDatabaseConnectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }
            //else
            //{
            //    app.UseHsts();
            //}

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("RPL Identity Server is running!");
                });
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            var identityConfiguration = new IdentityConfiguration(Configuration);

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (context.Clients.Any())
                {
                    foreach (var client in context.Clients)
                    {
                        context.Clients.Remove(client);
                    }
                    context.SaveChanges();
                }

                foreach (var client in identityConfiguration.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in identityConfiguration.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in identityConfiguration.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in identityConfiguration.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
