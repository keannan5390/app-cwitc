﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using CWITC.Clients.Portable;

namespace CWITC.Clients.UI
{
    public class EvolveMap
    {
        public string Local { get; set; }
        //public string Url { get; set; }
        public string Title { get; set; }
    }

    public partial class FloorMap : ContentView
    {
        public FloorMap ()
        {
            InitializeComponent ();

            BindingContextChanged += FloorMap_BindingContextChanged;

            MainImage.PropertyChanged += (sender, e) => 
            {
                if (e.PropertyName != nameof (MainImage.IsLoading))
                    return;
                ProgressBar.IsRunning = MainImage.IsLoading;
                ProgressBar.IsVisible = MainImage.IsLoading;
            };
        }

        private void FloorMap_BindingContextChanged(object sender, EventArgs e)
        {
            var data = BindingContext as EvolveMap;

            try
            {
                MainImage.Source = ImageSource.FromFile(data.Local);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert image to URI: " + ex);
                DependencyService.Get<IToast>().SendToast("Unable to load image.");
            }
        }
    }
}

