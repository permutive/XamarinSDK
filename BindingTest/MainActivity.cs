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
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            bool useRelease = false;

            UUID projectId;
            UUID apiKey;

            if (useRelease)
            {
                projectId = UUID.FromString("e0039147-51e7-4224-a814-0e2d438aabcd");
                apiKey = UUID.FromString("da4d09b5-843a-4bd5-bd79-8cea7f69f730");
            }
            else
            {
                projectId = UUID.FromString("5c2b415d-7b20-4bc9-84fb-4f04bf0e5743");
                apiKey = UUID.FromString("be668577-07f5-444d-98e0-222b990951b1");
            }


            Permutive permutive = new Permutive.Builder()
                        .ApiKey(projectId)
                        .ProjectId(apiKey)
                        //.AliasProvider(null)
                        //.Identity("testIdentity@xamarin.com")
                        .Context(this)
                        .Build();


            //permutive.SetIdentity("someIdentity");

            permutive.SetTitle("Xamarin example");
            permutive.SetUrl(Uri.Parse("http://permutive.com/tutorials"));
            permutive.SetReferrer(Uri.Parse("http://permutive.com/tutorials?referrer=johnDoe"));


            EventTracker eventTracker = permutive.EventTracker();
            bool doStuff = false;
            if (doStuff)
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


            TriggersProvider triggersProvider = permutive.TriggersProvider();
            TriggersProvider.TriggerAction querySegments = triggersProvider.QuerySegments(new SegmentListener());

            TriggersProvider.TriggerAction queryReactions = triggersProvider.QueryReactions("dfp", new ReactionsListener());

            TriggersProvider.TriggerAction specific = triggersProvider.InvokeTriggerAction(1068, new QueryListener());

            TriggersProvider.TriggerAction map = triggersProvider.TriggerActionMap(1068, new QueryListener());

            //queryReactions.Close();

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
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

