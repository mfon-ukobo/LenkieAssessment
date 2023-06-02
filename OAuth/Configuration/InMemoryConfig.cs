using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace OAuth.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResource("roles", "User role(s)", new List<string> { "role" })
          };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "web_app",
                    ClientSecrets = new [] { new Secret("lenkie_secret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "library_api" }
               }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
           new List<ApiScope> { 
               new ApiScope("library_api", "Library API Scope")
           };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("library_api", "Library API")
            };
    }
}
