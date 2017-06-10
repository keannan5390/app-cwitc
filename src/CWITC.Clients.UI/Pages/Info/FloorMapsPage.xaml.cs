using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CWITC.Clients.UI
{
    public partial class FloorMapsPage : ContentPage
    {
        public FloorMapsPage()
        {
            InitializeComponent();

            CarouselMaps.ItemsSource = new List<EvolveMap>
            {
                new EvolveMap
                {
                    Local = "campus_map.jpg",
                    Url = "http://www.mstc.edu/sites/default/files/styles/campus-map/public/StevensPointCampusMap_0.jpg?itok=UNMMfqBA",
                    Title = "Campus Maps"
                }
            };
            

            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
            {
                Title = "Campus Map";
                CarouselMaps.ItemSelected += (sender, args) =>
                {
                    var current = args.SelectedItem as EvolveMap;
                    if (current == null)
                        return;
                    Title = current.Title;
                };
            }
        }
    }
}
