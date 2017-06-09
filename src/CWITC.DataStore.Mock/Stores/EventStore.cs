using System;
using CWITC.DataObjects;
using CWITC.DataStore.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace CWITC.DataStore.Mock
{
    public class EventStore : BaseStore<FeaturedEvent>, IEventStore
    {
        List<FeaturedEvent> Events { get; }
        ISponsorStore sponsors;
        public EventStore()
        {
            Events = new List<FeaturedEvent>();
            sponsors = DependencyService.Get<ISponsorStore>();
        }

        public override async Task InitializeStore()
        {
            if (Events.Count != 0)
                return;

            var sponsorList = await sponsors.GetItemsAsync();


                Events.Add(new FeaturedEvent
                {
                    Title = "Social Event",
                    Description = "A social event sponsored by CREATE Portage County.",
                    StartTime = new DateTime(2017, 9, 29, 23, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 30, 2, 59, 0, DateTimeKind.Utc),
                    LocationName = "TBD",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Registration and Breakfast",
                    Description = "Get ready for CWITC. Check in, get your badge, visit the boots and get some bagels & coffee!",
                    StartTime = new DateTime(2017, 9, 30, 11, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 30, 12, 50, 0, DateTimeKind.Utc),
                    LocationName = "Lobby",
                    IsAllDay = false,
                });
            
            Events.Add(new FeaturedEvent
                {
                    Title = "Keynote",
                    Description = "",
                    StartTime = new DateTime(2017, 9, 30, 13, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 30, 13, 50, 0, DateTimeKind.Utc),
                    LocationName = "General Session",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Session Breakout 1",
                    Description = "",
                    StartTime = new DateTime(2017, 9, 30, 14, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 30, 14, 50, 0, DateTimeKind.Utc),
                    LocationName = "Session Rooms",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Session Breakout 2",
                    Description = "",
                    StartTime = new DateTime(2017, 9, 30, 15, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 20, 15, 50, 0, DateTimeKind.Utc),
                    LocationName = "Session Rooms",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Lunch",
                    Description = "Visit one of our partnering local restaurants for lunch in downtown Stevens Point.",
                    StartTime = new DateTime(2017, 9, 30, 16, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 9, 30, 17, 20, 0, DateTimeKind.Utc),
                    LocationName = "Downtown Stevens Point",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Session Breakout 3",
                    Description = "",
                    StartTime = new DateTime(2017, 9, 30, 17, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 30, 18, 20, 0, DateTimeKind.Utc),
                    LocationName = "Session Rooms",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Session Breakout 4",
                    Description = "",
                    StartTime = new DateTime(2017, 9, 30, 18, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 30, 19, 20, 0, DateTimeKind.Utc),
                    LocationName = "Session Rooms",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Closing Ceremony",
                    Description = "",
                    StartTime = new DateTime(2017, 9, 30, 19, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2017, 9, 30, 20, 00, 0, DateTimeKind.Utc),
                    LocationName = "General Session",
                    IsAllDay = false,
                });
        }

        public override async Task<IEnumerable<FeaturedEvent>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject (Events);
            return Events;
        }
    }
}

