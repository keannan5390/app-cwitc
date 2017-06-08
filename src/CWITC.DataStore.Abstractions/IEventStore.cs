using System;
using CWITC.DataObjects;

namespace CWITC.DataStore.Abstractions
{
    public interface IEventStore : IBaseStore<FeaturedEvent>
    {
    }
}

