﻿using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle("CWITC.Droid")]
[assembly: AssemblyDescription("CWITC Android")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CENWIDEV")]
[assembly: AssemblyProduct("CWITC")]
[assembly: AssemblyCopyright("Copyright © 2016 CENWIDEV All Rights Reserved")]
[assembly: AssemblyTrademark("CENWIDEV")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.0")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: Permission(Name = Android.Manifest.Permission.Internet)]
[assembly: Permission(Name = Android.Manifest.Permission.WriteExternalStorage)]
[assembly: Permission(Name = Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: Permission(Name = Android.Manifest.Permission.AccessFineLocation)]
[assembly: Permission(Name = Android.Manifest.Permission.AccessLocationExtraCommands)]

// This will prevent other apps on the device from receiving GCM messages for this app
// It is crucial that the package name does not start with an uppercase letter - this is forbidden by Android.
[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]

// Gives the app permission to register and receive messages.
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

// NOTE: Facebook SDK rquires that the 'Value' point to a string resource
//       in your values/ folder (eg: strings.xml file).
//       It will not allow you to use the app_id value directly here!
[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/facebook_app_id")]