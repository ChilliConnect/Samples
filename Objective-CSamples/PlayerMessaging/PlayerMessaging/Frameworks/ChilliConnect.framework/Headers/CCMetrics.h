//
//  This file was auto-generated using the ChilliConnect SDK Generator.
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Ltd
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//

@import Foundation;

#import "ForwardDeclarations.h"

NS_ASSUME_NONNULL_BEGIN
		
/*!
 A block describing a successful callback from generateUuid.
 
 @param response The response from the server.
 */
typedef void (^CCGenerateUuidResponseCallback)(CCGenerateUuidResponse *response);	
		
/*!
 A block describing a successful callback from startSession.
 
 @param request The original request.
 */
typedef void (^CCStartSessionResponseCallback)(CCStartSessionRequest *request);	
		
/*!
 A block describing a successful callback from addEvent.
 
 @param request The original request.
 */
typedef void (^CCAddEventResponseCallback)(CCAddEventRequest *request);	
		
/*!
 A block describing a successful callback from addEvents.
 
 @param request The original request.
 */
typedef void (^CCAddEventsResponseCallback)(CCAddEventsRequest *request);	
		
/*!
 A block describing a successful callback from addIapEvent.
 
 @param request The original request.
 */
typedef void (^CCAddIapEventResponseCallback)(CCAddIapEventRequest *request);	
		
/*!
 A block describing a successful callback from endSession.
 */
typedef void (^CCEndSessionResponseCallback)();	
	
/*!
 A block describing an error callback from generateUuid.
 
 @param error The error that ocurred.
 */
typedef void (^CCGenerateUuidErrorCallback)(CCGenerateUuidError *error);
	
/*!
 A block describing an error callback from startSession.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCStartSessionErrorCallback)(CCStartSessionRequest *request, CCStartSessionError *error);
	
/*!
 A block describing an error callback from addEvent.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCAddEventErrorCallback)(CCAddEventRequest *request, CCAddEventError *error);
	
/*!
 A block describing an error callback from addEvents.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCAddEventsErrorCallback)(CCAddEventsRequest *request, CCAddEventsError *error);
	
/*!
 A block describing an error callback from addIapEvent.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCAddIapEventErrorCallback)(CCAddIapEventRequest *request, CCAddIapEventError *error);
	
/*!
 A block describing an error callback from endSession.
 
 @param error The error that ocurred.
 */
typedef void (^CCEndSessionErrorCallback)(CCEndSessionError *error);

/*!
 The ChillConnect Metrics module. This provides the means to log metrics events
 with the server.

 This is thread-safe.
 */
@interface CCMetrics : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCMetrics
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)metricsWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties. with the given logger, task scheduler 
 and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The initialised CCMetrics.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Generates a universally unique identifier (UUID) that can be persisted locally on
 a device and used to identify a player on subsequent calls to StartSession.
 
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)generateUuidWithSuccessCallback:(CCGenerateUuidResponseCallback)successCallback errorCallback:(CCGenerateUuidErrorCallback)errorCallback;

/*!
 Registers the start of a new session with the metrics platforms. Sessions are
 used when calculating DAU, WAU, MAU and Retention Metrics. On successfully
 starting a session, a Metrics-Access-Token value will be returned. This should
 then be used on subsequent calls to register custom events within the session, as
 well as closing the session.
 
 @param userId ID that uniquely identifies this player. This ID should not clash with any other
        player and should persist across Sessions.	
 @param appVersion The version of your game from which the Session was started.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)startSessionWithUserId:(NSString *)userId appVersion:(NSString *)appVersion successCallback:(CCStartSessionResponseCallback)successCallback errorCallback:(CCStartSessionErrorCallback)errorCallback;

/*!
 Records a custom metrics event that occured within the context of a session. The
 behaviour of this method is identical to AddEvents method, with the exception
 that the request format accepts a single Event json object rather than an array.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)addEventWithDesc:(CCAddEventRequestDesc *)desc successCallback:(CCAddEventResponseCallback)successCallback errorCallback:(CCAddEventErrorCallback)errorCallback;		

/*!
 Records one or more custom metrics event that occurred within the context of a
 session. The posted body to this method should be a json encoded array of
 individual custom events. Events are validated against the custom event
 definitions created within the ChilliConnect dashboard. If any events are
 invalid, the request will not be processed and an InvalidRequest response
 returned. The data property of the response will contain a JSON structure that
 indicates the number of events successfully processed as well as the number
 failed in addition to specific error messages for each failed event as well as
 that events index within the original upload. If the provided events are valid,
 an empty json object will be returned.
 
 @param events An array of events.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)addEvents:(NSArray *)events successCallback:(CCAddEventsResponseCallback)successCallback errorCallback:(CCAddEventsErrorCallback)errorCallback;        

/*!
 Records a successfully completed IAP transaction.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)addIapEventWithDesc:(CCAddIapEventRequestDesc *)desc successCallback:(CCAddIapEventResponseCallback)successCallback errorCallback:(CCAddIapEventErrorCallback)errorCallback;		

/*!
 Closes a session previously opened with a call to StartSession. On successful
 close an empty response with a HTTP code of 200 is returned. No request body is
 expected.
 
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)endSessionWithSuccessCallback:(CCEndSessionResponseCallback)successCallback errorCallback:(CCEndSessionErrorCallback)errorCallback;

@end

NS_ASSUME_NONNULL_END
