//
//  PermutiveAPI.h
//  Permutive
//
//  Created by Gregg Jaskiewicz on 13/03/2018.
//  Copyright © 2018 Permutive. All rights reserved.
//

#import <Foundation/Foundation.h>


#ifndef PermutiveAPI_h
#define PermutiveAPI_h

/**
 Constant Event related values used in the SDK
 */

@interface PermutiveEventPropertyValueConsts: NSObject

/**
  use this value in your event properties if you want it to be replaced by geo information
 */
@property (nonatomic, copy, readonly, nonnull, class) NSString *geo_info;

/**
 use this value in your event properties if you want it to be replaced by isp information
 */
@property (nonatomic, copy, readonly, nonnull, class) NSString *isp_info;

@end

/**
 Callback passed optionally to each event. It will be called with eventName, as processed; properties, as processed; and if there was any error - eventError will be set appropriately.
 */
typedef void(^PermutiveEventCallback)(NSError * __nullable eventError, NSString * __nonnull eventName, NSDictionary<NSString *, id> * __nonnull properties);

/**
 Track an Event
*/
@protocol PermutiveEventActionInterface

/** Tracking events by calling this method
 @discussion Call this method to send an event. Only events defined in the dashboard will be recorded
 @param eventName The event name. Events which are not configured on the dashboard will be ignored.
 @param properties dictionary of values attached to the event. This will be sent to the backend.
*/
- (void)track:(NSString *__nonnull)eventName properties:(NSDictionary<NSString *, id> * __nonnull)properties;

/**
 Tracking events by calling this method
 @discussion Call this method to send an event. Only events defined in the dashboard will be recorded
 @param eventName The event name. Events which are not configured on the dashboard will be ignored.
*/
- (void)track:(NSString *__nonnull)eventName;

@end


typedef NS_ENUM(NSUInteger, PermutiveTriggerType) {
    PermutiveTriggerOnEnter,
    PermutiveTriggerOnLeave,
    PermutiveTriggerOnChange,
    PermutiveTriggerAlways,
};


typedef void(^PermutiveTriggerActionBlock)(NSUInteger segmentID, NSNumber * __nullable value, NSNumber * __nullable oldValue) __attribute__ ((deprecated));

/**
 Thin light object that will be given to the user of the SDK - to attach the block
 To resign from the callback, just release this object.
 */
@protocol PermutiveTriggerAction<NSObject>

/**
 Allows you to set the trigger block.
 @param triggerBlock The block cannot be nil
 */
- (void)setTriggerBlock:(PermutiveTriggerActionBlock __nonnull)triggerBlock  __attribute__ ((deprecated));

// more methods related to this trigger will go here

@end


/**
 @discussion Provides trigger for given segment, of trigger type
 */
@protocol PermutiveTriggersProvider<NSObject>

/**
 returns all current segments identified by the SDK
 */
@property (nonatomic, readonly, copy) NSArray<NSNumber *> * _Nonnull querySegments;

/**
 returns DFP specific custom targeting dictionary. This dictionary can be passed into currentTargeting property of DFPRequest object, found in Google Mobile Ads SDK. This will return an array of segments configured in the Permutive Dashboard for DFP
 */
@property (nonatomic, readonly, copy) NSDictionary<NSString *, NSArray<NSNumber *> *> * _Nonnull dfpRequestCustomTargeting;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific segment
 @param triggerType type of trigger required
 @param segments The IDs of segments you want to trigger action
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> __nullable)triggerActionForSegments:(NSArray<NSNumber *> *  _Nonnull)segments triggerType:(PermutiveTriggerType)triggerType __attribute__ ((deprecated));

