using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;
using Domain;

namespace OAuth.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile()
          };

        public static IEnumerable<ApiScope> GetApiScopes() =>
           new List<ApiScope> {
               new ApiScope("libraryApi")
           };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
               new ApiResource("library_api", "Library API")
               {
                   Scopes = { "libraryApi" }
               }
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "web_app",
                    ClientSecrets = new [] { new Secret("client_secret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { StandardScopes.OpenId, StandardScopes.Profile, "libraryApi" },
                    AllowedCorsOrigins = {"http://localhost:4200"},
                    RedirectUris = { "http://localhost:4200/signin-callback", "http://localhost:4200" },
                    PostLogoutRedirectUris = {"http://localhost:4200"}
               }
            };
    }
}
