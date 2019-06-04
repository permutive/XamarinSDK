using System;
using System.Collections.Generic;
using Permutive.Xamarin.iOS.Binding;
using Foundation;
using System.Threading;
using System.Threading.Tasks;

namespace Permutive.Xamarin
{
    public class PermutiveImpl : PermutiveSdk
    {

        public override void Dispose()
        {
            //does nothing
        }

        public override EventTracker EventTracker()
        {
            return new EventTrackerImpl(PermutiveIosSdk.GetPermutive().EventTracker);
        }

        public override PermutiveSdk Initialize(PermutiveOptions options)
        {
            iOS.Binding.PermutiveOptions permutiveOptions = iOS.Binding.PermutiveOptions.OptionsWithProjectId(new NSUuid(options.ProjectId), new NSUuid(options.ApiKey));
            permutiveOptions.UserIdentity = options.identity;

            PermutiveIosSdk.ConfigureWithOptions(permutiveOptions);

            return this;
        }

        public override void SetIdentity(string identity)
        {
            PermutiveIosSdk.SetIdentity(identity);
        }

        public override void SetReferrer(Uri referrer)
        {
            PermutiveIosSdk.Context.SetReferrer(NSUrl.FromString(referrer.ToString()));
        }

        public override void SetTitle(string title)
        {
            PermutiveIosSdk.Context.SetTitle(title);
        }

        public override void SetUrl(Uri url)
        {
            PermutiveIosSdk.Context.SetUrl(NSUrl.FromString(url.ToString()));
        }

        public override TriggersProvider TriggersProvider()
        {
            return new TriggersProviderImpl(PermutiveIosSdk.GetPermutive().TriggersProvider);
        }

        public override EventProperties.Builder CreateEventPropertiesBuilder()
        {
            return new EventPropertiesImpl.BuilderImpl();
        }
    };

    internal class TriggersProviderImpl : TriggersProvider
    {
        private Permutive.Xamarin.iOS.Binding.IPermutiveTriggersProvider triggersProvider;

        internal TriggersProviderImpl(Permutive.Xamarin.iOS.Binding.IPermutiveTriggersProvider triggersProvider)
        {
            this.triggersProvider = triggersProvider;
        }

        public override IDisposable QueryReactions(string reaction, Action<List<int>> callback) 
        {
            if (reaction == "dfp")
            {
                Action<int> callbackWrapper = new Action<int>(a => {
                    NSDictionary<NSString, NSArray<NSNumber>> dictionary = triggersProvider.DfpRequestCustomTargeting;

                    NSArray<NSNumber> numbers = dictionary.ObjectForKey((NSString) NSObject.FromObject("permutive"));
                    List<int> list = new List<int>();
                    if (numbers != null)
                    {
                        foreach (var num in numbers)
                        {
                            list.Add(num.Int32Value);
                        }
                    }

                    callback(list);
                });

                Action<int> debouncedCallbackWrapper = callbackWrapper.Debounce(300);

                triggersProvider.TriggerActionForAllSegmentsWithCallback((segmentCode, active) => { debouncedCallbackWrapper(segmentCode.Int32Value); });
            }
            else
            {
                throw new ArgumentException("Illegal argument {reaction} only dfp is allowed.");
            }

            return null;
        }

        public override IDisposable QuerySegments(Action<List<int>> callback)
        {
            Action<int> callbackWrapper = new Action<int>(a => {
                NSNumber[] number = triggersProvider.QuerySegments;
                List<int> list = new List<int>(number.Length);
                foreach (var num in number)
                {
                    list.Add(num.Int32Value);
                }

                callback(list);
            });

            Action<int> debouncedCallbackWrapper = callbackWrapper.Debounce(300);

            triggersProvider.TriggerActionForAllSegmentsWithCallback((segmentCode, active) => { debouncedCallbackWrapper(segmentCode.Int32Value); });
            return null;
        }

