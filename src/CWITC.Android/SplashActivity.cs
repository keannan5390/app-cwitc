﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace CWITC.Droid
{
    [Activity(Label = "CWITC 17", Icon = "@drawable/ic_launcher", Theme="@style/SplashTheme", MainLauncher=true)]            
    //[MetaData ("android.app.shortcuts", Resource ="@xml/shortcuts")]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}

