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
               new ApiScope(Scopes.ReadBooks, "Read Books"),
               new ApiScope(Scopes.WriteBooks, "Create, Update & Delete Books"),
               new ApiScope(Scopes.ReadUsers, "Read Users"),
               new ApiScope(Scopes.WriteUsers, "Create, Update & Delete Users"),
           };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
               new ApiResource("library_api", "Library API")
               {
                   Scopes = { Scopes.ReadBooks, Scopes.WriteBooks, Scopes.ReadUsers, Scopes.WriteUsers }
               }
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "web_app",
                    ClientSecrets = new [] { new Secret("client_secret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { StandardScopes.OpenId, Scopes.ReadBooks, Scopes.WriteBooks, Scopes.ReadUsers, Scopes.WriteUsers },
               }
            };
    }
}
