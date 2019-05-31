using System;
using System.Collections.Generic;
using Permutive.Xamarin.iOS.Binding;
using Foundation;

namespace Permutive.Xamarin
{
    public class PermutiveImpl : Permutive
    {

        public override void Dispose()
        {
            //does nothing
        }

        public override EventTracker EventTracker()
        {
            return new EventTrackerImpl(PermutiveSdk.GetPermutive().EventTracker);
        }

        public override Permutive Initialize(PermutiveOptions options)
        {
            iOS.Binding.PermutiveOptions permutiveOptions = iOS.Binding.PermutiveOptions.OptionsWithProjectId(new NSUuid(options.ProjectId), new NSUuid(options.ApiKey));
            permutiveOptions.UserIdentity = options.identity;

            PermutiveSdk.ConfigureWithOptions(permutiveOptions);

            return this;
        }

        public override void SetIdentity(string identity)
        {
            PermutiveSdk.SetIdentity(identity);
        }

        public override void SetReferrer(Uri referrer)
        {
            PermutiveSdk.Context.SetReferrer(NSUrl.FromString(referrer.ToString()));
        }

        public override void SetTitle(string title)
        {
            PermutiveSdk.Context.SetTitle(title);
        }

        public override void SetUrl(Uri url)
        {
            PermutiveSdk.Context.SetUrl(NSUrl.FromString(url.ToString()));
        }

        public override TriggersProvider TriggersProvider()
        {
            return new TriggersProviderImpl(PermutiveSdk.GetPermutive().TriggersProvider);
        }

        public override EventProperties.Builder CreateEventPropertiesBuilder()
        {
            return new EventPropertiesImpl.BuilderImpl();
        }
    };

    internal class TriggersProviderImpl : TriggersProvider
    {
        private iOS.Binding.PermutiveTriggersProvider triggersProvider;

        internal TriggersProviderImpl(iOS.Binding.PermutiveTriggersProvider triggersProvider)
        {
            this.triggersProvider = triggersProvider;
        }

        public override IDisposable QueryReactions(string reaction, Action<List<int>> callback) 
        {
            if (reaction == "dfp")
            {
                //do something...
                //callback()
                //triggersProvider.DfpRequestCustomTargeting

            }
            else
            {
                throw new ArgumentException("Illegal argument {reaction} only dfp is allowed.");
            }

            return null;
        }

        public override IDisposable QuerySegments(Action<List<int>> callback)
        {
            //Action<NSNumber, bool> action = new Action<NSNumber, bool>();
            //triggersProvider.TriggerActionForAllSegmentsWithCallback()
            //MethodListWrapper<int> methodWrapper = new MethodListWrapper<int>(callback);
            //Com.Permutive.Android.TriggersProvider.TriggerAction action = triggersProvider.QuerySegments(methodWrapper);
            //return new TriggerActionWrapper(action);
            return null;
        }

        public override IDisposable TriggerAction<T>(int queryId, Action<T> callback)
        {
            /*
            MethodWrapper<T> methodWrapper = new MethodWrapper<T>(callback);


            Type type = typeof(T);
            PermutiveTriggerAction triggerAction;


            NSNumber queryIdAsNsNumber = NSNumber.FromInt32(queryId);

            if (type == typeof(bool))
            {
                triggerAction = triggersProvider.TriggerActionForBooleanQueryID(queryIdAsNsNumber, (Action<bool>)(object)callback);
            }
            else if (type == typeof(string))
            {
                new Action<NSString>
                {

                };
                triggerAction = triggersProvider.TriggerActionForStringQueryID(NSNumber.FromInt32(queryId), (Action<string>)(object)callback);
                //triggerAction = triggersProvider.TriggerActionForStringQueryID(NSNumber.FromInt32(queryId), (Action<string>)(object)callback);

            }
            */


            /*
            T returns;

            if (type == typeof(bool))
            {
                Java.Lang.Boolean value = obj.JavaCast<Java.Lang.Boolean>();
                returns = (T)(object)value.BooleanValue();
            }
            else if (type == typeof(string))
            {
                Java.Lang.String value = obj.JavaCast<Java.Lang.String>();
                returns = (T)(object)value.ToString();
            }
            else if (type == typeof(int))
            {
                Java.Lang.Integer value = obj.JavaCast<Java.Lang.Integer>();
                returns = (T)(object)value.IntValue();
            }
            else if (type == typeof(long))
            {
                Java.Lang.Long value = obj.JavaCast<Java.Lang.Long>();
                returns = (T)(object)value.LongValue();
            }
            else if (type == typeof(float))
            {
                Java.Lang.Float value = obj.JavaCast<Java.Lang.Float>();
                returns = (T)(object)value.FloatValue();
            }
            else if (type == typeof(double))
            {
                Java.Lang.Double value = obj.JavaCast<Java.Lang.Double>();
                returns = (T)(object)value.DoubleValue();
            }
            else
            {
                //throw new IllegalArgumentException($"Type parameter must be: bool/string/int/long/float/double but was {type}");
            }
*/



            //Com.Permutive.Android.TriggersProvider.TriggerAction action = triggersProvider.InvokeTriggerAction(queryId, methodWrapper);
            //return new TriggerActionWrapper(action);
            return null;
        }
    }

    public class EventPropertiesImpl : EventProperties
    {
        internal NSDictionary<NSString,NSObject> eventProperties;

        internal EventPropertiesImpl(NSDictionary<NSString, NSObject> eventProperties)
        {
            this.eventProperties = eventProperties;
        }

        public class BuilderImpl : EventProperties.Builder
        {
            private NSDictionary<NSString, NSObject> builder;

            internal BuilderImpl()
            {
                this.builder = new NSDictionary<NSString, NSObject>();
            }

