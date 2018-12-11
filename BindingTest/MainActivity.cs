using Android.App;
using Android.Widget;
using Android.OS;

using Com.Permutive.Android;
using Java.Util;
using Android.Net;

using System;
using System.Collections.Generic;
using Uri = Android.Net.Uri;
using Java.Lang;
using Android.Runtime;
using Com.Permutive.Android.Internal;

namespace BindingTest
{
    [Activity(Label = "BindingTest", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private int count = 1;
        private Permutive permutive;

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
            permutive.SetUrl(Uri.Parse("http://permutive.com/tutorials"));
            permutive.SetReferrer(Uri.Parse("http://permutive.com/tutorials?referrer=johnDoe"));


            EventTracker eventTracker = permutive.EventTracker();
            bool doStuff = false;
            if (doStuff)
            {
            }

            TriggersProvider triggersProvider = permutive.TriggersProvider();
            TriggersProvider.TriggerAction querySegments = triggersProvider.QuerySegments(new SegmentListener());

            TriggersProvider.TriggerAction queryReactions = triggersProvider.QueryReactions("dfp", new ReactionsListener());

            TriggersProvider.TriggerAction specific = triggersProvider.InvokeTriggerAction(1068, new QueryListener());

            TriggersProvider.TriggerAction map = triggersProvider.TriggerActionMap(1068, new QueryListener());

            //queryReactions.Close();

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }

        private void trackEvent()
        {
            EventProperties eventProperties =
                new EventProperties.Builder()
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
                .With("innerMap", new EventProperties.Builder()
                                    .Build())
                .WithStrings("stringArray", new List<string> { "string" })
                //.WithBooleans("booleanArray", new List<Boolean> { Boolean.True })
                //.WithInts("intArray", new List<Int> { 1 })
                //.WithLongs("longArray", 1L)
                //.WithFloats("floatArray", 1f)
                //.WithDoubles("doubleArray", 1.0)
                //.WithEventProperties("innerMap", 
                //new EventProperties.Builder()
                //.Build()
                //)
                .Build();

            permutive.EventTracker().Track("pageView", eventProperties);
        }

    }


    class SegmentListener : Java.Lang.Object, Com.Permutive.Android.Internal.IMethod
    {

        void IMethod.Invoke(Java.Lang.Object p0)
        {
            Java.Util.IList segments = p0.JavaCast<Java.Util.IList>();
            Android.Util.Log.Debug("WTF", $"Segments updated: {segments}");
        }
    }

    class ReactionsListener : Java.Lang.Object, Com.Permutive.Android.Internal.IMethod
    {

        void IMethod.Invoke(Java.Lang.Object p0)
        {
            Java.Util.IList segments = p0.JavaCast<Java.Util.IList>();
            Android.Util.Log.Debug("WTF", $"Reactions updated: {segments}");
        }
    }

    class QueryListener : Java.Lang.Object, Com.Permutive.Android.Internal.IMethod
    {

        void IMethod.Invoke(Java.Lang.Object p0)
        {
            Android.Util.Log.Debug("WTF", $"Query updated: {p0} {p0.GetType()}");
        }
    }

}

