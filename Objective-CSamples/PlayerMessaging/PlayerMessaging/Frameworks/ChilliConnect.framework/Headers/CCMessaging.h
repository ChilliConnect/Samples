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
 A block describing a successful callback from sendMessage.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCSendMessageResponseCallback)(CCSendMessageRequest *request, CCSendMessageResponse *response);	
		
/*!
 A block describing a successful callback from sendMessageFromPlayer.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCSendMessageFromPlayerResponseCallback)(CCSendMessageFromPlayerRequest *request, CCSendMessageFromPlayerResponse *response);	
		
/*!
 A block describing a successful callback from getMessage.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetMessageResponseCallback)(CCGetMessageRequest *request, CCGetMessageResponse *response);	
		
/*!
 A block describing a successful callback from getMessages.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetMessagesResponseCallback)(CCGetMessagesRequest *request, CCGetMessagesResponse *response);	
		
/*!
 A block describing a successful callback from redeemMessageRewards.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCRedeemMessageRewardsResponseCallback)(CCRedeemMessageRewardsRequest *request, CCRedeemMessageRewardsResponse *response);	
		
/*!
 A block describing a successful callback from markMessageAsRead.
 
 @param request The original request.
 */
typedef void (^CCMarkMessageAsReadResponseCallback)(CCMarkMessageAsReadRequest *request);	
		
/*!
 A block describing a successful callback from deleteMessage.
 
 @param request The original request.
 */
typedef void (^CCDeleteMessageResponseCallback)(CCDeleteMessageRequest *request);	
	
/*!
 A block describing an error callback from sendMessage.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCSendMessageErrorCallback)(CCSendMessageRequest *request, CCSendMessageError *error);
	
/*!
 A block describing an error callback from sendMessageFromPlayer.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCSendMessageFromPlayerErrorCallback)(CCSendMessageFromPlayerRequest *request, CCSendMessageFromPlayerError *error);
	
/*!
 A block describing an error callback from getMessage.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetMessageErrorCallback)(CCGetMessageRequest *request, CCGetMessageError *error);
	
/*!
 A block describing an error callback from getMessages.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetMessagesErrorCallback)(CCGetMessagesRequest *request, CCGetMessagesError *error);
	
/*!
 A block describing an error callback from redeemMessageRewards.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCRedeemMessageRewardsErrorCallback)(CCRedeemMessageRewardsRequest *request, CCRedeemMessageRewardsError *error);
	
/*!
 A block describing an error callback from markMessageAsRead.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCMarkMessageAsReadErrorCallback)(CCMarkMessageAsReadRequest *request, CCMarkMessageAsReadError *error);
	
/*!
 A block describing an error callback from deleteMessage.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCDeleteMessageErrorCallback)(CCDeleteMessageRequest *request, CCDeleteMessageError *error);

/*!
 The ChilliConnect Messaging module. Enables sending of messages, and gifting
 economy items to players.

 This is thread-safe.
 */
@interface CCMessaging : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCMessaging
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)messagingWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

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

 @return The initialised CCMessaging.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Sends a message to a player. This call allows the From parameter to be set -
 enabling sending from any Player, or not set - enabling sending a system message.
 To send a message from the currently logged-in Player see SendMessageFromPlayer.
 A message can generate, or transfer from the sender, Currency and Inventory
 items. Direct access to this method from the SDKs is disabled by default and must
 be enabled from the ChilliConnect dashboard. Note: At least one of; Title, Text,
 Data, or Rewards is required.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)sendMessageWithDesc:(CCSendMessageRequestDesc *)desc successCallback:(CCSendMessageResponseCallback)successCallback errorCallback:(CCSendMessageErrorCallback)errorCallback;		

/*!
 Sends a message to a player from the currently logged-in Player. A message can
 transfer Currency and Inventory items between players. Direct access to this
 method from the SDKs is disabled by default and must be enabled from the
 ChilliConnect dashboard. Note: At least one of; Title, Text, Data, or Rewards is
 required.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)sendMessageFromPlayerWithDesc:(CCSendMessageFromPlayerRequestDesc *)desc successCallback:(CCSendMessageFromPlayerResponseCallback)successCallback errorCallback:(CCSendMessageFromPlayerErrorCallback)errorCallback;		

/*!
 Gets a message for a Player.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getMessageWithDesc:(CCGetMessageRequestDesc *)desc successCallback:(CCGetMessageResponseCallback)successCallback errorCallback:(CCGetMessageErrorCallback)errorCallback;		

/*!
 Lists the messages for Player.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getMessagesWithDesc:(CCGetMessagesRequestDesc *)desc successCallback:(CCGetMessagesResponseCallback)successCallback errorCallback:(CCGetMessagesErrorCallback)errorCallback;		

/*!
 Redeem the rewards contained in a message. Direct access to this method from the
 SDKs is disabled by default and must be enabled from the ChilliConnect dashboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)redeemMessageRewardsWithDesc:(CCRedeemMessageRewardsRequestDesc *)desc successCallback:(CCRedeemMessageRewardsResponseCallback)successCallback errorCallback:(CCRedeemMessageRewardsErrorCallback)errorCallback;		

/*!
 Marks a Player's message as read. Direct access to this method from the SDKs is
 disabled by default and must be enabled from the ChilliConnect dashboard.
 
 @param messageId An identifier for the message.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)markMessageAsReadWithMessageId:(NSString *)messageId successCallback:(CCMarkMessageAsReadResponseCallback)successCallback errorCallback:(CCMarkMessageAsReadErrorCallback)errorCallback;

/*!
 Deletes a message. Direct access to this method from the SDKs is disabled by
 default and must be enabled from the ChilliConnect dashboard.
 
 @param messageId An identifier for the message.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)deleteMessageWithMessageId:(NSString *)messageId successCallback:(CCDeleteMessageResponseCallback)successCallback errorCallback:(CCDeleteMessageErrorCallback)errorCallback;

@end

NS_ASSUME_NONNULL_END
