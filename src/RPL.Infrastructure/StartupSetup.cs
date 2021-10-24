using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPL.Core.ProjectAggregate.Entities;
using RPL.Infrastructure.Data;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace RPL.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddIdentityDbContext(this IServiceCollection services, string identityDatabaseConnectionString) =>
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(identityDatabaseConnectionString)); // will be created in web project root

        public static void AddMainDbContext(this IServiceCollection services, string mainDatabaseConnectionString) =>
            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(mainDatabaseConnectionString)); // will be created in web project root


        public static void AddIdentity(this IServiceCollection services) =>
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();


        public static void AddIdentityServer(this IServiceCollection services, X509Certificate2 certificate, string identityDatabaseConnectionString) =>
            services.AddIdentityServer()
                    .AddSigningCredential(certificate)
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder =>
                            builder.UseSqlServer(identityDatabaseConnectionString, 
                                sql => sql.MigrationsAssembly(typeof(StartupSetup).GetTypeInfo().Assembly.GetName().Name));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder =>
                            builder.UseSqlServer(identityDatabaseConnectionString,
                                sql => sql.MigrationsAssembly(typeof(StartupSetup).GetTypeInfo().Assembly.GetName().Name));

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                        options.TokenCleanupInterval = 3600; // interval in seconds (default is 3600)

                    })
                    .AddAspNetIdentity<ApplicationUser>();
    }
}
