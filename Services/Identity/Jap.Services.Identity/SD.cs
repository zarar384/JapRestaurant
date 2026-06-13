using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Jap.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        //resources
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            //new example
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            //name,surname atc
            new IdentityResources.Profile(),
        };

        //identify resources. Where the client wants to access
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("jap-client", "Jap Server"),
                new ApiScope(name:"read", displayName:"Read your data."),
                new ApiScope(name:"write", displayName:"Write your data."),
                new ApiScope(name:"delete", displayName:"Delete your data.")
            };

        //create client. Client requests a token from the identity server to authenticate a user or access some resource.
        //Client - Jap.Web
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    //user data collection
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"read","write","profile"}
                },
                //new client
                new Client
                {
                    ClientId = "jap-client",
                    ClientSecrets= { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    //url main app. signin-oidc - needed for OpenId Connect
                    RedirectUris={ "https://localhost:7171/signin-oidc", "https://localhost:44356/signin-oidc" },
                    //where to pass the implementation of the code after successfully logging out of the account
                    PostLogoutRedirectUris={ "https://localhost:7171/signout-callback-oidc", "https://localhost:44356/signout-callback-oidc" },
                    //scope permissions
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "jap-client"
                    }
                },
            };
    }
}
