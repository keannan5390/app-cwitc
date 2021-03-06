﻿using System;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using CWITC.Clients.Portable;
using FormsToolkit;
using CWITC.DataStore.Abstractions;

namespace CWITC.Clients.UI
{
    public class RootPageAndroid : MasterDetailPage
    {
        Dictionary<int, EvolveNavigationPage> pages;
        DeepLinkPage page;
        bool isRunning = false;
        public RootPageAndroid()
        {
            pages = new Dictionary<int, EvolveNavigationPage>();
            Master = new MenuPage(this);

            pages.Add(0, new EvolveNavigationPage(new FeedPage()));

            Detail = pages[0];
            MessagingService.Current.Subscribe<DeepLinkPage>("DeepLinkPage", async (m, p) =>
                {
                    page = p;

                    if(isRunning)
                        await GoToDeepLink();
                });
        }


        public async Task NavigateAsync(int menuId)
        {
            EvolveNavigationPage newPage = null;
            if (!pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {
                    case (int)AppPage.Feed: //Feed
                        pages.Add(menuId, new EvolveNavigationPage(new FeedPage()));
                        break;
                    case (int)AppPage.Sessions://sessions
                        pages.Add(menuId, new EvolveNavigationPage(new SessionsPage()));
                        break;
                    case (int)AppPage.Schedule://events
                        pages.Add(menuId, new EvolveNavigationPage(new EventsPage()));
                        break;
                    case (int)AppPage.Gallery://gallery
                        pages.Add(menuId, new EvolveNavigationPage(new GalleryPage()));
						break;
                    case (int)AppPage.Sponsors://sponsors
                        newPage = new EvolveNavigationPage(new SponsorsPage());
                        break;
                    case (int)AppPage.Venue: //venue
                        newPage = new EvolveNavigationPage(new VenuePage());
                        break;
                    case (int)AppPage.FloorMap://Floor Maps
                        newPage = new EvolveNavigationPage(new FloorMapsPage());
                        break;
                    case (int)AppPage.LunchLocations://Lunch!
                        newPage = new EvolveNavigationPage(new LunchLocationsPage());
						break;
                    case (int)AppPage.Settings://Settings
                        newPage = new EvolveNavigationPage(new SettingsPage());
                        break;
                }
            }

            if(newPage == null)
                newPage = pages[menuId];
            
            if(newPage == null)
                return;

            //if we are on the same tab and pressed it again.
            if (Detail == newPage)
            {
                await newPage.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Settings.Current.FirstRun)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }

            isRunning = true;

            await GoToDeepLink();

        }

        async Task GoToDeepLink()
        {
            if (page == null)
                return;
            var p = page.Page;
            var id = page.Id;
            page = null;
            switch(p)
            {
                case AppPage.Sessions:
                    await NavigateAsync((int)AppPage.Sessions);
                    break;
                case AppPage.Session:
                    await NavigateAsync((int)AppPage.Sessions);
                    if (string.IsNullOrWhiteSpace(id))
                        break;

                    var session = await DependencyService.Get<ISessionStore>().GetAppIndexSession(id);
                    if (session == null)
                        break;
                    await Detail.Navigation.PushAsync(new SessionDetailsPage(session));
                    break;
                case AppPage.Schedule:
                    await NavigateAsync((int)AppPage.Schedule);
                    break;
            }

        }

    }
}


