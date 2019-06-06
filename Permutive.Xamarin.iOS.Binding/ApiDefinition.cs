using System;
using CoreFoundation;
using Foundation;
using ObjCRuntime;

namespace Permutive.Xamarin.iOS.Binding
{
    // @interface PermutiveEventPropertyValueConsts : NSObject
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface PermutiveEventPropertyValueConsts
    {
        // @property (readonly, copy, nonatomic, class) NSString * _Nonnull geo_info;
        [Static]
        [Export("geo_info")]
        string Geo_info { get; }

        // @property (readonly, copy, nonatomic, class) NSString * _Nonnull isp_info;
        [Static]
        [Export("isp_info")]
        string Isp_info { get; }
    }

    // typedef void (^PermutiveEventCallback)(NSError * _Nullable, NSString * _Nonnull, NSDictionary<NSString *,id> * _Nonnull);
    delegate void PermutiveEventCallback([NullAllowed] NSError arg0, string arg1, NSDictionary<NSString, NSObject> arg2);

    interface IPermutiveEventActionInterface { }

    // @protocol PermutiveEventActionInterface
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface PermutiveEventActionInterface
    {
        // @required -(void)track:(NSString * _Nonnull)eventName properties:(NSDictionary<NSString *,id> * _Nonnull)properties;
        [Abstract]
        [Export("track:properties:")]
        void Properties(string eventName, NSDictionary<NSString, NSObject> properties);

        // @required -(void)track:(NSString * _Nonnull)eventName;
        [Abstract]
        [Export("track:")]
        void Track(string eventName);
    }

    // typedef void (^PermutiveTriggerActionBlock)(NSUInteger, NSNumber * _Nullable, NSNumber * _Nullable);
    delegate void PermutiveTriggerActionBlock(nuint arg0, [NullAllowed] NSNumber arg1, [NullAllowed] NSNumber arg2);

    interface IPermutiveTriggerAction { }

    // @protocol PermutiveTriggerAction <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface PermutiveTriggerAction
    {
        // @required -(void)setTriggerBlock:(PermutiveTriggerActionBlock _Nonnull)triggerBlock __attribute__((deprecated("")));
        [Abstract]
        [Export("setTriggerBlock:")]
        void SetTriggerBlock(PermutiveTriggerActionBlock triggerBlock);
    }


    interface IPermutiveTriggersProvider { }

