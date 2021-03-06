﻿using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using CWITC.Clients.Portable;
using System.Diagnostics;
#if __IOS__
using Microsoft.Azure.Mobile.Analytics;
#endif

[assembly:Dependency(typeof(EvolveLogger))]
namespace CWITC.Clients.Portable
{
    public class EvolveLogger : ILogger
    {
        bool enableHockeyApp = false;

#region ILogger implementation
       
        public virtual void TrackPage(string page, string id = null)
        {
            Debug.WriteLine("Evolve Logger: TrackPage: " + page.ToString() + " Id: " + id ?? string.Empty);

            if (!enableHockeyApp)
                return;
#if __ANDROID__
            Com.Microsoft.Azure.Mobile.Analytics.AndroidAnalytics.TrackEvent($"{page}Page");
#elif __IOS__
            Analytics.TrackEvent($"{page}Page");
#endif
        }


        public virtual void Track(string trackIdentifier)
        {
            Debug.WriteLine("Evolve Logger: Track: " + trackIdentifier);

            if (!enableHockeyApp)
                return;

#if __ANDROID__
            Com.Microsoft.Azure.Mobile.Analytics.AndroidAnalytics.TrackEvent(trackIdentifier);
#elif __IOS__
            Analytics.TrackEvent(trackIdentifier);
#endif
        }

        public virtual void Track(string trackIdentifier, string key, string value)
        {
            Debug.WriteLine("Evolve Logger: Track: " + trackIdentifier + " key: " + key + " value: " + value);

            if (!enableHockeyApp)
                return;
            
            trackIdentifier = $"{trackIdentifier}-{key}-{@value}";

#if __ANDROID__
            Com.Microsoft.Azure.Mobile.Analytics.AndroidAnalytics.TrackEvent(trackIdentifier);
#elif __IOS__
            Analytics.TrackEvent(trackIdentifier);
#endif
        }
       
        public virtual void Report(Exception exception = null, Severity warningLevel = Severity.Warning)
        {
            Debug.WriteLine("Evolve Logger: Report: " + exception);
        }
        public virtual void Report(Exception exception, IDictionary extraData, Severity warningLevel = Severity.Warning)
        {
            Debug.WriteLine("Evolve Logger: Report: " + exception);
        }
        public virtual void Report(Exception exception, string key, string value, Severity warningLevel = Severity.Warning)
        {
            Debug.WriteLine("Evolve Logger: Report: " + exception + " key: " + key + " value: " + value);
        }
#endregion
    }

  
}

