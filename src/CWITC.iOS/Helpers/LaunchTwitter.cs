using System;
using CWITC.Clients.Portable;
using Foundation;
using UIKit;
using Xamarin.Forms;
using CWITC.iOS;

[assembly:Dependency(typeof(LaunchTwitter))]
namespace CWITC.iOS
{
    public class LaunchTwitter : ILaunchTwitter
    {
        #region ILaunchTwitter implementation

        public bool OpenUserName(string username)
        {
            try
            {
                if(UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"twitter://user?screen_name={username}")))
                    return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url" + ex);
            }

            try
            {
                if(UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"tweetbot://{username}/timeline")))
                    return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url " + ex);
            }
            return false;
        }

        public bool OpenStatus(string statusId)
        {
            
            try
            {
                if(UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"twitter://status?id={statusId}")))
                    return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url " + ex);
            }

            try
            {
                if(UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"tweetbot:///status/{statusId}")))
                    return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url " + ex);
            }
            return false;
        }

        #endregion


    }
}

