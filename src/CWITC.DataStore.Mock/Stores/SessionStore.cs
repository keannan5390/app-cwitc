using System;
using CWITC.DataStore.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using CWITC.DataObjects;

using CWITC.DataStore.Mock;
using System.Linq;
using Xamarin.Forms;

namespace CWITC.DataStore.Mock
{
    public class SessionStore : BaseStore<Session>, ISessionStore
    {

        List<Session> sessions;
        ISpeakerStore speakerStore;
        ICategoryStore categoryStore;
        IFavoriteStore favoriteStore;
        IFeedbackStore feedbackStore;
        public SessionStore()
        {
            speakerStore = DependencyService.Get<ISpeakerStore>();
            favoriteStore = DependencyService.Get<IFavoriteStore>();
            categoryStore = DependencyService.Get<ICategoryStore>();
            feedbackStore = DependencyService.Get<IFeedbackStore>();
        }

        #region ISessionStore implementation

        public async override Task<Session> GetItemAsync(string id)
        {
            if (!initialized)
                await InitializeStore();
            
            return sessions.FirstOrDefault(s => s.Id == id);
        }

        public async override Task<IEnumerable<Session>> GetItemsAsync(bool forceRefresh = false)
        {
            if (!initialized)
                await InitializeStore();
            
            return sessions as IEnumerable<Session>;
        }

        public async Task<IEnumerable<Session>> GetSpeakerSessionsAsync(string speakerId)
        {
            if (!initialized)
                await InitializeStore();
            
            var results =  from session in sessions
                           where session.StartTime.HasValue
                           orderby session.StartTime.Value
                           from speaker in session.Speakers
                           where speaker.Id == speakerId
                           select session;
            
            return results;
        }

        public async Task<IEnumerable<Session>> GetNextSessions()
        {
            if (!initialized)
                await InitializeStore();

            var date = DateTime.UtcNow.AddMinutes(-30);

            var results = (from session in sessions
                where (session.IsFavorite && session.StartTime.HasValue && session.StartTime.Value > date)
                                    orderby session.StartTime.Value
                                    select session).Take(2);


            var enumerable = results as Session[] ?? results.ToArray();
            return !enumerable.Any() ? null : enumerable;
        }

        #endregion

        #region IBaseStore implementation
        bool initialized = false;
        public async override Task InitializeStore()
        {
            if (initialized)
                return;
            
            initialized = true;
            var categories = (await categoryStore.GetItemsAsync()).ToArray();
            await speakerStore.InitializeStore();
            var speakers = (await speakerStore.GetItemsAsync().ConfigureAwait(false)).ToArray();
            sessions = new List<Session>();
            int speaker = 0;
            int speakerCount = 0;
            int room = 0;
            int category = 0;
            var day = new DateTime(2017, 9, 30, 14, 0, 0, DateTimeKind.Utc);
            int dayCount = 0;
            for (int i = 0; i < titles.Length; i++)
            {
                var sessionSpeakers = new List<Speaker>();
                speakerCount++;
                
                for (int j = 0; j < speakerCount; j++)
                {
                    sessionSpeakers.Add(speakers[speaker]);
                    speaker++;
                    if (speaker >= speakers.Length)
                        speaker = 0;
                }

                if (i == 1)
                    sessionSpeakers.Add(sessions[0].Speakers.ElementAt(0));

                var cat = categories[category];
                category++;
                if (category >= categories.Length)
                    category = 0;

                var ro = rooms[room];
                room++;
                if (room >= rooms.Length)
                    room = 0;

                sessions.Add(new Session
                    {
                        Id = i.ToString(),
                        Abstract = "This is an abstract that is going to tell us all about how awsome this session is and that you should go over there right now and get ready for awesome!.",
                        MainCategory = cat,
                        Room = ro,
                        Speakers = sessionSpeakers,
                        Title = titles[i],
                        ShortTitle = titlesShort[i],
                        RemoteId = i.ToString()
                    });
                
                sessions[i].IsFavorite = await favoriteStore.IsFavorite(sessions[i].Id);
                sessions[i].FeedbackLeft = await feedbackStore.LeftFeedback(sessions[i]);

                SetStartEnd(sessions[i], day);

                    dayCount++;
                    if (dayCount == 4)
                    {
                        day = day.AddHours(1);

	                    if(day.Hour == 16)
	                    {
	                        day = day.AddHours(1).AddMinutes(30);
	                    }

                        dayCount = 0;
                    }

                if (speakerCount > 2)
                    speakerCount = 0;
            }

            sessions[sessions.Count - 1].IsFavorite = await favoriteStore.IsFavorite(sessions[sessions.Count - 1].Id);
            sessions[sessions.Count - 1].FeedbackLeft = await feedbackStore.LeftFeedback(sessions[sessions.Count - 1]);
            sessions[sessions.Count - 1].StartTime = null;
            sessions[sessions.Count - 1].EndTime = null;
        }

        void SetStartEnd(Session session, DateTime day)
        {
            session.StartTime = day;
            session.EndTime = session.StartTime.Value.AddHours(1);
        }

        public Task<Session> GetAppIndexSession (string id)
        {
            return GetItemAsync (id);
        }

        Room [] rooms = new [] 
        {
                new Room {Name = "Room 410"},
                new Room {Name = "Room 415"},
                new Room {Name = "Room 420"},
                new Room {Name = "Room 425"}
        };


        

        string[] titles = new [] {
            "This is the Really Awesome Session #1",
            "This is the Really Awesome Session #2",
            "This is the Really Awesome Session #3",
            "This is the Really Awesome Session #4",
            "This is the Really Awesome Session #5",
            "This is the Really Awesome Session #6",
            "This is the Really Awesome Session #7",
            "This is the Really Awesome Session #8",
            "This is the Really Awesome Session #9",
            "This is the Really Awesome Session #10",
            "This is the Really Awesome Session #11",
            "This is the Really Awesome Session #12",
            "This is the Really Awesome Session #13",
            "This is the Really Awesome Session #14",
            "This is the Really Awesome Session #15",
            "This is the Really Awesome Session #16"
        };

        string[] titlesShort = new [] {
            "Awesome Session #1",
            "Awesome Session #2",
            "Awesome Session #3",
            "Awesome Session #4",
            "Awesome Session #5",
            "Awesome Session #6",
            "Awesome Session #7",
            "Awesome Session #8",
            "Awesome Session #9",
            "Awesome Session #10",
            "Awesome Session #11",
            "Awesome Session #12",
            "Awesome Session #13",
            "Awesome Session #14",
            "Awesome Session #15",
            "Awesome Session #16"
        };

        #endregion
    }
}

