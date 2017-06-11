using System;
#if __ANDROID__
namespace CWITC.Droid
{
    public partial class AndroidAuthSSOClient
#elif __IOS__
namespace CWITC.iOS
{
    public partial class iOSAuthSSOClient
#endif

    {
        
    }
}
