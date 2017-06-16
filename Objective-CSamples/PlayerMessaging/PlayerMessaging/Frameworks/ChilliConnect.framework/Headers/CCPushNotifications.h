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
 A block describing a successful callback from registerToken.
 
 @param request The original request.
 */
typedef void (^CCRegisterTokenResponseCallback)(CCRegisterTokenRequest *request);	
		
/*!
 A block describing a successful callback from unregisterToken.
 
 @param request The original request.
 */
typedef void (^CCUnregisterTokenResponseCallback)(CCUnregisterTokenRequest *request);	
		
/*!
 A block describing a successful callback from setPushGroups.
 
 @param request The original request.
 */
typedef void (^CCSetPushGroupsResponseCallback)(CCSetPushGroupsRequest *request);	
	
/*!
 A block describing an error callback from registerToken.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCRegisterTokenErrorCallback)(CCRegisterTokenRequest *request, CCRegisterTokenError *error);
	
/*!
 A block describing an error callback from unregisterToken.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCUnregisterTokenErrorCallback)(CCUnregisterTokenRequest *request, CCUnregisterTokenError *error);
	
/*!
 A block describing an error callback from setPushGroups.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCSetPushGroupsErrorCallback)(CCSetPushGroupsRequest *request, CCSetPushGroupsError *error);

/*!
 The ChillConnect Push Notifications module. Provides the means to send push
 messages to players using Amazon Device Messaging, Apple Push Notification
 Service and Google Cloud Messaging.

 This is thread-safe.
 */
@interface CCPushNotifications : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCPushNotifications
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)pushNotificationsWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

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

 @return The initialised CCPushNotifications.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Registers a Device Push Token for the currently logged in ChilliConnect player
 for a particular Push Notification Service. On success, returns an empty JSON
 object.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)registerTokenWithDesc:(CCRegisterTokenRequestDesc *)desc successCallback:(CCRegisterTokenResponseCallback)successCallback errorCallback:(CCRegisterTokenErrorCallback)errorCallback;		

/*!
 UnRegister a Push Token previously registered. If the Push Token has not been
 registered with the currently logged in Player, the request is ignored.
 
 @param service The push notification service the device token belongs to. Must be one of APNS,
        GCM or ADM.	
 @param deviceToken The Push Token. Note: for APNS (iOS) base64 encode the NSData object to form this
        string.	
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)unregisterTokenWithService:(NSString *)service deviceToken:(NSString *)deviceToken successCallback:(CCUnregisterTokenResponseCallback)successCallback errorCallback:(CCUnregisterTokenErrorCallback)errorCallback;

/*!
 Set the Push Groups for the current ChilliConnect Player. Push Groups allow Mass
 Push Notifications to be targeted at a specific subset of Players. Setting a
 Players Push Groups will overwrite any previously set Push Groups.
 
 @param groups A list of Push Groups that the player belongs to, up to a maximum of 10.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)setPushGroups:(NSArray *)groups successCallback:(CCSetPushGroupsResponseCallback)successCallback errorCallback:(CCSetPushGroupsErrorCallback)errorCallback;        

@end

NS_ASSUME_NONNULL_END
