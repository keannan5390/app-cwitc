using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Auth0.OidcClient;
using CoreGraphics;
using CWITC.Clients.Portable;
using FormsToolkit;
using Foundation;
using SafariServices;
using UIKit;

namespace CWITC.iOS
{
    public class iOSAuthSSOClient : ISSOClient
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
            var redirect = "org.cenwidev.cwitc%3A%2F%2Fcwitc.auth0.com/ios/org.cenwidev.cwitc/logout";
            string logoutUri = $"https://cwitc.auth0.com/v2/logout?returnTo={redirect}";
            var controller = new SFSafariViewController(new NSUrl(logoutUri));

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            var topVC = GetViewController();
            MessagingService.Current.Subscribe(MessageKeys.LogoutCallback, (IMessagingService arg1) =>
            {
                topVC.DismissViewController(true, () => { });

                tcs.SetResult(true);

            });

            topVC.PresentViewController(controller, true, () => { });

            return tcs.Task;
        }

        private UIViewController GetViewController()
        {
            var vc = TrackCurrentViewControllerRenderer.CurrentViewController;

            return vc;
        }
    }
}
