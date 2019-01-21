using Android.App;
using Android.Widget;
using Android.OS;

using System;
using System.Collections.Generic;
using Android.Runtime;
using Permutive.Xamarin;
using Android.Gms.Ads.DoubleClick;
//using Com.Permutive.Android.Ads;

namespace BindingTest
{
    [Activity(Label = "BindingTest", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Sensor)]
    public class MainActivity : Activity
    {
        private Permutive.Xamarin.Permutive permutive;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Button sendEventButton = FindViewById<Button>(Resource.Id.SendEventButton);
            Button setIdentityButton = FindViewById<Button>(Resource.Id.setIdentityButton);
            PublisherAdView publisherAdView = FindViewById<PublisherAdView>(Resource.Id.publisherAdView);

            BindingTestApplication application = (BindingTestApplication)Application;

            permutive = application.GetPermutive();

            permutive.SetTitle("Xamarin example");
            permutive.SetUrl(new Uri("http://permutive.com/tutorials"));
            permutive.SetReferrer(new Uri("http://permutive.com/tutorials?referrer=johnDoe"));

            TriggersProvider triggersProvider = permutive.TriggersProvider();

            var querySegmentsDisposable = triggersProvider.QuerySegments(result =>
            {
                Android.Util.Log.Debug("Permutive", "Current user is in segments:");
                foreach (var value in result)
                {
                    Android.Util.Log.Debug("Permutive", $"\t{value}");
                }
            });


            var queryReactionsDisposable = triggersProvider.QueryReactions("dfp", result =>
            {
                Android.Util.Log.Debug("Permutive", "Current user is in DFP segments:");
                foreach (var value in result)
                {
                    Android.Util.Log.Debug("Permutive", $"\t{value}");
                }
            });

            var triggerDisposable = triggersProvider.TriggerAction<bool>(1068, result => Android.Util.Log.Debug("Permutive", $"Is user in segment 1068? {result}"));

            //when we wish to stop listening to events: 
            //querySegmentsDisposable.Dispose();
            //queryReactionsDisposable.Dispose();
            //triggerDisposable.Dispose();

            sendEventButton.Click += delegate { trackEvent(); };
            setIdentityButton.Click += delegate { permutive.SetIdentity("testIdentity1@test.com"); };

            publisherAdView.LoadAd(
                new PublisherAdRequest.Builder()
                    .AddTestDevice(PublisherAdRequest.DeviceIdEmulator)
                    //.AddPermutiveTargeting(permutive)
                    .BuildWithPermutiveTargeting(permutive)
                    //.Build()
            );
        }

        private void trackEvent()
        {
            EventProperties eventProperties =
                permutive.CreateEventPropertiesBuilder()
                .With("string", "string")
                .With("boolean", true)
                .With("int", 1)
                .With("long", 1L)
                .With("float", 1f)
                .With("double", 1.0)
                .With("string", "string")
                .With("boolean", true)
                .With("int", 1)
                .With("long", 1L)
                .With("float", 1f)
                .With("double", 1.0)
                .With("geo", EventProperties.GEO_INFO)
                .With("isp", EventProperties.ISP_INFO)
                .With("innermap", permutive.CreateEventPropertiesBuilder()
                                    .Build())
                .With("stringarray", new List<string> { "string" })
                .With("booleanarray", new List<bool> { true })
                .With("intarray", new List<int> { 1 })
                .With("longarray", new List<long> { 1L })
                .With("floatarray", new List<float> { 1f })
                .With("doublearray", new List<double> { 1.0 })
                .With("innermaps", new List<EventProperties> {permutive.CreateEventPropertiesBuilder()
                .Build() }
                )
                .Build();

            //permutive.EventTracker().TrackEvent("Pageview", eventProperties);
            permutive.EventTracker().TrackEvent("Pageview");
        }
    }
}