/**
 Provide trigger action object for all segments,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific segment
 @param triggerType type of trigger required
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> __nullable)triggerActionForAllSegmentsWithTriggerType:(PermutiveTriggerType)triggerType __attribute__ ((deprecated));

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific segment
 @param segment The ID of segment you want to trigger action
 @param callback The block called on change of the segment
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
// TODO verify the ab ove statement
- (id<PermutiveTriggerAction> _Nullable)triggerActionForSegment:(NSNumber * __nonnull)segment callback:(void (^__nonnull)(BOOL value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific segment
 @param segments The IDs of segments you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForSegments:(NSArray<NSNumber *> * __nonnull)segments callback:(void (^__nonnull)(NSNumber *__nonnull segment, BOOL value))callback;

/**
 Provide trigger action object for all segments,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific segment
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForAllSegmentsWithCallback:(void (^ __nonnull)(NSNumber *__nonnull segmentID, BOOL value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific queries
 @param queryIDs The IDs of queries you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForMapQueryIDs:(NSArray<NSNumber *> * __nonnull)queryIDs callback:(void (^__nonnull)(NSNumber *__nonnull queryID, NSDictionary<NSString *, id> *__nonnull value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific query
 @param queryID The ID of query you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForObjectQueryID:(NSNumber * __nonnull)queryID callback:(void (^ __nonnull)(id __nonnull value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific queries
 @param queryIDs The IDs of queries you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForObjectQueryIDs:(NSArray<NSNumber *> * __nonnull)queryIDs callback:(void (^__nonnull)(NSNumber * __nonnull queryID, id __nonnull value ))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific query
 @param queryID The ID of query you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForStringQueryID:(NSNumber * __nonnull)queryID callback:(void (^ __nonnull)(NSString * __nonnull value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific queries
 @param queryIDs The IDs of queries you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForStringQueryIDs:(NSArray<NSNumber *> * __nonnull)queryIDs callback:(void (^ __nonnull)(NSNumber *__nonnull queryID, NSString * __nonnull value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific query
 @param queryID The ID of query you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForDoubleQueryID:(NSNumber * __nonnull)queryID callback:(void (^ __nonnull)(double value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific queries
 @param queryIDs The IDs of queries you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForDoubleQueryIDs:(NSArray<NSNumber *> * __nonnull)queryIDs callback:(void (^__nonnull)(NSNumber *__nonnull queryID, double value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific query
 @param queryID The ID of query you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForIntegerQueryID:(NSNumber * __nonnull)queryID callback:(void (^__nonnull)(NSInteger value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific queries
 @param queryIDs The IDs of queries you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForIntegerQueryIDs:(NSArray<NSNumber *> * __nonnull)queryIDs callback:(void (^__nonnull)(NSNumber *__nonnull queryID, NSInteger value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific query
 @param queryID The ID of query you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForBooleanQueryID:(NSNumber * __nonnull)queryID callback:(void (^ __nonnull)(BOOL value))callback;

/**
 Provide trigger action object for given segment,with trigger type.
 @discussion Trigger action allows user to set a callback block, or query this specific queries
 @param queryIDs The IDs of queries you want to trigger action
 @param callback The block called on change of the segments
 @returns Returns nil if segment does not exist in the current state machine configuration.
 */
- (id<PermutiveTriggerAction> _Nullable)triggerActionForBooleanQueryIDs:(NSArray<NSNumber *> * __nonnull)queryIDs callback:(void (^__nonnull)(NSNumber *__nonnull queryID, BOOL value))callback;



@end

/**
 Context container
 @discussion When using the JS SDK, each event by default will be enriched with domain, URL, referrer and Page Title Properties. Since these are not available on iOS - this allows the Application to create context which is supplied when requesting the event tracker. By default all properites are null
 */
@interface PermutiveEventActionContext: NSObject<NSCopying>

/**
 If domain has not been provided, and no url has been provided - the domain wil be set to the main application bundle ID.
 */
@property (nonatomic, copy, nullable) NSURL *domain;

/**
  If domain has not been set, the domain and host of the URL will be used to set the domain property.
 */
