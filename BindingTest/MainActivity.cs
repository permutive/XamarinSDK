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

            MonitorSegments(result => {
                Android.Util.Log.Debug("WTF", "WTF: MonitorSegments result:");
                foreach(var value in result)
                {
                    Android.Util.Log.Debug("WTF", $"WTF:\t{value}");
                }
            });

            MonitorReactions("dfp", result => {
                Android.Util.Log.Debug("WTF", "WTF: MonitorReactions result:");
                foreach (var value in result)
                {
                    Android.Util.Log.Debug("WTF", $"WTF:\t{value}");
                }
            });

            //TriggersProvider.TriggerAction querySegments = triggersProvider.QuerySegments(new SegmentListener());

            /*
            TriggersProvider.TriggerAction queryReactions = triggersProvider.QueryReactions("dfp", new ReactionsListener());

            TriggersProvider.TriggerAction specific = triggersProvider.InvokeTriggerAction(1068, new QueryListener());

            TriggersProvider.TriggerAction map = triggersProvider.TriggerActionMap(1068, new QueryListener());
            */

            MonitorQuery<bool>(1068, result => Android.Util.Log.Debug("WTF", $"MonitorQuery result: {result}"));


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

        void MonitorSegments(Action<List<int>> action)
        {
            MethodListWrapper<int> methodWrapper = new MethodListWrapper<int>(action);
            permutive.TriggersProvider().QuerySegments(methodWrapper);
        }

        void MonitorReactions(string reaction, Action<List<int>> action)
        {
            MethodListWrapper<int> methodWrapper = new MethodListWrapper<int>(action);
            permutive.TriggersProvider().QueryReactions(reaction, methodWrapper);
        }

        void MonitorQuery<T>(int queryId, Action<T> action)
        {
            MethodWrapper<T> methodWrapper = new MethodWrapper<T>(action);
            permutive.TriggersProvider().InvokeTriggerAction(queryId, methodWrapper);
        }

    }

    //Can be of type:
    //Boolean
    //String
    //Int
    //Long
    //Float
    //Double
    //Map<String, Any>

    class MethodWrapper<T> : Java.Lang.Object, Com.Permutive.Android.Internal.IMethod
    {
        private Action<T> action;

        public MethodWrapper(Action<T> action)
        {
            this.action = action;
        }

        void IMethod.Invoke(Java.Lang.Object p0)
        {
            //Android.Util.Log.Debug("WTF", $"WTF: MethodWrapper::invoke({p0}) type is {p0.GetType()}");

            Type type = typeof(T);

            if (type == typeof(bool))
            {
                Java.Lang.Boolean value = p0.JavaCast<Java.Lang.Boolean>();
                action((T)(object)value.BooleanValue());
            }
            else if (type == typeof(string))
            {
                Java.Lang.String value = p0.JavaCast<Java.Lang.String>();
                action((T)(object)value.ToString());
            }
            else if (type == typeof(int))
            {
                Java.Lang.Integer value = p0.JavaCast<Java.Lang.Integer>();
                action((T)(object)value.IntValue());
            }
            else if (type == typeof(long))
            {
                Java.Lang.Long value = p0.JavaCast<Java.Lang.Long>();
                action((T)(object)value.LongValue());
            }
            else if (type == typeof(float))
            {
                Java.Lang.Float value = p0.JavaCast<Java.Lang.Float>();
                action((T)(object)value.FloatValue());
            }
            else if (type == typeof(double))
            {
                Java.Lang.Double value = p0.JavaCast<Java.Lang.Double>();
                action((T)(object)value.DoubleValue());
            }
            else
            {
                throw new IllegalArgumentException("Type parameter must be: bool/string/int/long/float/double");
            }
        }
    }


    class MethodListWrapper<T> : Java.Lang.Object, Com.Permutive.Android.Internal.IMethod
    {
        private Action<List<T>> action;

        public MethodListWrapper(Action<List<T>> action)
        {
            this.action = action;
        }

        void IMethod.Invoke(Java.Lang.Object p0)
        {
            Android.Util.Log.Debug("WTF", $"WTF: MethodListWrapper::invoke({p0}) type is {p0.GetType()}");

            Java.Util.IList list = p0.JavaCast<Java.Util.IList>();

            var returnList = new List<T>(list.Size());


            for (int index = 0; index < list.Size(); index++)
            {
                returnList.Add(convertBasicType(list.Get(index)));
            }


            action(returnList);

        }

        private T convertBasicType(Java.Lang.Object p0)
        {
            Type type = typeof(T);
            T returns;

            if (type == typeof(bool))
            {
                Java.Lang.Boolean value = p0.JavaCast<Java.Lang.Boolean>();
                returns = (T)(object)value.BooleanValue();
            }
            else if (type == typeof(string))
            {
                Java.Lang.String value = p0.JavaCast<Java.Lang.String>();
                returns = (T)(object)value.ToString();
            }
            else if (type == typeof(int))
            {
                Java.Lang.Integer value = p0.JavaCast<Java.Lang.Integer>();
                returns = (T)(object)value.IntValue();
            }
            else if (type == typeof(long))
            {
                Java.Lang.Long value = p0.JavaCast<Java.Lang.Long>();
                returns = (T)(object)value.LongValue();
            }
            else if (type == typeof(float))
            {
                Java.Lang.Float value = p0.JavaCast<Java.Lang.Float>();
                returns = (T)(object)value.FloatValue();
            }
            else if (type == typeof(double))
            {
                Java.Lang.Double value = p0.JavaCast<Java.Lang.Double>();
                returns = (T)(object)value.DoubleValue();
            }
            else
            {
                throw new IllegalArgumentException("Type parameter must be: bool/string/int/long/float/double");
            }

            return returns;
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

