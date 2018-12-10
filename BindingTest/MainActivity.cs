using Android.App;
using Android.Widget;
using Android.OS;

using Com.Permutive.Android;
using Java.Util;
using Android.Net;

using System;
using System.Collections.Generic;
using Uri = Android.Net.Uri;

//using Com.Example.Testlib;
//using Android.Arch.Persistence.Room;

//using IO.Reactivex;
//using Com.Squareup.Duktape;


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


            UUID TRANTOR_STAGING_API_KEY = UUID.FromString("5c2b415d-7b20-4bc9-84fb-4f04bf0e5743");
            UUID TRANTOR_STAGING_PROJECT_ID = UUID.FromString("be668577-07f5-444d-98e0-222b990951b1");



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
                        //.Identity("testIdentity@xamarin.com")
                        .Context(this)
                        .Build();


            permutive.SetIdentity("someIdentity");

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

                permutive.EventTracker().Track("pageView");
            }


            TriggersProvider triggers = permutive.TriggersProvider();


            /*
            //TestJava test = new TestJava();
            TestKotlin testKotlin = new TestKotlin();

            button.Text = "wha: "+ testKotlin.ReturnSomething();
            button.Text = "wha: " + testKotlin.TestWithArrow();
            button.Text = "huhwa: " + testKotlin.TestWithDuktape();

            //button.Text = "rx: " + Single.Just("Rx Hullo").BlockingGet();

            button.Text = "rx: " + testKotlin.TestWithRx();
            button.Text = "moshi2: " + testKotlin.TestMoshi();

            button.Text = "retrofit3: " + testKotlin.TestRetrofit();

            button.Text = "room: " + testKotlin.TestRoom(this);

            //Duktape dt = Duktape.Create();

            //button.Text = (string) dt.Evaluate("'hello world'.toUpperCase();");

            //button.Text = "hey";
            */

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }
    }
}

