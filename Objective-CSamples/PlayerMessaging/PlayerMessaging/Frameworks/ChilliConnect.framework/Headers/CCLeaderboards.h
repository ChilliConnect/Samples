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
 A block describing a successful callback from addScore.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCAddScoreResponseCallback)(CCAddScoreRequest *request, CCAddScoreResponse *response);	
		
/*!
 A block describing a successful callback from getPlayerScore.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetPlayerScoreResponseCallback)(CCGetPlayerScoreRequest *request, CCGetPlayerScoreResponse *response);	
		
/*!
 A block describing a successful callback from getScoresAroundPlayer.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetScoresAroundPlayerResponseCallback)(CCGetScoresAroundPlayerRequest *request, CCGetScoresAroundPlayerResponse *response);	
		
/*!
 A block describing a successful callback from getScoresForChilliConnectIds.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetScoresForChilliConnectIdsResponseCallback)(CCGetScoresForChilliConnectIdsRequest *request, CCGetScoresForChilliConnectIdsResponse *response);	
		
/*!
 A block describing a successful callback from getScoresForFacebookFriends.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetScoresForFacebookFriendsResponseCallback)(CCGetScoresForFacebookFriendsRequest *request, CCGetScoresForFacebookFriendsResponse *response);	
		
/*!
 A block describing a successful callback from getScores.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetScoresResponseCallback)(CCGetScoresRequest *request, CCGetScoresResponse *response);	
	
/*!
 A block describing an error callback from addScore.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCAddScoreErrorCallback)(CCAddScoreRequest *request, CCAddScoreError *error);
	
/*!
 A block describing an error callback from getPlayerScore.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetPlayerScoreErrorCallback)(CCGetPlayerScoreRequest *request, CCGetPlayerScoreError *error);
	
/*!
 A block describing an error callback from getScoresAroundPlayer.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetScoresAroundPlayerErrorCallback)(CCGetScoresAroundPlayerRequest *request, CCGetScoresAroundPlayerError *error);
	
/*!
 A block describing an error callback from getScoresForChilliConnectIds.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetScoresForChilliConnectIdsErrorCallback)(CCGetScoresForChilliConnectIdsRequest *request, CCGetScoresForChilliConnectIdsError *error);
	
/*!
 A block describing an error callback from getScoresForFacebookFriends.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetScoresForFacebookFriendsErrorCallback)(CCGetScoresForFacebookFriendsRequest *request, CCGetScoresForFacebookFriendsError *error);
	
/*!
 A block describing an error callback from getScores.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetScoresErrorCallback)(CCGetScoresRequest *request, CCGetScoresError *error);

/*!
 The ChillConnect Leaderboards module. Provides the means to add to and query from
 leaderboards.

 This is thread-safe.
 */
@interface CCLeaderboards : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCLeaderboards
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)leaderboardsWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

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

 @return The initialised CCLeaderboards.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Adds a score to a leaderboard for the currently logged in player.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)addScoreWithDesc:(CCAddScoreRequestDesc *)desc successCallback:(CCAddScoreResponseCallback)successCallback errorCallback:(CCAddScoreErrorCallback)errorCallback;		

/*!
 Retrieve the currently logged in player's score and rank for a given leaderboard.
 
 @param key The Key that identifies the leaderboard.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getPlayerScoreWithKey:(NSString *)key successCallback:(CCGetPlayerScoreResponseCallback)successCallback errorCallback:(CCGetPlayerScoreErrorCallback)errorCallback;

/*!
 Retrieve paged scores for a given leaderboard around the currently logged in
 player. The response is the same as the GetScores method, but with additional
 fields populated at the top level of the response to indicate the player's
 position in the global leaderboard and also their index in the returned Scores
 array.
 
 @param key The Key that identifies the leaderboard.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getScoresAroundPlayerWithKey:(NSString *)key successCallback:(CCGetScoresAroundPlayerResponseCallback)successCallback errorCallback:(CCGetScoresAroundPlayerErrorCallback)errorCallback;

/*!
 Retrieve scores and ranks for a provided list of ChilliConnectIDs. For each
 player that has a score in the provided leaderboard, their global rank will be
 returned along with their local ranking within the returned scores.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getScoresForChilliConnectIdsWithDesc:(CCGetScoresForChilliConnectIdsRequestDesc *)desc successCallback:(CCGetScoresForChilliConnectIdsResponseCallback)successCallback errorCallback:(CCGetScoresForChilliConnectIdsErrorCallback)errorCallback;		

/*!
 Retrieve scores for each of the currently logged in player's facebook friends.
 Friends are retrieved from Facebook using the AccessToken provided on the players
 last succesful login to Facebook. Returns an array of objects for each player
 with a score posted on the provided leaderboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getScoresForFacebookFriendsWithDesc:(CCGetScoresForFacebookFriendsRequestDesc *)desc successCallback:(CCGetScoresForFacebookFriendsResponseCallback)successCallback errorCallback:(CCGetScoresForFacebookFriendsErrorCallback)errorCallback;		

/*!
 Retrieve paged scores for a given leaderboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getScoresWithDesc:(CCGetScoresRequestDesc *)desc successCallback:(CCGetScoresResponseCallback)successCallback errorCallback:(CCGetScoresErrorCallback)errorCallback;		

@end

NS_ASSUME_NONNULL_END
