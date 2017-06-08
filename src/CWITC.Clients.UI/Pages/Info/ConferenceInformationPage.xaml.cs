using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CWITC.Clients.Portable;

namespace CWITC.Clients.UI
{
    public partial class ConferenceInformationPage : ContentPage
    {
        ConferenceInfoViewModel vm; 
        public ConferenceInformationPage()
        {
            InitializeComponent();
            BindingContext = vm = new ConferenceInfoViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CodeOfConductText.Text = CodeOfConductPage.Conduct;
            await vm.UpdateConfigs();
        }
    }
}

