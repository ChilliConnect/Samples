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
 A block describing a successful callback from setPlayerData.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCSetPlayerDataResponseCallback)(CCSetPlayerDataRequest *request, CCSetPlayerDataResponse *response);	
		
/*!
 A block describing a successful callback from getPlayerData.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetPlayerDataResponseCallback)(CCGetPlayerDataRequest *request, CCGetPlayerDataResponse *response);	
		
/*!
 A block describing a successful callback from getPlayerDataForChilliConnectIds.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetPlayerDataForChilliConnectIdsResponseCallback)(CCGetPlayerDataForChilliConnectIdsRequest *request, CCGetPlayerDataForChilliConnectIdsResponse *response);	
		
/*!
 A block describing a successful callback from getPlayerDataForFacebookFriends.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetPlayerDataForFacebookFriendsResponseCallback)(CCGetPlayerDataForFacebookFriendsRequest *request, CCGetPlayerDataForFacebookFriendsResponse *response);	
		
/*!
 A block describing a successful callback from deletePlayerData.
 
 @param request The original request.
 */
typedef void (^CCDeletePlayerDataResponseCallback)(CCDeletePlayerDataRequest *request);	
		
/*!
 A block describing a successful callback from addCollectionObject.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCAddCollectionObjectResponseCallback)(CCAddCollectionObjectRequest *request, CCAddCollectionObjectResponse *response);	
		
/*!
 A block describing a successful callback from updateCollectionObject.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCUpdateCollectionObjectResponseCallback)(CCUpdateCollectionObjectRequest *request, CCUpdateCollectionObjectResponse *response);	
		
/*!
 A block describing a successful callback from deleteCollectionObject.
 
 @param request The original request.
 */
typedef void (^CCDeleteCollectionObjectResponseCallback)(CCDeleteCollectionObjectRequest *request);	
		
/*!
 A block describing a successful callback from queryCollection.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCQueryCollectionResponseCallback)(CCQueryCollectionRequest *request, CCQueryCollectionResponse *response);	
	
/*!
 A block describing an error callback from setPlayerData.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCSetPlayerDataErrorCallback)(CCSetPlayerDataRequest *request, CCSetPlayerDataError *error);
	
/*!
 A block describing an error callback from getPlayerData.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetPlayerDataErrorCallback)(CCGetPlayerDataRequest *request, CCGetPlayerDataError *error);
	
/*!
 A block describing an error callback from getPlayerDataForChilliConnectIds.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetPlayerDataForChilliConnectIdsErrorCallback)(CCGetPlayerDataForChilliConnectIdsRequest *request, CCGetPlayerDataForChilliConnectIdsError *error);
	
/*!
 A block describing an error callback from getPlayerDataForFacebookFriends.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetPlayerDataForFacebookFriendsErrorCallback)(CCGetPlayerDataForFacebookFriendsRequest *request, CCGetPlayerDataForFacebookFriendsError *error);
	
/*!
 A block describing an error callback from deletePlayerData.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCDeletePlayerDataErrorCallback)(CCDeletePlayerDataRequest *request, CCDeletePlayerDataError *error);
	
/*!
 A block describing an error callback from addCollectionObject.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCAddCollectionObjectErrorCallback)(CCAddCollectionObjectRequest *request, CCAddCollectionObjectError *error);
	
/*!
 A block describing an error callback from updateCollectionObject.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCUpdateCollectionObjectErrorCallback)(CCUpdateCollectionObjectRequest *request, CCUpdateCollectionObjectError *error);
	
/*!
 A block describing an error callback from deleteCollectionObject.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCDeleteCollectionObjectErrorCallback)(CCDeleteCollectionObjectRequest *request, CCDeleteCollectionObjectError *error);
	
/*!
 A block describing an error callback from queryCollection.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCQueryCollectionErrorCallback)(CCQueryCollectionRequest *request, CCQueryCollectionError *error);

/*!
 The ChilliConnect Cloud Data module. Provides the means to store custom data
 against Player Accounts for retrieval.

 This is thread-safe.
 */
@interface CCCloudData : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCCloudData
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)cloudDataWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

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

 @return The initialised CCCloudData.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Sets the value of a specified Custom Data Key for the currently logged in player.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)setPlayerDataWithDesc:(CCSetPlayerDataRequestDesc *)desc successCallback:(CCSetPlayerDataResponseCallback)successCallback errorCallback:(CCSetPlayerDataErrorCallback)errorCallback;		

/*!
 Returns the value of a set of specified Custom Data Keys for the currently logged
 in player.
 
 @param keys The Custom Data Keys for which to retrieve the values of.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getPlayerDataWithKeys:(NSArray *)keys successCallback:(CCGetPlayerDataResponseCallback)successCallback errorCallback:(CCGetPlayerDataErrorCallback)errorCallback;

/*!
 Returns the value of a specified Custom Data Key for a list of provided
 ChilliConnectIDs.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getPlayerDataForChilliConnectIdsWithDesc:(CCGetPlayerDataForChilliConnectIdsRequestDesc *)desc successCallback:(CCGetPlayerDataForChilliConnectIdsResponseCallback)successCallback errorCallback:(CCGetPlayerDataForChilliConnectIdsErrorCallback)errorCallback;		

/*!
 Returns the value of a specified Custom Data Key for all of a players Facebook
 Friends.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getPlayerDataForFacebookFriendsWithDesc:(CCGetPlayerDataForFacebookFriendsRequestDesc *)desc successCallback:(CCGetPlayerDataForFacebookFriendsResponseCallback)successCallback errorCallback:(CCGetPlayerDataForFacebookFriendsErrorCallback)errorCallback;		

/*!
 Deletes a specified Custom Data Key.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)deletePlayerDataWithDesc:(CCDeletePlayerDataRequestDesc *)desc successCallback:(CCDeletePlayerDataResponseCallback)successCallback errorCallback:(CCDeletePlayerDataErrorCallback)errorCallback;		

/*!
 Adds an object to the specified collection.
 
 @param key The Collection Key.
 @param value The data to be saved. When serialised the maximum size is 400kb.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)addCollectionObjectWithKey:(NSString *)key value:(SCMultiTypeValue *)value successCallback:(CCAddCollectionObjectResponseCallback)successCallback errorCallback:(CCAddCollectionObjectErrorCallback)errorCallback;

/*!
 Updates an object in the specified collection.
 
 @param key The Collection Key.
 @param objectId Identifier for the Collection object to be updated.
 @param value The data to be saved. When serialised the maximum size is 400kb.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)updateCollectionObjectWithKey:(NSString *)key objectId:(NSString *)objectId value:(SCMultiTypeValue *)value successCallback:(CCUpdateCollectionObjectResponseCallback)successCallback errorCallback:(CCUpdateCollectionObjectErrorCallback)errorCallback;

/*!
 Deletes an object in the specified collection.
 
 @param key The Collection Key.
 @param objectId Identifier for the Collection object to be deleted.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)deleteCollectionObjectWithKey:(NSString *)key objectId:(NSString *)objectId successCallback:(CCDeleteCollectionObjectResponseCallback)successCallback errorCallback:(CCDeleteCollectionObjectErrorCallback)errorCallback;

/*!
 Returns objects that satisfy the query for a specified collection.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)queryCollectionWithDesc:(CCQueryCollectionRequestDesc *)desc successCallback:(CCQueryCollectionResponseCallback)successCallback errorCallback:(CCQueryCollectionErrorCallback)errorCallback;		

@end

NS_ASSUME_NONNULL_END
