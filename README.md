# How-to-add-FingerPrint-authentication-for-Xamarin.Forms-app

Authentication plays a vital role in case of business or banking applications. Xamarin forms FingerPrint(https://www.nuget.org/packages/Plugin.Fingerprint/) open source plugin which uses the framework security service makes it easy and simple to use for cross platform applications. Setting up the plugin in each platform is so simple like having a toast after morning jog.

## Setting up in android:

### Enable Permission:

Add the 'USE_FINGERPRINT' permission request in Android project properties or add them manually in AndroidManifest.xml

```
<uses-permission android:name="android.permission.USE_FINGERPRINT" />
```

### Get current activity

The authentication dialog will be popped up over the Xamarin.Forms page. To allow this action, need to get the current activity of the application which can be done using Xamarin.Forms Essentials in MainActivity.cs.

```
protected override void OnCreate(Bundle savedInstanceState)
{
    base.OnCreate(savedInstanceState);

    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
    CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    LoadApplication(new App());
}
```

That's all for android! :O

## Setting up in iOS:

### Enable Permission:

Request permission to access the FaceID(Applicable for both Face ID and finger print ID) and specify the reason in Info.plist file.

```
<key>NSFaceIDUsageDescription</key>
<string>AuthenticateFingerAndFaceID</string>
```

aaaaand done ;)

## Configuring authentication in PCL:

Here comes the great part. First step is to check whether the finger print authentication is available and set for the device. If available, we can move on to retreiving the status of the authentication. Since every device has 2 step authentication, we can also enable alternative authentication by enabling the 'AllowAlternativeAuthentication' API.

```
private async Task<bool> GetAuthentication()
{
    var availability = await CrossFingerprint.Current.GetAvailabilityAsync(true);
    if (availability == FingerprintAvailability.Available)
    {
        var authenticate = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Authentication required", "Touch the fingerprint sensor") { AllowAlternativeAuthentication = true });

        if (authenticate.Status == FingerprintAuthenticationResultStatus.Succeeded)
        {
            return true;
        }
    }

    return false;
}
```

For more details regarding the platform specific limitations along with API and its functions, refer to the documentation repository below.

https://github.com/smstuebe/xamarin-fingerprint


