using System;
using CWITC.DataObjects;
using CWITC.DataStore.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace CWITC.DataStore.Mock
{
    public class NotificationStore  : BaseStore<Notification>, INotificationStore
    {
        public NotificationStore()
        {
        }

        public async Task<Notification> GetLatestNotification()
        {
            var items = await GetItemsAsync();
            return items.ElementAt(0);
        }

        public override Task<IEnumerable<Notification>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = new []
            {
                new Notification
                {
                    Date = DateTime.UtcNow,
                    Text = "Welcome to CWITC!"
                }
            };
            return Task.FromResult(items as IEnumerable<Notification>);
        }
    }
}

