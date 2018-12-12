using Android.App;
using Android.Widget;
using Android.OS;

using System;
using System.Collections.Generic;
using Android.Runtime;
using Permutive.Xamarin;

namespace BindingTest
{
    [Activity(Label = "BindingTest", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private int count = 1;
        private Permutive.Xamarin.Permutive permutive;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            BindingTestApplication application = (BindingTestApplication)Application;

            permutive = application.GetPermutive();

            //permutive.SetIdentity("someIdentity");

            permutive.SetTitle("Xamarin example");
            //permutive.SetUrl(Uri.Parse("http://permutive.com/tutorials"));
            //permutive.SetReferrer(Uri.Parse("http://permutive.com/tutorials?referrer=johnDoe"));


            //EventTracker eventTracker = permutive.EventTracker();

            TriggersProvider triggersProvider = permutive.TriggersProvider();


            triggersProvider.QuerySegments(result =>
            {
                Android.Util.Log.Debug("WTF", "WTF: MonitorSegments result:");
                foreach (var value in result)
                {
                    Android.Util.Log.Debug("WTF", $"WTF:\t{value}");
                }
            });


            triggersProvider.QueryReactions("dfp", result =>
            {
                Android.Util.Log.Debug("WTF", "WTF: Monitor DFP reactions result:");
                foreach (var value in result)
                {
                    Android.Util.Log.Debug("WTF", $"WTF:\t{value}");
                }
            });

            var thing = triggersProvider.TriggerAction<bool>(1068, result => Android.Util.Log.Debug("WTF", $"WTF: 1068 : {result}"));

            //triggersProvider.TriggerAction<Dictionary<string, object>>(1068, result => Android.Util.Log.Debug("WTF", $"WTF: 1068 (map) : {result}"));

            /*
            TriggersProvider.TriggerAction map = triggersProvider.TriggerActionMap(1068, new QueryListener());
            */



            //queryReactions.Dispose();

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
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
                .With("innerMap", permutive.CreateEventPropertiesBuilder()
                                    .Build())
                .With("stringArray", new List<string> { "string" })
                .With("booleanArray", new List<bool> { true })
                .With("intArray", new List<int> { 1 })
                .With("longArray", new List<long> { 1L })
                .With("floatArray", new List<float> { 1f })
                .With("doubleArray", new List<double> { 1.0 })
                .With("innerMaps", new List<EventProperties> {permutive.CreateEventPropertiesBuilder()
                .Build() }
                )
                .Build();

            permutive.EventTracker().TrackEvent("Pageview", eventProperties);
        }
    }
}

