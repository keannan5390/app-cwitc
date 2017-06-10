using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content;
using Auth0.OidcClient;
using CWITC.Clients.Portable;
using FormsToolkit;
using IdentityModel.OidcClient;
using Plugin.CurrentActivity;

namespace CWITC.Droid
{
	public class AuthSSOClient : ISSOClient
	{
		public async Task<AccountResponse> LoginAsync()
		{
			var client = new Auth0Client(new Auth0ClientOptions
			{
				Domain = "cwitc.auth0.com",
				ClientId = "r2xGTXLZeEgCmqYIgLOaRJwD1sDoySzh",
                Activity = CrossCurrentActivity.Current.Activity
			});

            AuthorizeState authorizeState = await client.PrepareLoginAsync();

			var uri = global::Android.Net.Uri.Parse(authorizeState.StartUrl);
			var intent = new Intent(Intent.ActionView, uri);
			intent.AddFlags(ActivityFlags.NoHistory);
            CrossCurrentActivity.Current.Activity.StartActivity(intent);

            TaskCompletionSource<AccountResponse> tcs = new TaskCompletionSource<AccountResponse>();

            MessagingService.Current.Subscribe(MessageKeys.LoginCallback, async (IMessagingService arg1, string dataString) =>
            {
                var loginResult = await client.ProcessResponseAsync(intent.DataString, authorizeState);

                if (loginResult.IsError)
                {
                    Debug.WriteLine($"An error occurred during login: {loginResult.Error}");

                    tcs.SetResult(new AccountResponse { Error = loginResult.Error, Success = false });
                }

                string token = loginResult.IdentityToken;
                string accesstoken = loginResult.AccessToken;
                string name = loginResult.User.FindFirst(c => c.Type == "name")?.Value;
                string email = loginResult.User.FindFirst(c => c.Type == "email")?.Value;


                tcs.SetResult(new AccountResponse
                {
                    Success = true,
                    Token = token,
                    User = new User
                    {
                        FirstName = name.Split(' ')[0],
                        LastName = name.Split(' ')[1],
                        Email = email
                    }
                });
            });

            return await tcs.Task;
		}

		public Task LogoutAsync()
		{
			throw new NotImplementedException();
		}
	}
}
