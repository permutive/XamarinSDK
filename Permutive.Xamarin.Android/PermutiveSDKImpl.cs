using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Com.Permutive.Android.Internal;
using Java.Lang;

namespace Permutive.Xamarin
{
    public class PermutiveAndroid : PermutiveSdk
    {
        private Context context;
        private Com.Permutive.Android.Permutive permutive;

        public PermutiveAndroid(Context context)
        {
            this.context = context;
        }

        public Com.Permutive.Android.Permutive InternalPermutive()
        {
            return permutive;
        }

        public override void Dispose()
        {
            permutive.Close();
            permutive = null;
        }

        public override EventTracker EventTracker()
        {
            return new EventTrackerImpl(permutive.EventTracker());
        }

        public override PermutiveSdk Initialize(PermutiveOptions options)
        {
            Com.Permutive.Android.Permutive.Builder builder = new Com.Permutive.Android.Permutive.Builder()
                .Context(context)
                .ProjectId(Java.Util.UUID.FromString(options.ProjectId))
                .ApiKey(Java.Util.UUID.FromString(options.ApiKey));

            if (options.AliasProviders != null)
            {
                foreach (var provider in options.AliasProviders)
                {
                    if (provider.InternalProvider() is Com.Permutive.Android.Identify.AliasProvider internalProvider)
                    {
                        builder.AliasProvider(internalProvider);
                    }
                }
            }

            permutive = builder.Build();

            return this;
        }

        public override void SetIdentity(string identity)
        {
            permutive.SetIdentity(identity);
        }

        public override void SetReferrer(Uri referrer)
        {
            permutive.SetReferrer(Android.Net.Uri.Parse(referrer.ToString()));
        }

        public override void SetTitle(string title)
        {
            permutive.SetTitle(title);
        }

        public override void SetUrl(Uri url)
        {
            permutive.SetUrl(Android.Net.Uri.Parse(url.ToString()));
        }

        public override TriggersProvider TriggersProvider()
        {
            return new TriggersProviderImpl(permutive.TriggersProvider());
        }

        public override EventProperties.Builder CreateEventPropertiesBuilder()
        {
            return new EventPropertiesImpl.BuilderImpl();
        }
    };

    internal class TriggersProviderImpl : TriggersProvider
    {
        private Com.Permutive.Android.ITriggersProvider triggersProvider;

        internal TriggersProviderImpl(Com.Permutive.Android.ITriggersProvider triggersProvider)
        {
            this.triggersProvider = triggersProvider;
        }

        public override IDisposable QueryReactions(string reaction, Action<List<int>> callback)
        {
            MethodListWrapper<int> methodWrapper = new MethodListWrapper<int>(callback);
            Com.Permutive.Android.ITriggerAction action = triggersProvider.QueryReactions(reaction, methodWrapper);
            return new TriggerActionWrapper(action);
        }

        public override IDisposable QuerySegments(Action<List<int>> callback)
        {
            MethodListWrapper<int> methodWrapper = new MethodListWrapper<int>(callback);
            Com.Permutive.Android.ITriggerAction action = triggersProvider.QuerySegments(methodWrapper);
            return new TriggerActionWrapper(action);
        }

        public override IDisposable TriggerAction<T>(int queryId, Action<T> callback)
        {
            MethodWrapper<T> methodWrapper = new MethodWrapper<T>(callback);
            Com.Permutive.Android.ITriggerAction action = triggersProvider.TriggerAction(queryId, methodWrapper);
            return new TriggerActionWrapper(action);
        }
    }

    public class EventPropertiesImpl : EventProperties
    {
        internal Com.Permutive.Android.EventProperties eventProperties;

        internal EventPropertiesImpl(Com.Permutive.Android.EventProperties eventProperties)
        {
            this.eventProperties = eventProperties;
        }

        public class BuilderImpl : EventProperties.Builder
        {
            private Com.Permutive.Android.EventProperties.Builder builder;

            internal BuilderImpl()
            {
                this.builder = new Com.Permutive.Android.EventProperties.Builder();
            }

