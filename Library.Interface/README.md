Library.Interface describes the common interface between the Android and iOS libraries. Please note that iOS is not yet supported but will be in the near future.

The Xamarin SDK is comprised of one core SDK, and several kit libraries integrating with other 3rd Party SDKs.

Using multiple kit libraries enables feature-rich integration with minimal effort for developers and greater benefits to our clients.

## Download

To include in your Xamarin (Android) application, you should add the NuGet package. [See here](https://developer.xamarin.com/guides/cross-platform/xamarin-studio/nuget_walkthrough/)

```bash
Install-Package Permutive.Xamarin.Android -Version 0.1.1
```


## Initialise the SDK

The SDK requires that you provide it with a **Project ID** and **API Key** - which are available from the dashboard. None of the other functionality of the SDK is available until these are passed in.

The Permutive object should be created only once, we suggest initialising this in your application's onCreate method, or defining as a singleton in your DI graph.

Construction of the Permutive object is quick - it won't cause any issues with framerate drops (for the beta there are still some issues around this, but this will be fixed for release). You can start making permutive calls straight away.

```cs

using Permutive.Xamarin;
//..

  public override void OnCreate()
  {
    //..
    PermutiveOptions options = new PermutiveOptions();
    options.ApiKey= YOUR_API_KEY;
    options.ProjectId = YOUR_PROJECT_ID;

    var permutive = new PermutiveImpl(this);
    permutive.Initialize(options);
  }

```

## Identity Management

You can set a custom identity for the user, whenever it's available. This may be appropriate when users login to your app, and a custom identity such as an internal identifier or email address may be available. Identifying a user with a custom identity is useful if you want to identify and synchronise users across platforms, for example across Android and web.

You can start the Permutive SDK with an optional identity, if you have this:

```cs
    PermutiveOptions options = new PermutiveOptions();
    options.ApiKey= YOUR_API_KEY;
    options.ProjectId = YOUR_PROJECT_ID;
    options.Identity = "xamarin@permutive.com"; 

    var permutive = new PermutiveImpl(this);
    permutive.Initialize(options);
```

Or you can call setIdentity at any point after the Permutive SDK has been created.

```cs
permutive.SetIdentity("xamarin@permutive.com")
```


## Event Tracking

Events represent any activity in your application worth tracking. Examples include entering and leaving view, engagement changes (i.e. scrolling inside an article) and any other user or environment driven inputs.
To track an event, grab an instance of EventTracker object - and call the **Track** method. Event tracking can include properties passed in as a map.

Please note: event names should only contain characters in **[a-zA-Z0-9_]**, and property names should only contain characters in **[a-z0-9_]**. Any character outside the allowed character set will result in an IllegalArgumentException.

As with all other Permutive SDK APIs, the event tracker object is lightweight and multiple instances of it can be created, as needed. Event tracking is thread-safe, so feel free to track events from any thread. It is also asynchronous, and so will not block.

###Adding context

Just like on the web, you can add extra context to the events you are tracking. You can set values for url, title and referrer. Domain is automatically inferred from url, if it has been set. Subsequent calls to **Track** will contain the extra context as part of the event. The context is persisted between application instances.

```cs

EventTracker eventTracker = permutive.EventTracker();

eventTracker.TrackEvent("app launched", 
                         permutive.CreateEventPropertiesBuilder()
                             .With("first_time", true)
                             .Build());

eventTracker.TrackEvent("app started");

permutive.SetTitle("Xamarin Tutorial");
permutive.SetUrl(
    new Uri("http://www.permutive.com/android/tutorials"));
permutive.SetReferrer(
    new Uri("http://www.permutive.com/android/tutorials?referrer=johnDoe"));

//contains url/title/referrer & domain context implicitly
EventTracker.TrackEvent("page viewed") 

```

### Event enrichment

To pass through geographic or ISP information for your custom events, you can pass through a special value when tracking the event:

```cs

EventTracker eventTracker = permutive.EventTracker();

final EventProperties eventProperties =
    permutive.CreateEventPropertiesBuilder()
        .With("my_geo_info", EventProperties.GEO_INFO)
        .With("my_isp_info", EventProperties.ISP_INFO)
        .Build()

eventTracker.TrackEvent("eventX" , eventProperties)
```

The event will be enriched to (for example):
```json
{
    "my_geo_info": {
        "city": "Ayr",
        "continent": "Europe",
        "country": "United Kingdom",
        "postal_code": "KA7",
        "province": "South Ayrshire"
    },
    "my_isp_info": {
        "autonomous_system_number": 2856,
        "autonomous_system_organization": "British Telecommunications PLC",
        "isp": "BT",
        "organization": "BT"
    }
}

```

This also applies to any values that are deeper in the properties tree (since properties can contain other Map<String, Any> objects as values).

If at the time no geo or isp information is available, these properties are stripped from the event information.

## Segment Query

To track what segments the user belongs to, you can use the API call below. The callback will be called straight away with the current segments the user is in, and then called each time the segment list changes. Segments are returned as a list of Integers. These correspond to segment IDs setup in the Permutive Dashboard. The returned list is never null.

```cs
TriggerProvider triggerProvider = permutive.TriggerProvider();
var action =
    triggerProvider.QuerySegments(segments => 
    {
        foreach (var segment in segments)
        {
            Log($"The user is in segment: {segment}");
        }
    });
```

## Tracking Segment and Query Changes

To track real time changes to any queries, or segments, the developer can use trigger actions. Trigger actions contain a callback that is called immediately with the current value of that query/segment, and then each time that value changes. The value is **never** null. Please be aware that the callback may be called from a different thread than the one setting it up.

When you wish to stop receiving updates, call **Dispose** on the trigger action. Not calling dispose on a trigger action may result in context leaks, it's a good idea to dispose any actions that are associated with your UI component at an appropriate point in it's lifecycle (say for an Activity, at it's onPause/onStop callbacks).

Segments are always of type bool - so it is appropriate to use the TriggerAction<bool> for these. Queries can be of a simple type - bool, string, int, long, float, double. More complex types are not yet supported in the Xamarin SDK.

If a query Id does not exist, the trigger will still be created, but the callback will never be called. If, however, the query does exist at a later point (the SDK may fetch a fresher version of the queries), then the callback will be called when a value is present, and then for each time the value changes.

Using a wrong type for a given query Id will immediately log an error to the console. The callback will **never** be called.

```cs
TriggersProvider triggersProvider = permutive.TriggersProvider();

var triggerAction =
    triggersProvider.TriggerAction<bool>(1, value => Log($"Query 1 change: {value}")); //value is bool

//when you wish to stop tracking changes, dispose the action
triggerAction.Dispose();
```

## Tracking reactions

Tracking reactions is very similiar to tracking segment changes, once you register a listener, you will get the initial state, and on each change you will get an update until you close the trigger action.

```cs

var triggerProvider = permutive.TriggersProvider();
var queryReactionsDisposable =
    triggerProvider.QueryReactions("dfp",segments => 
    {
        foreach (var segment in segments){
            Log($"The user is in segment: {segment}");
        }
    });

//when you wish to stop tracking changes, dispose the action
queryReactionsDisposable.Dispose();

```

We have provided an easy way to add targeting for google ads requests. See the **google-ads** library instructions below.

## Tracking AAID identities

In addition to identifying your user via a custom identity, you can also automatically track a user by their AAID. To do this, just include the google-ads dependency in your gradle build:
```bash
Install-Package Permutive.Xamarin.Android.GoogleAds -Version 0.1.1
```

When creating the permutive object, simply add the **AliasAaidProvider** to the alias providers
```cs
using Permutive.Xamarin;
//..

  public override void OnCreate()
  {
    //..
    PermutiveOptions options = new PermutiveOptions();
    options.ApiKey= YOUR_API_KEY;
    options.ProjectId = YOUR_PROJECT_ID;
    options.AliasProviders = new List<IAliasProvider>
    {
        new AliasProvider(this)
    } 
	

    var permutive = new PermutiveImpl(this);
    permutive.Initialize(options);
  }
```

The AAID will be tracked automatically by the SDK as soon as it is available.

## Custom targeting with Google Ads

We provide an easy way to add custom targeting of your Permutive reactions to your google ads requests. To do this:
```cs
//To add custom targeting
val adRequest1 = 
            new PublisherAdRequest.Builder()
                .AddPermutiveTargeting(permutive)
                .build()

//Or simply without the build call:
val adRequest2 = 
        new PublisherAdRequest.Builder()
            .BuildWithPermutiveTargeting(permutive)
```
