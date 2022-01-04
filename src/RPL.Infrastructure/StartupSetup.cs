using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RPL.Core.Entities;
using RPL.Core.Settings.Identity;
using RPL.Core.Settings.Swagger;
using RPL.Infrastructure.Data;
using RPL.Infrastructure.Mappers;
using RPL.Infrastructure.Middlewares;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace RPL.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddIdentityDbContext(this IServiceCollection services, string identityDatabaseConnectionString) =>
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseMySql(identityDatabaseConnectionString, ServerVersion.AutoDetect(identityDatabaseConnectionString))); // will be created in web project root

        public static void AddMainDbContext(this IServiceCollection services, string mainDatabaseConnectionString) =>
            services.AddDbContext<MainDbContext>(options =>
                options.UseMySql(mainDatabaseConnectionString, ServerVersion.AutoDetect(mainDatabaseConnectionString))); // will be created in web project root


        public static void AddIdentitySystemConfigurations(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            new IdentityBuilder(typeof(ApplicationUser), typeof(IdentityRole), services)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddIdentityServerConfigurations(this IServiceCollection services, X509Certificate2 certificate, string identityDatabaseConnectionString)
        {
            services.AddIdentityServer()
                    .AddSigningCredential(certificate)
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder =>
                            builder.UseMySql(identityDatabaseConnectionString, ServerVersion.AutoDetect(identityDatabaseConnectionString),
                                sql => sql.MigrationsAssembly(typeof(StartupSetup).GetTypeInfo().Assembly.GetName().Name));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder =>
                            builder.UseMySql(identityDatabaseConnectionString, ServerVersion.AutoDetect(identityDatabaseConnectionString),
                                sql => sql.MigrationsAssembly(typeof(StartupSetup).GetTypeInfo().Assembly.GetName().Name));

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                        options.TokenCleanupInterval = 3600; // interval in seconds (default is 3600)

                    })
                    .AddAspNetIdentity<ApplicationUser>();
        }

        public static void AddIdentityAuthenticationConfigurations(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = settings.IdentityServerUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiName = settings.Scope;
                        options.ApiSecret = settings.ClientSecret;
                    });
        }

        public static void AddIdentityGlobalAuthorizationConfigurations(this IServiceCollection services, params string[] scopes)
        {
            services.AddMvcCore(options =>
            {
                var policy = ScopePolicy.Create(scopes);
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddAuthorization();
        }

        public static void AddAutoMapperConfigurations(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(AutomapperMaps));

        public static void AddSwaggerConfigurations(this IServiceCollection services,
            SwaggerSettings settings) =>
            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(settings.Name, new OpenApiInfo
                {
                    Version = settings.Version,
                    Title = settings.Title,
                    Description = settings.Description,
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Shayne Boyer",
                    //    Email = string.Empty,
                    //    Url = new Uri("https://twitter.com/spboyer"),
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Use under LICX",
                    //    Url = new Uri("https://example.com/license"),
                    //}
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                c.EnableAnnotations();

                // Set the comments path for the Swagger JSON and UI.
                c.IncludeXmlComments(settings.XmlPath);
            });

        public static void AddLocalizationConfigurations(this IServiceCollection services) =>
            services
            .AddLocalization(options =>
            {
                options.ResourcesPath = "";
            })
            .Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("mm"),
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

        public static void ConfigureGlobalExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