@property (nonatomic, copy, nullable) NSURL *url;
@property (nonatomic, copy, nullable) NSURL *referrer;
@property (nonatomic, copy, nullable) NSString *title;

@end

/**
 Main Permutive SDK API. This object will provide all required objects
 */
@protocol PermutiveProviderInterface<NSObject>

/**
 Returns instance of an Event Tracker object "PermutiveEventActionInterface". The event tracker objects are lightweight - and can be requested whenever needed.
 */
- (id<PermutiveEventActionInterface> _Nonnull)eventTracker;

/**
 Returns instance of an Event Tracker object "PermutiveEventActionInterface". The event tracker objects are lightweight - and can be requested whenever needed.
 */
@property (nonatomic, readonly, strong) id<PermutiveEventActionInterface> _Nonnull eventTracker;

/**
 Returns instance of an Trigger Provider object "PermutiveTriggersProvider". These objects are lightweight - and can be requested whenever needed.
 */
@property (nonatomic, readonly, strong) id<PermutiveTriggersProvider> _Nonnull triggersProvider;

// segments
// queries

@end

@interface PermutiveOptions: NSObject<NSCopying>

- (instancetype __nonnull)init __attribute__((unavailable("You cannot create a PermutiveOptions instance through init - please use optionsWithProjectId:apiKey:")));

/**
  Create configuration options for the SDK
  @param projectId ProjectID. This is available from the Permutive Dashboard
  @param apiKey Permutive API Key This is available from the Permutive Dashboard.
 */
+ (instancetype __nullable)optionsWithProjectId:(NSUUID * __nonnull)projectId apiKey:(NSUUID * __nonnull)apiKey;

@property (nonatomic, copy, nullable) NSString *userIdentity;

@end

typedef NS_ENUM(NSUInteger, PermutiveInterfaceBackendType) {
    PermutiveInterfaceBackendTypeProduction, // default
    PermutiveInterfaceBackendTypeStaging,
};

/**
 Main Permutive SDK object. Use this as your starting point.
 First API that needs to be called before anything else is configureWithProjectId:apiKey:
 All objects returned by this SDK are lightweight. There are no delegates here to content with :-)
 */
@interface Permutive: NSObject

/**
Context container
 **/
+ (PermutiveEventActionContext * __nonnull)context;

/**
 Set the context container
 **/
+ (void)setContext:(PermutiveEventActionContext * __nonnull)context;

/**
 Permutive unique Identifier
 **/
+ (NSString * __nonnull)userId;
/**
 Configure the SDK
 @discussion This method must be called before app accesses any other functions of this SDK
 */
+ (void)configureWithOptions:(PermutiveOptions * __nonnull)options;

/**
 Returns permutive services provider interface
 @discussion This is the main source of Permutive API instances.
 @returns Returns an instance of Permutive API provider. The underlying objects are very lightwaight and as such - application can create as many instances of permutive provider as required. This method will return nil, if API is not fully configured. See [Permutive configureWithProjectId:apiKey:]
 */
+ (id<PermutiveProviderInterface> __nullable)permutive;

/**
 Set current user identiy.
 @discussion Associate all events tracked afterwards with identity. All events retrieved afterwards will be those that belong to identity.
 @param identity specify identity. This could for instance be user name, although we recommend using derrived data - for instance a username/email SHA512 hash.
 */
+ (void)setIdentity:(NSString * __nonnull)identity;

/**
 Resets the state of the SDK installation.
 @discussion This call will wipe out all current internal storage, reset the segments, states and user identity. Resulting state will be equal to SDK being configured on the device for the first time. This call will block until completed. Calls to event action objects or Permutive static methods in other threads have undefined effects. As such, after this call - Application will have to set identity and other properties again. You do not have to set API Key or ProjectID again.
 */
+ (void)reset;

/**
 internal use only
 */
+ (void)setBackendType:(PermutiveInterfaceBackendType)backendType;

@end


#endif /* PermutiveAPI_h */