    // @protocol PermutiveTriggersProvider <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface PermutiveTriggersProvider
    {
        // @required @property (readonly, copy, nonatomic) NSArray<NSNumber *> * _Nonnull querySegments;
        [Abstract]
        [Export("querySegments", ArgumentSemantic.Copy)]
        NSNumber[] QuerySegments { get; }

        // @required @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSArray<NSNumber *> *> * _Nonnull dfpRequestCustomTargeting;
        [Abstract]
        [Export("dfpRequestCustomTargeting", ArgumentSemantic.Copy)]
        NSDictionary<NSString, NSArray<NSNumber>> DfpRequestCustomTargeting { get; }

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForSegments:(NSArray<NSNumber *> * _Nonnull)segments triggerType:(PermutiveTriggerType)triggerType __attribute__((deprecated("")));
        [Abstract]
        [Export("triggerActionForSegments:triggerType:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForSegments(NSNumber[] segments, PermutiveTriggerType triggerType);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForAllSegmentsWithTriggerType:(PermutiveTriggerType)triggerType __attribute__((deprecated("")));
        [Abstract]
        [Export("triggerActionForAllSegmentsWithTriggerType:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForAllSegmentsWithTriggerType(PermutiveTriggerType triggerType);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForSegment:(NSNumber * _Nonnull)segment callback:(void (^ _Nonnull)(BOOL))callback;
        [Abstract]
        [Export("triggerActionForSegment:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForSegment(NSNumber segment, Action<bool> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForSegments:(NSArray<NSNumber *> * _Nonnull)segments callback:(void (^ _Nonnull)(NSNumber * _Nonnull, BOOL))callback;
        [Abstract]
        [Export("triggerActionForSegments:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForSegments(NSNumber[] segments, Action<NSNumber, bool> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForAllSegmentsWithCallback:(void (^ _Nonnull)(NSNumber * _Nonnull, BOOL))callback;
        [Abstract]
        [Export("triggerActionForAllSegmentsWithCallback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForAllSegmentsWithCallback(Action<NSNumber, bool> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForMapQueryIDs:(NSArray<NSNumber *> * _Nonnull)queryIDs callback:(void (^ _Nonnull)(NSNumber * _Nonnull, NSDictionary<NSString *,id> * _Nonnull))callback;
        [Abstract]
        [Export("triggerActionForMapQueryIDs:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForMapQueryIDs(NSNumber[] queryIDs, Action<NSNumber, NSDictionary<NSString, NSObject>> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForObjectQueryID:(NSNumber * _Nonnull)queryID callback:(void (^ _Nonnull)(id _Nonnull))callback;
        [Abstract]
        [Export("triggerActionForObjectQueryID:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForObjectQueryID(NSNumber queryID, Action<NSObject> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForObjectQueryIDs:(NSArray<NSNumber *> * _Nonnull)queryIDs callback:(void (^ _Nonnull)(NSNumber * _Nonnull, id _Nonnull))callback;
        [Abstract]
        [Export("triggerActionForObjectQueryIDs:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForObjectQueryIDs(NSNumber[] queryIDs, Action<NSNumber, NSObject> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForStringQueryID:(NSNumber * _Nonnull)queryID callback:(void (^ _Nonnull)(NSString * _Nonnull))callback;
        [Abstract]
        [Export("triggerActionForStringQueryID:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForStringQueryID(NSNumber queryID, Action<NSString> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForStringQueryIDs:(NSArray<NSNumber *> * _Nonnull)queryIDs callback:(void (^ _Nonnull)(NSNumber * _Nonnull, NSString * _Nonnull))callback;
        [Abstract]
        [Export("triggerActionForStringQueryIDs:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForStringQueryIDs(NSNumber[] queryIDs, Action<NSNumber, NSString> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForDoubleQueryID:(NSNumber * _Nonnull)queryID callback:(void (^ _Nonnull)(double))callback;
        [Abstract]
        [Export("triggerActionForDoubleQueryID:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForDoubleQueryID(NSNumber queryID, Action<double> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForDoubleQueryIDs:(NSArray<NSNumber *> * _Nonnull)queryIDs callback:(void (^ _Nonnull)(NSNumber * _Nonnull, double))callback;
        [Abstract]
        [Export("triggerActionForDoubleQueryIDs:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForDoubleQueryIDs(NSNumber[] queryIDs, Action<NSNumber, double> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForIntegerQueryID:(NSNumber * _Nonnull)queryID callback:(void (^ _Nonnull)(NSInteger))callback;
        [Abstract]
        [Export("triggerActionForIntegerQueryID:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForIntegerQueryID(NSNumber queryID, Action<nint> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForIntegerQueryIDs:(NSArray<NSNumber *> * _Nonnull)queryIDs callback:(void (^ _Nonnull)(NSNumber * _Nonnull, NSInteger))callback;
        [Abstract]
        [Export("triggerActionForIntegerQueryIDs:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForIntegerQueryIDs(NSNumber[] queryIDs, Action<NSNumber, nint> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForBooleanQueryID:(NSNumber * _Nonnull)queryID callback:(void (^ _Nonnull)(BOOL))callback;
        [Abstract]
        [Export("triggerActionForBooleanQueryID:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForBooleanQueryID(NSNumber queryID, Action<bool> callback);

        // @required -(id<PermutiveTriggerAction> _Nullable)triggerActionForBooleanQueryIDs:(NSArray<NSNumber *> * _Nonnull)queryIDs callback:(void (^ _Nonnull)(NSNumber * _Nonnull, BOOL))callback;
        [Abstract]
        [Export("triggerActionForBooleanQueryIDs:callback:")]
        [return: NullAllowed]
        IPermutiveTriggerAction TriggerActionForBooleanQueryIDs(NSNumber[] queryIDs, Action<NSNumber, bool> callback);
    }


    // @interface PermutiveEventActionContext : NSObject <NSCopying>
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface PermutiveEventActionContext : INSCopying
    {
        // @property (copy, nonatomic) NSURL * _Nullable domain;
        [NullAllowed, Export("domain", ArgumentSemantic.Copy)]
        NSUrl Domain { get; set; }

        // @property (copy, nonatomic) NSURL * _Nullable url;
        [NullAllowed, Export("url", ArgumentSemantic.Copy)]
        NSUrl Url { get; set; }

        // @property (copy, nonatomic) NSURL * _Nullable referrer;
        [NullAllowed, Export("referrer", ArgumentSemantic.Copy)]
        NSUrl Referrer { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable title;
        [NullAllowed, Export("title")]
        string Title { get; set; }
    }

    //Bare interface
    interface IPermutiveProviderInterface { }


    // @protocol PermutiveProviderInterface <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface PermutiveProviderInterface
    {
        // @required @property (readonly, nonatomic, strong) id<PermutiveEventActionInterface> _Nonnull eventTracker;
        [Abstract]
        [Export("eventTracker", ArgumentSemantic.Strong)]
        IPermutiveEventActionInterface EventTracker { get; }

        // @required @property (readonly, nonatomic, strong) id<PermutiveTriggersProvider> _Nonnull triggersProvider;
        [Abstract]
        [Export("triggersProvider", ArgumentSemantic.Strong)]
        IPermutiveTriggersProvider TriggersProvider { get; }
    }

    // @interface PermutiveOptions : NSObject <NSCopying>
    [Protocol]
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface PermutiveOptions : INSCopying
    {
        // +(instancetype _Nullable)optionsWithProjectId:(NSUUID * _Nonnull)projectId apiKey:(NSUUID * _Nonnull)apiKey;
        [Static]
        [Export("optionsWithProjectId:apiKey:")]
        [return: NullAllowed]
        PermutiveOptions OptionsWithProjectId(NSUuid projectId, NSUuid apiKey);

        // @property (copy, nonatomic) NSString * _Nullable userIdentity;
        [NullAllowed, Export("userIdentity")]
        string UserIdentity { get; set; }
    }

    // @interface Permutive : NSObject
    [Protocol]
    [BaseType(typeof(NSObject), Name="Permutive")]
    interface PermutiveIosSdk
    {
        // +(PermutiveEventActionContext * _Nonnull)context;
        // +(void)setContext:(PermutiveEventActionContext * _Nonnull)context;
        [Static]
        [Export("context")]
        PermutiveEventActionContext Context { get; set; }

        // +(NSString * _Nonnull)userId;
        [Static]
        [Export("userId")]
        string UserId();

        // +(void)configureWithOptions:(PermutiveOptions * _Nonnull)options;
        [Static]
        [Export("configureWithOptions:")]
        void ConfigureWithOptions(PermutiveOptions options);

        // +(id<PermutiveProviderInterface> _Nullable)permutive;
        [Static]
        [Export("permutive")]
        [return: NullAllowed]
        IPermutiveProviderInterface GetPermutive();

        // +(void)setIdentity:(NSString * _Nonnull)identity;
        [Static]
        [Export("setIdentity:")]
        void SetIdentity(string identity);

        // +(void)reset;
        [Static]
        [Export("reset")]
        void Reset();
    }
}