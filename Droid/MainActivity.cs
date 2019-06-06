using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Permutive.Xamarin;

namespace SharedSample.Droid
{
    public class AndroidLogger : Logger
    {
        public void Log(string message)
        {
            Android.Util.Log.Debug("Permutive", message);
        }
    }



    [Activity(Label = "SharedSample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private PermutiveSdkTester tester;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            tester = new PermutiveSdkTester(new Permutive.Xamarin.PermutiveAndroid(this), new AndroidLogger());

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            Button sendEventButton = FindViewById<Button>(Resource.Id.sendEventButton);
            Button setIdentityButton = FindViewById<Button>(Resource.Id.setIdentityButton);

            startButton.Click += delegate {
                //var providers = new List<IAliasProvider> { new AliasAaidProvider(this) };
                tester.Initialise(null);
                tester.Setup();
             };
            sendEventButton.Click += delegate {
                tester.TrackEvent();
             };
            setIdentityButton.Click += delegate {
                tester.SetIdentity();
             };
        }
    }
}

