using System;
using System.Collections.Generic;

namespace Permutive.Xamarin
{
    public interface IAliasProvider
    {
        object InternalProvider();
    }

    public sealed class PermutiveOptions
    {
        public string ProjectId;
        public string ApiKey;
        public List<IAliasProvider> AliasProviders = new List<IAliasProvider>();
        public string identity = null;
    }

    public abstract class EventProperties
    {
        public const string ISP_INFO = "$ip_isp_info";
        public const string GEO_INFO = "$ip_geo_info";

        public abstract class Builder
        {
            public abstract Builder With(string key, bool value);

            public abstract Builder With(string key, string value);

            public abstract Builder With(string key, int value);

            public abstract Builder With(string key, long value);

            public abstract Builder With(string key, float value);

            public abstract Builder With(string key, double value);

            public abstract Builder With(string key, EventProperties value);

            public abstract Builder With(string key, IList<bool> value);

            public abstract Builder With(string key, IList<string> value);

            public abstract Builder With(string key, IList<int> value);

            public abstract Builder With(string key, IList<long> value);

            public abstract Builder With(string key, IList<float> value);

            public abstract Builder With(string key, IList<double> value);

            public abstract Builder With(string key, IList<EventProperties> value);

            public abstract EventProperties Build();
        }
    }

    public abstract class EventTracker
    {
        public abstract void TrackEvent(string eventName, EventProperties properties = null);
    }

    public abstract class TriggersProvider
    {
        public abstract IDisposable QuerySegments(Action<List<int>> callback);
        public abstract IDisposable QueryReactions(string reaction, Action<List<int>> callback);
        public abstract IDisposable TriggerAction<T>(int queryId, Action<T> callback);
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

        public abstract EventProperties.Builder CreateEventPropertiesBuilder();
    }
}
