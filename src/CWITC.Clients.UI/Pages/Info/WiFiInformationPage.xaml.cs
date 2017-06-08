using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CWITC.Clients.Portable;

namespace CWITC.Clients.UI
{
    public partial class WiFiInformationPage : ContentPage
    {
        ConferenceInfoViewModel vm;
        public WiFiInformationPage()
        {
            InitializeComponent();
            BindingContext = vm = new ConferenceInfoViewModel();
        }

        protected override async void OnAppearing ()
        {
            base.OnAppearing ();

            await vm.UpdateConfigs ();
        }
    }
}