            public override Builder With(string key, bool value)
            {
                builder.With(key, value);
                return this;
            }

            public override Builder With(string key, string value)
            {
                builder.With(key, value);
                return this;
            }

            public override Builder With(string key, int value)
            {
                builder.With(key, value);
                return this;
            }

            public override Builder With(string key, long value)
            {
                builder.With(key, value);
                return this;
            }

            public override Builder With(string key, float value)
            {
                builder.With(key, value);
                return this;
            }

            public override Builder With(string key, double value)
            {
                builder.With(key, value);
                return this;
            }

            public override Builder With(string key, EventProperties value)
            {
                builder.With(key, ((EventPropertiesImpl) value).eventProperties);
                return this;
            }

            public override Builder With(string key, IList<bool> value)
            {
                builder.WithBooleans(key, new List<bool>(value).ConvertAll<Java.Lang.Boolean>(v => Java.Lang.Boolean.ValueOf(v)));
                return this;
            }

            public override Builder With(string key, IList<string> value)
            {
                builder.WithStrings(key, value);
                return this;
            }

            public override Builder With(string key, IList<int> value)
            {
                builder.WithInts(key, new List<int>(value).ConvertAll<Java.Lang.Integer>(v => Java.Lang.Integer.ValueOf(v)));
                return this;
            }

            public override Builder With(string key, IList<long> value)
            {
                builder.WithLongs(key, new List<long>(value).ConvertAll<Java.Lang.Long>(v => Java.Lang.Long.ValueOf(v)));
                return this;
            }

            public override Builder With(string key, IList<float> value)
            {
                builder.WithFloats(key, new List<float>(value).ConvertAll<Java.Lang.Float>(v => Java.Lang.Float.ValueOf(v)));
                return this;
            }

            public override Builder With(string key, IList<double> value)
            {
                builder.WithDoubles(key, new List<double>(value).ConvertAll<Java.Lang.Double>(v => Java.Lang.Double.ValueOf(v)));
                return this;
            }

            public override Builder With(string key, IList<EventProperties> value)
            {
                builder.WithEventProperties(key, new List<EventProperties>(value).ConvertAll<Com.Permutive.Android.EventProperties>(v => ((EventPropertiesImpl)v).eventProperties));
                return this;
            }

            public override EventProperties Build()
            {
                return new EventPropertiesImpl(builder.Build());
            }
        }
    }

    internal class EventTrackerImpl : EventTracker
    {
        private Com.Permutive.Android.IEventTracker eventTracker;

        internal EventTrackerImpl(Com.Permutive.Android.IEventTracker eventTracker)
        {
            this.eventTracker = eventTracker;
        }

        public override void TrackEvent(string eventName, EventProperties properties = null)
        {
            if (properties != null)
            {
                eventTracker.Track(eventName, ((EventPropertiesImpl)properties).eventProperties);
            }
            else
            {
                eventTracker.Track(eventName);
            }
        }
    }

    internal class TriggerActionWrapper : IDisposable
    {
        private Com.Permutive.Android.ITriggerAction triggerAction;

        internal TriggerActionWrapper(Com.Permutive.Android.ITriggerAction triggerAction)
        {
            this.triggerAction = triggerAction;
        }

        public void Dispose()
        {
            if (triggerAction != null)
            {
                triggerAction.Close();
                triggerAction = null;
            }
        }
    }

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

    class Utils
    {
        internal static T ConvertBasicType<T>(Java.Lang.Object obj)
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
                throw new IllegalArgumentException($"Type parameter must be: bool/string/int/long/float/double but was {type}");
            }

            return returns;
        }

        internal static List<T> ConvertList<T>(Java.Lang.Object obj)
        {
            Java.Util.IList list = obj.JavaCast<Java.Util.IList>();

            var returnList = new List<T>(list.Size());

            for (int index = 0; index < list.Size(); index++)
            {
                returnList.Add(Utils.ConvertBasicType<T>(list.Get(index)));
            }

            return returnList; 
        }
    }
}

