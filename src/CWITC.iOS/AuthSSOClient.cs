using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Auth0.OidcClient;
using CWITC.Clients.Portable;
using UIKit;

namespace CWITC.iOS
{
    public class AuthSSOClient : ISSOClient
    {
        public async Task<AccountResponse> LoginAsync()
        {
            var client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "cwitc.auth0.com",
                ClientId = "r2xGTXLZeEgCmqYIgLOaRJwD1sDoySzh",
                Controller = GetViewController()
            });

            var loginResult = await client.LoginAsync();

            if (loginResult.IsError)
            {
                Debug.WriteLine($"An error occurred during login: {loginResult.Error}");

                return new AccountResponse { Error = loginResult.Error, Success = false };
            }

            string token = loginResult.IdentityToken;
            string accesstoken = loginResult.AccessToken;
            string name = loginResult.User.FindFirst(c => c.Type == "name")?.Value;
            string email = loginResult.User.FindFirst(c => c.Type == "email")?.Value;

            string[] nameParts = name?.Split(' ');

            return new AccountResponse
            {
                Success = true,
                Token = token,
                User = new User
                {
                    FirstName = nameParts[0],
                    LastName = nameParts[1],
                    Email = email
                }
            };
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        private UIViewController GetViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            return vc;
            //
        }
    }
}
