﻿using System;
using System.Collections.Generic;
using CWITC.Clients.Portable;
using CWITC.DataObjects;
using Xamarin.Forms;

namespace CWITC.Clients.UI
{
    public partial class LunchLocationsPage : ContentPage
    {
		LunchLocationsViewModel vm;
		LunchLocationsViewModel ViewModel => vm ?? (vm = BindingContext as LunchLocationsViewModel);

		public LunchLocationsPage()
        {
            InitializeComponent();

            BindingContext = new LunchLocationsViewModel(this.Navigation);

			ListViewLocations.ItemTapped += (sender, e) => ListViewLocations.SelectedItem = null;
			ListViewLocations.ItemSelected += async (sender, e) =>
			{
				var ev = ListViewLocations.SelectedItem as LunchLocation;
				if (ev == null)
					return;

                var eventDetails = new LunchLocationDetailsPage();

				eventDetails.Location = ev;
                App.Logger.TrackPage(AppPage.LunchLocation.ToString(), ev.Name);
				await NavigationService.PushAsync(Navigation, eventDetails);

				ListViewLocations.SelectedItem = null;
			};
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

                if (ViewModel.Locations.Count == 0)
                ViewModel.LoadLocationsCommand.Execute(false);
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
    }
}