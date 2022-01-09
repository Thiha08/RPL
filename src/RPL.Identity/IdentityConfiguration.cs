using IdentityModel;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };


        public IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(Configuration["RyawgenAppIdentitySettings:Scope"]),
                new ApiScope(Configuration["ParamanAppIdentitySettings:Scope"]),
                new ApiScope(Configuration["LarbanAppIdentitySettings:Scope"])
            };

        public IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource(Configuration["RyawgenAppIdentitySettings:Scope"])
                {
                    Scopes = new List<string> 
                    { 
                        Configuration["RyawgenAppIdentitySettings:Scope"] 
                    },
                    
                    ApiSecrets = new List<Secret>
                    { 
                        new Secret(Configuration["RyawgenAppIdentitySettings:ClientSecret"].Sha256()) 
                    },

                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Name
                    }
                },
                
                new ApiResource(Configuration["ParamanAppIdentitySettings:Scope"])
                {
                    Scopes = new List<string> 
                    { 
                        Configuration["ParamanAppIdentitySettings:Scope"] 
                    },

                    ApiSecrets = new List<Secret>
                    { 
                        new Secret(Configuration["ParamanAppIdentitySettings:ClientSecret"].Sha256()) 
                    },

                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Name
                    }
                },
                
                new ApiResource(Configuration["LarbanAppIdentitySettings:Scope"])
                {
                    Scopes = new List<string> 
                    { 
                        Configuration["LarbanAppIdentitySettings:Scope"] 
                    },

                    ApiSecrets = new List<Secret>
                    { 
                        new Secret(Configuration["LarbanAppIdentitySettings:ClientSecret"].Sha256()) 
                    },

                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Name
                    }
                },
            };

        public IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = Configuration["RyawgenAppIdentitySettings:ClientId"],

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(Configuration["RyawgenAppIdentitySettings:ClientSecret"].Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { Configuration["RyawgenAppIdentitySettings:Scope"] },
                    AccessTokenLifetime = 259200, // Three Days
                    SlidingRefreshTokenLifetime = 2592000, // One Month
                    AbsoluteRefreshTokenLifetime = 31536000, // One Year
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly
                },

                new Client
                {
                    ClientId = Configuration["ParamanAppIdentitySettings:ClientId"],

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(Configuration["ParamanAppIdentitySettings:ClientSecret"].Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { Configuration["ParamanAppIdentitySettings:Scope"] },
                    AccessTokenLifetime = 259200, // Three Days
                    SlidingRefreshTokenLifetime = 2592000, // One Month
                    AbsoluteRefreshTokenLifetime = 31536000, // One Year
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly
                },

                new Client
                {
                    ClientId = Configuration["LarbanAppIdentitySettings:ClientId"],

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(Configuration["LarbanAppIdentitySettings:ClientSecret"].Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { Configuration["LarbanAppIdentitySettings:Scope"] },
                    AccessTokenLifetime = 259200, // Three Days
                    SlidingRefreshTokenLifetime = 2592000, // One Month
                    AbsoluteRefreshTokenLifetime = 31536000, // One Year
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly
                },
            };
    }
}
