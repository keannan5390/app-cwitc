﻿using System;
using Xamarin.Forms;
using CWITC.Clients.Portable;
using System.Globalization;
using CWITC.DataObjects;
using Humanizer;
using System.Diagnostics;

namespace CWITC.Clients.UI
{
    class SessionTimeDisplayConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var session = value as Session;
                if(session == null)
                    return string.Empty;

                return Device.OS == TargetPlatform.iOS ? session.GetDisplayTime() : session.GetDisplayName();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert: " + ex);
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    class SessionDateDisplayConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var session = value as Session;
                if(session == null)
                    return string.Empty;
                
                return session.GetDisplayName();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert: " + ex);
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    class EventDateDisplayConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var featured = value as FeaturedEvent;
                if(featured == null)
                    return string.Empty;
                
                return featured.GetDisplayName();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert: " + ex);
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
        
    class EventTimeDisplayConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var featured = value as FeaturedEvent;
                if(featured == null)
                    return string.Empty;

                return featured.GetDisplayTime();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert: " + ex);
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    class EventDayDisplayConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if(!(value is DateTime))
                    return string.Empty;

                return ((DateTime)value).ToEasternTimeZone().DayOfWeek.ToString().Substring(0,3).ToUpperInvariant();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert: " + ex);
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class EventColorDisplayConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var accent = (Color)Application.Current.Resources["Accent"];;
            try
            {
                if (!(value is DateTime))
                    return accent;

                return DateTime.UtcNow > ((DateTime)value).ToUniversalTime() ? Color.FromHex("D3D2D2") : accent;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert: " + ex);
            }

            return accent;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class HumanizeDateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is DateTime) 
                {
                    var date = ((DateTime)value);
                    if (date.Kind == DateTimeKind.Local) 
                    {
                        return date.Humanize (false);
                    }

                    return date.Humanize ();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to convert: " + ex);
            }

            return string.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

