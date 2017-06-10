using CWITC.DataStore.Abstractions;
using CWITC.DataObjects;
using CWITC.DataStore.Mock;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CWITC.DataStore.Mock
{
    public class SponsorStore : BaseStore<Sponsor>, ISponsorStore
    {
        List<Sponsor> Sponsors;
        readonly static string[] Companies =
            {
                "CENWI DEV",
                "CWITA",
                "WDB",
                "Mid-State Technical College"
            };

        readonly static int[] Levels =
            {
                5,
                5,
                5,
                1
            };

        readonly static string[] Logos =
            {
            "https://cenwidev.org/img/cenwidev.png",
            "http://cwita.org/wp-content/uploads/2016/01/cropped-cwita-app-logo-270x270.png",
            "http://ncwwdb.org/wp-content/uploads/2014/06/NCWWDB-Logo.png",
            "https://pbs.twimg.com/profile_images/509700737450786816/acCjjGEg.jpeg"
                 };

        readonly static string[] Descriptions =
        {
            "",
            "",
            "",
            ""
         };

        readonly static string[] Handles = {
            "cenwidev",
            null,
            null,
             "FollowMSTC"
        };

        readonly static string[] Websites = {
            "http://cenwidev.org",
            "http://cwita.org",
			"http://ncwwdb.org",
            "https://www.mstc.edu",
        };


        public override async Task<Sponsor> GetItemAsync(string id)
        {
            await InitializeStore();
            return Sponsors.FirstOrDefault(s => s.Id == id);
        }

        public override async Task<IEnumerable<Sponsor>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Sponsors);
            return Sponsors as IEnumerable<Sponsor>;
        }

        bool initialized;
        public override Task InitializeStore()
        {
            if (initialized)
                return Task.FromResult(true);

            initialized = true;

            Sponsors = new List<Sponsor>();
            for (int i = 0; i < Companies.Length; i++)
            {
                Sponsors.Add(new Sponsor
                {
                    Name = Companies[i],
                    ImageUrl = Logos[i],
                    Description = Descriptions[i],
                    WebsiteUrl = Websites[i],
                    TwitterUrl = Handles[i],
                    SponsorLevel = GetLevel(Levels[i])
                });
            }

            return Task.FromResult(true);

        }
        List<SponsorLevel> sponsorLevels;
        SponsorLevel GetLevel(int level)
        {
            if (sponsorLevels == null)
            {
                sponsorLevels = new List<SponsorLevel> {
                    new SponsorLevel { Name = "Platinum", Rank = 0 },
                    new SponsorLevel { Name = "Gold", Rank = 1 },
                    new SponsorLevel{ Name = "Silver", Rank = 2 },
                    new SponsorLevel{ Name = "Bronze", Rank = 3 },
                    new SponsorLevel { Name = "Exhibitor", Rank = 4 },
                    new SponsorLevel { Name = "Organizing", Rank = 5 }
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(sponsorLevels);
            }



            return sponsorLevels[level];
        }
    }
}