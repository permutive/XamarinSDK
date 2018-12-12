using System;
using System.Collections.Generic;

namespace Permutive.Xamarin
{

    public interface AliasProvider
    {

    }


    public sealed class PermutiveOptions
    {
        public string ProjectId;
        public string ApiKey;
        public List<AliasProvider> AliasProviders = new List<AliasProvider>();
        public string identity = null;
    }

    public abstract class EventTracker
    {
        public abstract void TrackEvent(string eventName, Dictionary<string, object> properties = null);
    }

    public abstract class TriggersProvider
    {
        public abstract IDisposable QuerySegments(Action<List<int>> callback);
        public abstract IDisposable QueryReactions(string reaction, Action<List<int>> callback);
        public abstract IDisposable TriggerAction<T>(int queryId, Action<T> callback);
        //public abstract IDisposable TriggerActionMap<T>(int queryId, Action<T> callback); ?
    }


    public abstract class Permutive : IDisposable
    {
        public abstract void Dispose();

        public abstract Permutive Initialize(PermutiveOptions options);

        public abstract EventTracker EventTracker();

        public abstract TriggersProvider TriggersProvider();

        public abstract void SetIdentity(string identity);

        public abstract void SetTitle(string title);
        public abstract void SetReferrer(Uri referrer);
        public abstract void SetUrl(Uri url);
    }
}
