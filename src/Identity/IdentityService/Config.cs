using Duende.IdentityServer.Models;

namespace IdentityService
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(name: "bookstore", displayName: "Bookstore API", new []
                {
                    "role"
                })
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "swagger",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "bookstore" },
                    Claims = new List<ClientClaim>()
                    {
                        new ClientClaim("role", "reader")
                    }
                }
            };
    }
}
