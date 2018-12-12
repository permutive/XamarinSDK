using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Com.Permutive.Android.Internal;
using Java.Lang;

namespace Permutive.Xamarin
{
    public class PermutiveImpl : Permutive
    {
        private Context context;
        private Com.Permutive.Android.Permutive permutive;

        public PermutiveImpl(Context context)
        {
            this.context = context;
        }

        public override void Dispose()
        {
            permutive.Close();
            permutive = null;
        }

        public override EventTracker EventTracker()
        {
            throw new NotImplementedException();
        }

        public override Permutive Initialize(PermutiveOptions options)
        {
            permutive = new Com.Permutive.Android.Permutive.Builder()
                    .Context(context)
                    //.ProjectId(Java.Util.UUID.FromString("5c2b415d-7b20-4bc9-84fb-4f04bf0e5743"))
                    //.ApiKey(Java.Util.UUID.FromString("be668577-07f5-444d-98e0-222b990951b1"))
                    .ProjectId(Java.Util.UUID.FromString(options.ProjectId))
                    .ApiKey(Java.Util.UUID.FromString(options.ApiKey))
                    //other stuff?
                    .Build();

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
    };

    internal class TriggersProviderImpl : TriggersProvider
    {
        private Com.Permutive.Android.TriggersProvider triggersProvider;

        internal TriggersProviderImpl(Com.Permutive.Android.TriggersProvider triggersProvider)
        {
            this.triggersProvider = triggersProvider;
        }

        public override IDisposable QueryReactions(string reaction, Action<List<int>> callback)
        {
            MethodListWrapper<int> methodWrapper = new MethodListWrapper<int>(callback);
            Com.Permutive.Android.TriggersProvider.TriggerAction action = triggersProvider.QueryReactions(reaction, methodWrapper);
            return new TriggerActionWrapper(action);
        }

        public override IDisposable QuerySegments(Action<List<int>> callback)
        {
            MethodListWrapper<int> methodWrapper = new MethodListWrapper<int>(callback);
            Com.Permutive.Android.TriggersProvider.TriggerAction action = triggersProvider.QuerySegments(methodWrapper);
            return new TriggerActionWrapper(action);
        }

        public override IDisposable TriggerAction<T>(int queryId, Action<T> callback)
        {
            MethodWrapper<T> methodWrapper = new MethodWrapper<T>(callback);
            Com.Permutive.Android.TriggersProvider.TriggerAction action = triggersProvider.InvokeTriggerAction(queryId, methodWrapper);
            return new TriggerActionWrapper(action);
        }
    }


    class TriggerActionWrapper : IDisposable
    {
        private Com.Permutive.Android.TriggersProvider.TriggerAction triggerAction;

        internal TriggerActionWrapper(Com.Permutive.Android.TriggersProvider.TriggerAction triggerAction)
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


    //Can be of type:
    //Boolean
    //String
    //Int
    //Long
    //Float
    //Double
    //Map<String, Any> ..?

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
            Java.Util.IList list = obj.JavaCast<Java.Util.IList>();

            var returnList = new List<T>(list.Size());

            for (int index = 0; index < list.Size(); index++)
            {
                returnList.Add(Utils.ConvertBasicType<T>(list.Get(index)));
            }

            action(returnList);
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
                throw new IllegalArgumentException("Type parameter must be: bool/string/int/long/float/double");
            }

            return returns;
        }

    }

}