            public override Builder With(string key, bool value)
            {
                builder[key] = NSObject.FromObject(value);
                return this;
            }

            public override Builder With(string key, string value)
            {
                builder[key] = NSObject.FromObject(value);
                return this;
            }

            public override Builder With(string key, int value)
            {
                builder[key] = NSObject.FromObject(value);
                return this;
            }

            public override Builder With(string key, long value)
            {
                builder[key] = NSObject.FromObject(value);
                return this;
            }

            public override Builder With(string key, float value)
            {
                builder[key] = NSObject.FromObject(value);
                return this;
            }

            public override Builder With(string key, double value)
            {
                builder[key] = NSObject.FromObject(value);
                return this;
            }

            public override Builder With(string key, EventProperties value)
            {
                builder[key] = ((EventPropertiesImpl)value).eventProperties;
                return this;
            }

            public override Builder With(string key, IList<bool> value)
            {
                builder[key] = convertToArray(value);
                return this;
            }

            public override Builder With(string key, IList<string> value)
            {
                builder[key] = convertToArray(value);
                return this;
            }

            public override Builder With(string key, IList<int> value)
            {
                builder[key] = convertToArray(value);
                return this;
            }

            public override Builder With(string key, IList<long> value)
            {
                builder[key] = convertToArray(value);
                return this;
            }

            public override Builder With(string key, IList<float> value)
            {
                builder[key] = convertToArray(value);
                return this;
            }

            public override Builder With(string key, IList<double> value)
            {
                builder[key] = convertToArray(value);
                return this;
            }

            public override Builder With(string key, IList<EventProperties> value)
            {
                NSMutableArray mutableArray = new NSMutableArray();
                foreach (var item in value)
                {
                    mutableArray.Add(((EventPropertiesImpl)item).eventProperties);
                }

                builder[key] = mutableArray;
                return this;
            }

            public override EventProperties Build()
            {
                return new EventPropertiesImpl(builder);
            }

            static private NSArray convertToArray<T>(IList<T> list)
            {
                NSMutableArray mutableArray = new NSMutableArray();
                foreach (var item in list)
                {
                    mutableArray.Add(NSObject.FromObject(item));
                }

                return mutableArray;
            }
        }
    }

    internal class EventTrackerImpl : EventTracker
    {
        private PermutiveEventActionInterface eventTracker;

        internal EventTrackerImpl(PermutiveEventActionInterface eventTracker)
        {
            this.eventTracker = eventTracker;
        }

        public override void TrackEvent(string eventName, EventProperties properties = null)
        {
            if (properties != null)
            {
                eventTracker.Properties(eventName, ((EventPropertiesImpl)properties).eventProperties);
            }
            else
            {
                eventTracker.Track(eventName);
            }
        }
    }

    internal class TriggerActionWrapper : IDisposable
    {
        private PermutiveTriggerAction triggerAction;

        internal TriggerActionWrapper(PermutiveTriggerAction triggerAction)
        {
            this.triggerAction = triggerAction;
        }

        public void Dispose()
        {
            triggerAction = null;
        }
    }

    /*
    class MethodWrapper<T> : Java.Lang.Object, Com.Permutive.Android.Internal.IMethod
    {
        private Action<T> action;

        public MethodWrapper(Action<T> action)
        {
            this.action = action;
        }

        void IMethod.Invoke(Java.Lang.Object obj)
        {
            action(Utils.ConvertBasicType<T>(obj));
        }
    }

    class MethodListWrapper<T> : Java.Lang.Object, Com.Permutive.Android.Internal.IMethod
    {
        private Action<List<T>> action;

        public MethodListWrapper(Action<List<T>> action)
        {
            this.action = action;
        }

        void IMethod.Invoke(Java.Lang.Object obj)
        {
            action(Utils.ConvertList<T>(obj));
        }
    }
    */

    class Utils
    {
        /*
        internal class ActionConverter<T> : Action<T>
        {

        }


        internal static T ConvertBasicType<T>(NSObject obj)
        {
            Type type = typeof(T);
            T returns;

            if (type == typeof(bool))
            {
                Java.Lang.Boolean value = obj.JavaCast<Java.Lang.Boolean>();
                returns = (T)(object)value.BooleanValue();
            }
            else if (type == typeof(string))
            {
                returns = (T)(object)obj.ToString();
            }
            else if (type == typeof(int))
            {
                NSNumber.FromObject()
                Java.Lang.Integer value = obj.JavaCast<Java.Lang.Integer>();
                returns = (T)(object)value.IntValue();
            }
            else if (type == typeof(long))
            {
                Java.Lang.Long value = obj.JavaCast<Java.Lang.Long>();
                returns = (T)(object)value.LongValue();
            }
            else if (type == typeof(float))
            {
                Java.Lang.Float value = obj.JavaCast<Java.Lang.Float>();
                returns = (T)(object)value.FloatValue();
            }
            else if (type == typeof(double))
            {
                Java.Lang.Double value = obj.JavaCast<Java.Lang.Double>();
                returns = (T)(object)value.DoubleValue();
            }
            else
            {
                //throw new IllegalArgumentException($"Type parameter must be: bool/string/int/long/float/double but was {type}");
            }

            return returns;
        }
*/

        //internal static List<T> ConvertList<T>(Java.Lang.Object obj)
        //{
        //    Java.Util.IList list = obj.JavaCast<Java.Util.IList>();

        //    var returnList = new List<T>(list.Size());

        //    for (int index = 0; index < list.Size(); index++)
        //    {
        //        returnList.Add(Utils.ConvertBasicType<T>(list.Get(index)));
        //    }

        //    return returnList; 
        //}
    }
}