        public override IDisposable TriggerAction<T>(int queryId, Action<T> callback)
        {
            Type type = typeof(T);
            IPermutiveTriggerAction triggerAction;

            NSNumber queryIdAsNsNumber = NSNumber.FromInt32(queryId);

            if (type == typeof(bool))
            {
                triggerAction = triggersProvider.TriggerActionForBooleanQueryID(queryIdAsNsNumber, (Action<bool>)(object)callback); //TEST this
            }
            else if (type == typeof(string))
            {
                Action<NSString> actionWrapper = new Action<NSString> (a => callback((T)(object)a.ToString()));
                triggerAction = triggersProvider.TriggerActionForStringQueryID(queryIdAsNsNumber, actionWrapper);
            }
            else if (type == typeof(int))
            {
                triggerAction = triggersProvider.TriggerActionForIntegerQueryID(queryIdAsNsNumber, (Action<nint>)(object)callback); //TEST this
            }
            else if (type == typeof(long))
            {
                triggerAction = triggersProvider.TriggerActionForIntegerQueryID(queryIdAsNsNumber, (Action<nint>)(object)callback); //TEST this
            }
            else if (type == typeof(float))
            {
                Action<double> actionWrapper = new Action<double>(a => callback((T)(object)Convert.ToSingle(a)));
                triggerAction = triggersProvider.TriggerActionForDoubleQueryID(queryIdAsNsNumber, (Action<double>)(object)callback); //TEST this
            }
            else if (type == typeof(double))
            {
                triggerAction = triggersProvider.TriggerActionForDoubleQueryID(queryIdAsNsNumber, (Action<double>)(object)callback); //TEST this
            }
            else
            {
                throw new ArgumentException($"Type parameter must be: bool/string/int/long/float/double but was {type}");
            }

            return new TriggerActionWrapper(triggerAction);
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
            private Dictionary<string, NSObject> builder = new Dictionary<string, NSObject>();

            public override Builder With(string key, bool value)
            {
                builder.Add(key, NSObject.FromObject(value));
                return this;
            }

            public override Builder With(string key, string value)
            {
                builder.Add(key, NSObject.FromObject(value));
                return this;
            }

            public override Builder With(string key, int value)
            {
                builder.Add(key, NSObject.FromObject(value));
                return this;
            }

            public override Builder With(string key, long value)
            {
                builder.Add(key, NSObject.FromObject(value));
                return this;
            }

            public override Builder With(string key, float value)
            {
                builder.Add(key, NSObject.FromObject(value));
                return this;
            }

            public override Builder With(string key, double value)
            {
                builder.Add(key, NSObject.FromObject(value));
                return this;
            }

            public override Builder With(string key, EventProperties value)
            {
                builder.Add(key, ((EventPropertiesImpl)value).eventProperties);
                return this;
            }

            public override Builder With(string key, IList<bool> value)
            {
                builder.Add(key, convertToArray(value));
                return this;
            }

            public override Builder With(string key, IList<string> value)
            {
                builder.Add(key, convertToArray(value));
                return this;
            }

            public override Builder With(string key, IList<int> value)
            {
                builder.Add(key, convertToArray(value));
                return this;
            }

            public override Builder With(string key, IList<long> value)
            {
                builder.Add(key, convertToArray(value));
                return this;
            }

            public override Builder With(string key, IList<float> value)
            {
                builder.Add(key, convertToArray(value));
                return this;
            }

            public override Builder With(string key, IList<double> value)
            {
                builder.Add(key, convertToArray(value));
                return this;
            }

            public override Builder With(string key, IList<EventProperties> value)
            {
                NSMutableArray mutableArray = new NSMutableArray();
                foreach (var item in value)
                {
                    mutableArray.Add(((EventPropertiesImpl)item).eventProperties);
                }

                builder.Add(key, mutableArray);
                return this;
            }

            public override EventProperties Build()
            {
                NSString[] keys = new NSString[builder.Count]; 
                NSObject[] values = new NSObject[builder.Count];

                var index = 0;
                foreach (KeyValuePair<string, NSObject> entry in builder)
                {
                    keys.SetValue(new NSString(entry.Key), index);
                    values.SetValue(entry.Value, index);
                    index++;
                }

                NSDictionary < NSString, NSObject> dictionary = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(values,keys);
                return new EventPropertiesImpl(dictionary);
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
        private IPermutiveEventActionInterface eventTracker;

        internal EventTrackerImpl(IPermutiveEventActionInterface eventTracker)
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
        private IPermutiveTriggerAction triggerAction;

        internal TriggerActionWrapper(IPermutiveTriggerAction triggerAction)
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

    static class Utils
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

        //lifted from: https://stackoverflow.com/questions/28472205/c-sharp-event-debounce - note this is simple solution but is not super robust (see comments).
        internal static Action<T> Debounce<T>(this Action<T> func, int milliseconds = 300)
        {
            var last = 0;
            return arg =>
            {
                var current = Interlocked.Increment(ref last);
                Task.Delay(milliseconds).ContinueWith(task =>
                {
                    if (current == last) func(arg);
                    task.Dispose();
                });
            };
        }
    }
}

