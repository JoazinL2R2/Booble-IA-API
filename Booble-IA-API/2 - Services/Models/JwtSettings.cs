namespace Booble_IA_API._2___Services.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; } = 60;
        public int RefreshTokenExpirationDays { get; set; } = 7;
    }

    public class IdentityServerSettings
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public List<Client> Clients { get; set; } = new List<Client>();
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientName { get; set; }
        public List<string> AllowedGrantTypes { get; set; } = new List<string>();
        public List<string> RedirectUris { get; set; } = new List<string>();
        public List<string> PostLogoutRedirectUris { get; set; } = new List<string>();
        public List<string> AllowedScopes { get; set; } = new List<string>();
        public List<string> AllowedCorsOrigins { get; set; } = new List<string>();
        public bool RequireClientSecret { get; set; } = true;
        public bool RequirePkce { get; set; } = true;
        public bool AllowOfflineAccess { get; set; } = true;
    }
}
