﻿using System;

namespace CWITC.Clients.Portable
{
   
    public static class ApiKeys
    {
        public const string VSMobileCenterApiKeyAndroid = "ea8830bf-0486-40f8-96b5-52f5cd93761b";
        public const string VSMobileCenterApiKeyIOS = "4db3b3ce-08d1-4c01-be8f-a01921f2019e";

        public const string GrouveEventCode = "GZ49ME";
    }


    public static class MessageKeys
    {
        public const string LoginCallback = "auth_login_callback";
        public const string LogoutCallback = "auth_logout_callback";
        public const string NavigateToEvent = "navigate_event";
        public const string NavigateToSessionList = "navigate_session_list";
        public const string NavigateToSession = "navigate_session";
        public const string NavigateToSpeaker = "navigate_speaker";
        public const string NavigateToSponsor = "navigate_sponsor";
        public const string NavigateToImage = "navigate_image";
        public const string NavigateLogin = "navigate_login";
        public const string Error = "error";
        public const string Connection = "connection";
        public const string LoggedIn = "loggedin";
        public const string Message = "message";
        public const string Question = "question";
        public const string Choice = "choice";
        public const string TwitterAuthRefreshed = "refresh_tweets";
    }
}

