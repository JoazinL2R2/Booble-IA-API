using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Booble_IA_API._2___Services.Models
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("booble_api", "Booble API")
                {
                    UserClaims = { "name", "email", "sub" }
                },
                new ApiScope("booble_api.read", "Booble API Read Access"),
                new ApiScope("booble_api.write", "Booble API Write Access")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("booble_api", "Booble API")
                {
                    Scopes = { "booble_api", "booble_api.read", "booble_api.write" },
                    UserClaims = { "name", "email", "sub", "role" }
                }
            };

        public static IEnumerable<Duende.IdentityServer.Models.Client> Clients =>
            new Duende.IdentityServer.Models.Client[]
            {
                // Web Application Client (Authorization Code + PKCE)
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = "booble_web",
                    ClientName = "Booble Web Application",
                    ClientSecrets = { new Secret("booble_web_secret".Sha256()) },
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false, // Public client for SPA
                    
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "booble_api"
                    },
                    
                    RedirectUris = 
                    {
                        "http://localhost:3000/callback",
                        "https://localhost:3000/callback",
                        "http://localhost:3000/signin-oidc"
                    },
                    
                    PostLogoutRedirectUris = 
                    {
                        "http://localhost:3000",
                        "https://localhost:3000"
                    },
                    
                    AllowedCorsOrigins = 
                    {
                        "http://localhost:3000",
                        "https://localhost:3000"
                    },
                    
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600, // 1 hour
                    RefreshTokenUsage = TokenUsage.ReUse
                },
                
                // React Native Mobile App Client (Authorization Code + PKCE)
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = "booble_mobile",
                    ClientName = "Booble Mobile App",
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false, // Public client for mobile
                    
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "booble_api"
                    },
                    
                    RedirectUris = 
                    {
                        "com.booble.app://callback",
                        "booble://callback",
                        "exp://192.168.1.100:19000/--/callback", // Expo development
                        "exp://localhost:19000/--/callback"
                    },
                    
                    PostLogoutRedirectUris = 
                    {
                        "com.booble.app://logout",
                        "booble://logout"
                    },
                    
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600, // 1 hour
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 604800 // 7 days
                },
                
                // API Client for server-to-server communication
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = "booble_api_client",
                    ClientName = "Booble API Client",
                    ClientSecrets = { new Secret("booble_api_secret".Sha256()) },
                    
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    AllowedScopes = 
                    {
                        "booble_api",
                        "booble_api.read", 
                        "booble_api.write"
                    }
                },
                
                // Legacy JWT Client for backward compatibility
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = "booble_legacy",
                    ClientName = "Booble Legacy Client",
                    ClientSecrets = { new Secret("booble_legacy_secret".Sha256()) },
                    
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "booble_api"
                    },
                    
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600
                }
            };
    }
}