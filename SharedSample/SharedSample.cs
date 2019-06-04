using System;
using System.Collections.Generic;
using Permutive.Xamarin;

namespace SharedSample
{
    public interface Logger
    {
        void Log(string message);
    }

    public class PermutiveSdkTester
    {
        private Logger logger;
        private PermutiveSdk permutive;

        public PermutiveSdkTester(PermutiveSdk permutive, Logger logger)
        {
            this.permutive = permutive;
            this.logger = logger;
        }

        public void Initialise(IList<IAliasProvider> providers)
        {
            PermutiveOptions options = new PermutiveOptions();
            options.ApiKey = "da4d09b5-843a-4bd5-bd79-8cea7f69f730";
            options.ProjectId = "e0039147-51e7-4224-a814-0e2d438aabcd";
            options.AliasProviders = providers;
            permutive.Initialize(options);
        }


        public void Setup()
        {
            permutive.SetTitle("Xamarin example");
            permutive.SetUrl(new Uri("http://permutive.com/tutorials"));
            permutive.SetReferrer(new Uri("http://permutive.com/tutorials?referrer=johnDoe"));

            TriggersProvider triggersProvider = permutive.TriggersProvider();

            var querySegmentsDisposable = triggersProvider.QuerySegments(result =>
            {
                logger.Log("Current user is in segments:");
                foreach (var value in result)
                {
                    logger.Log( $"\t{value}");
                }
            });


            var queryReactionsDisposable = triggersProvider.QueryReactions("dfp", result =>
            {
                logger.Log( "Current user is in DFP segments:");
                foreach (var value in result)
                {
                    logger.Log( $"\t{value}");
                }
            });


            var triggerDisposable = triggersProvider.TriggerAction<bool>(1068, result => logger.Log( $"Is user in segment 1068? {result}"));
        }

        public void SetIdentity()
        {
            permutive.SetIdentity("testIdentity1@test.com");
        }

        public void TrackEvent()
        {
            EventProperties eventProperties =
                permutive.CreateEventPropertiesBuilder()
                .With("string", "string")
                .With("boolean", true)
                .With("int", 1)
                .With("long", 1L)
                .With("float", 1f)
                .With("double", 1.0)
                .With("geo", EventProperties.GEO_INFO)
                .With("isp", EventProperties.ISP_INFO)
                .With("innermap", permutive.CreateEventPropertiesBuilder()
                                    .Build())
                .With("stringarray", new List<string> { "string" })
                .With("booleanarray", new List<bool> { true })
                .With("intarray", new List<int> { 1 })
                .With("longarray", new List<long> { 1L })
                .With("floatarray", new List<float> { 1f })
                .With("doublearray", new List<double> { 1.0 })
                .With("innermaps", new List<EventProperties> {permutive.CreateEventPropertiesBuilder()
                .Build() }
                )
                .Build();

            //permutive.EventTracker().TrackEvent("Pageview", eventProperties);
            permutive.EventTracker().TrackEvent("Pageview");
        }

    }
}
