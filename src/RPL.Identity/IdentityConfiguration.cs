using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace RPL.Identity
{
    public class IdentityConfiguration
    {
        public IConfiguration Configuration { get; }

        public IdentityConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };


        public IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(Configuration["RyawgenAppIdentitySettings:Scope"]),
                new ApiScope(Configuration["ParamanAppIdentitySettings:Scope"]),
                new ApiScope(Configuration["LarbanAppIdentitySettings:Scope"])
            };

        public IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource(Configuration["RyawgenAppIdentitySettings:Scope"], Configuration["RyawgenAppIdentitySettings:Name"])
                {
                    Scopes = new List<string>{ Configuration["RyawgenAppIdentitySettings:Scope"] },
                    ApiSecrets = { new Secret(Configuration["RyawgenAppIdentitySettings:ClientSecret"].Sha256()) }
                },

                new ApiResource(Configuration["ParamanAppIdentitySettings:Scope"], Configuration["ParamanAppIdentitySettings:Name"])
                {
                    Scopes = new List<string>{ Configuration["ParamanAppIdentitySettings:Scope"] },
                    ApiSecrets = { new Secret(Configuration["ParamanAppIdentitySettings:ClientSecret"].Sha256()) }
                },

                new ApiResource(Configuration["LarbanAppIdentitySettings:Scope"], Configuration["LarbanAppIdentitySettings:Name"])
                {
                    Scopes = new List<string>{ Configuration["LarbanAppIdentitySettings:Scope"] },
                    ApiSecrets = { new Secret(Configuration["LarbanAppIdentitySettings:ClientSecret"].Sha256()) }
                }
            };

        public IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = Configuration["RyawgenAppIdentitySettings:ClientId"],
                    ClientName = Configuration["RyawgenAppIdentitySettings:Name"],
                    ClientSecrets = { new Secret(Configuration["RyawgenAppIdentitySettings:ClientSecret"].Sha256()) },
                    
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 259200, // Three Days
                    SlidingRefreshTokenLifetime = 2592000, // One Month
                    AbsoluteRefreshTokenLifetime = 31536000, // One Year
                    AllowOfflineAccess = true, // For refresh token.
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,

                    AllowedScopes = 
                    { 
                        Configuration["RyawgenAppIdentitySettings:Scope"],
                        "offline_access"
                    },
                },

                new Client
                {
                    ClientId = Configuration["ParamanAppIdentitySettings:ClientId"],
                    ClientName = Configuration["ParamanAppIdentitySettings:Name"],
                    ClientSecrets = { new Secret(Configuration["ParamanAppIdentitySettings:ClientSecret"].Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 259200, // Three Days
                    SlidingRefreshTokenLifetime = 2592000, // One Month
                    AbsoluteRefreshTokenLifetime = 31536000, // One Year
                    AllowOfflineAccess = true, // For refresh token.
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,

                    AllowedScopes = 
                    { 
                        Configuration["ParamanAppIdentitySettings:Scope"],
                        "offline_access"
                    },
                },

                new Client
                {
                    ClientId = Configuration["LarbanAppIdentitySettings:ClientId"],
                    ClientName = Configuration["LarbanAppIdentitySettings:Name"],
                    ClientSecrets = { new Secret(Configuration["LarbanAppIdentitySettings:ClientSecret"].Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenLifetime = 3600, // default is 60 minutes
                    SlidingRefreshTokenLifetime = 43200, // 12 hours
                    AbsoluteRefreshTokenLifetime = 86400, // 1 day
                    AllowOfflineAccess = true, // For refresh token.
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    
                    AccessTokenType = AccessTokenType.Reference,
                    RequireConsent = false,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        { $"{Configuration["LarbanAppIdentitySettings:DomainName"]}" },
                        { $"{Configuration["LarbanAppIdentitySettings:DomainName"]}/silent-renew.html" }
                    },

                    PostLogoutRedirectUris =
                    {
                        { $"{Configuration["LarbanAppIdentitySettings:DomainName"]}/unauthorized" },
                        { $"{Configuration["LarbanAppIdentitySettings:DomainName"]}" },
                    },

                    AllowedCorsOrigins =
                    {
                        { $"{Configuration["LarbanAppIdentitySettings:DomainName"]}" },
                    },

                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        StandardScopes.OfflineAccess,
                        "role",
                        Configuration["LarbanAppIdentitySettings:Scope"]
                    },
                },
            };
    }
}
