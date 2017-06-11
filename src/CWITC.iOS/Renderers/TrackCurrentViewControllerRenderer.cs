using System;
using CWITC.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(ContentPage), typeof(TrackCurrentViewControllerRenderer))]
namespace CWITC.iOS
{
    public class TrackCurrentViewControllerRenderer : PageRenderer
    {
        public static UIViewController CurrentViewController { get; private set; }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            CurrentViewController = this.ViewController;
        }
    }
}
