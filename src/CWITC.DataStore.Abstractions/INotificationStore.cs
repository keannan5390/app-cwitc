using System;
using CWITC.DataObjects;
using System.Threading.Tasks;

namespace CWITC.DataStore.Abstractions
{
    public interface INotificationStore : IBaseStore<Notification>
    {
        Task<Notification> GetLatestNotification();
    }
}

