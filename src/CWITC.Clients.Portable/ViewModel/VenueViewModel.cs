using System;

using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Plugin.ExternalMaps;
using Plugin.Messaging;
using FormsToolkit;
using Plugin.ExternalMaps.Abstractions;

namespace CWITC.Clients.Portable
{
    public class VenueViewModel : ViewModelBase
    {
        public bool CanMakePhoneCall => CrossMessaging.Current.PhoneDialer.CanMakePhoneCall;
        public string EventTitle => "CWITC";
        public string LocationTitle => "Mid-State Technical College";
        public string Address1 => "1001 Center Piont Dr";
        public string Address2 => "Stevens Point, WI 54481";
        public double Latitude => 44.524823;
        public double Longitude => -89.585239;

        ICommand  navigateCommand;
        public ICommand NavigateCommand =>
            navigateCommand ?? (navigateCommand = new Command(async () => await ExecuteNavigateCommandAsync())); 

        async Task ExecuteNavigateCommandAsync()
        {
            Logger.Track(EvolveLoggerKeys.NavigateToCWITC);
            if(!await CrossExternalMaps.Current.NavigateTo(LocationTitle, Latitude, Longitude, NavigationType.Default))
            {
                MessagingService.Current.SendMessage(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title = "Unable to Navigate",
                        Message = "Please ensure that you have a map application installed.",
                        Cancel = "OK"
                    });
            }
        }

        ICommand  callCommand;
        public ICommand CallCommand =>
            callCommand ?? (callCommand = new Command(ExecuteCallCommand)); 

        void ExecuteCallCommand()
        {
            Logger.Track(EvolveLoggerKeys.CallVenue);
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall) 
                phoneCallTask.MakePhoneCall("17153443063");
        }
    }
}


