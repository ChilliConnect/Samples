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
 A block describing a successful callback from createPlayer.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCCreatePlayerResponseCallback)(CCCreatePlayerRequest *request, CCCreatePlayerResponse *response);	
		
/*!
 A block describing a successful callback from logInUsingChilliConnect.
 
 @param request The original request.
 */
typedef void (^CCLogInUsingChilliConnectResponseCallback)(CCLogInUsingChilliConnectRequest *request);	
		
/*!
 A block describing a successful callback from logInUsingEmail.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCLogInUsingEmailResponseCallback)(CCLogInUsingEmailRequest *request, CCLogInUsingEmailResponse *response);	
		
/*!
 A block describing a successful callback from logInUsingFacebook.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCLogInUsingFacebookResponseCallback)(CCLogInUsingFacebookRequest *request, CCLogInUsingFacebookResponse *response);	
		
/*!
 A block describing a successful callback from logInUsingUserName.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCLogInUsingUserNameResponseCallback)(CCLogInUsingUserNameRequest *request, CCLogInUsingUserNameResponse *response);	
		
/*!
 A block describing a successful callback from setPlayerDetails.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCSetPlayerDetailsResponseCallback)(CCSetPlayerDetailsRequest *request, CCSetPlayerDetailsResponse *response);	
		
/*!
 A block describing a successful callback from getPlayerDetails.
 
 @param response The response from the server.
 */
typedef void (^CCGetPlayerDetailsResponseCallback)(CCGetPlayerDetailsResponse *response);	
		
/*!
 A block describing a successful callback from linkFacebookAccount.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCLinkFacebookAccountResponseCallback)(CCLinkFacebookAccountRequest *request, CCLinkFacebookAccountResponse *response);	
		
/*!
 A block describing a successful callback from verifyFacebookToken.
 
 @param response The response from the server.
 */
typedef void (^CCVerifyFacebookTokenResponseCallback)(CCVerifyFacebookTokenResponse *response);	
		
/*!
 A block describing a successful callback from lookupFacebookPlayers.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCLookupFacebookPlayersResponseCallback)(CCLookupFacebookPlayersRequest *request, CCLookupFacebookPlayersResponse *response);	
		
/*!
 A block describing a successful callback from lookupUserNames.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCLookupUserNamesResponseCallback)(CCLookupUserNamesRequest *request, CCLookupUserNamesResponse *response);	
		
/*!
 A block describing a successful callback from getFacebookFriends.
 
 @param response The response from the server.
 */
typedef void (^CCGetFacebookFriendsResponseCallback)(CCGetFacebookFriendsResponse *response);	
		
/*!
 A block describing a successful callback from unlinkFacebookAccount.
 
 @param response The response from the server.
 */
typedef void (^CCUnlinkFacebookAccountResponseCallback)(CCUnlinkFacebookAccountResponse *response);	
	
/*!
 A block describing an error callback from createPlayer.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCCreatePlayerErrorCallback)(CCCreatePlayerRequest *request, CCCreatePlayerError *error);
	
/*!
 A block describing an error callback from logInUsingChilliConnect.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCLogInUsingChilliConnectErrorCallback)(CCLogInUsingChilliConnectRequest *request, CCLogInUsingChilliConnectError *error);
	
/*!
 A block describing an error callback from logInUsingEmail.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCLogInUsingEmailErrorCallback)(CCLogInUsingEmailRequest *request, CCLogInUsingEmailError *error);
	
/*!
 A block describing an error callback from logInUsingFacebook.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCLogInUsingFacebookErrorCallback)(CCLogInUsingFacebookRequest *request, CCLogInUsingFacebookError *error);
	
/*!
 A block describing an error callback from logInUsingUserName.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCLogInUsingUserNameErrorCallback)(CCLogInUsingUserNameRequest *request, CCLogInUsingUserNameError *error);
	
/*!
 A block describing an error callback from setPlayerDetails.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCSetPlayerDetailsErrorCallback)(CCSetPlayerDetailsRequest *request, CCSetPlayerDetailsError *error);
	
/*!
 A block describing an error callback from getPlayerDetails.
 
 @param error The error that ocurred.
 */
typedef void (^CCGetPlayerDetailsErrorCallback)(CCGetPlayerDetailsError *error);
	
/*!
 A block describing an error callback from linkFacebookAccount.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCLinkFacebookAccountErrorCallback)(CCLinkFacebookAccountRequest *request, CCLinkFacebookAccountError *error);
	
/*!
 A block describing an error callback from verifyFacebookToken.
 
 @param error The error that ocurred.
 */
typedef void (^CCVerifyFacebookTokenErrorCallback)(CCVerifyFacebookTokenError *error);
	
/*!
 A block describing an error callback from lookupFacebookPlayers.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCLookupFacebookPlayersErrorCallback)(CCLookupFacebookPlayersRequest *request, CCLookupFacebookPlayersError *error);
	
/*!
 A block describing an error callback from lookupUserNames.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCLookupUserNamesErrorCallback)(CCLookupUserNamesRequest *request, CCLookupUserNamesError *error);
	
/*!
 A block describing an error callback from getFacebookFriends.
 
 @param error The error that ocurred.
 */
typedef void (^CCGetFacebookFriendsErrorCallback)(CCGetFacebookFriendsError *error);
	
/*!
 A block describing an error callback from unlinkFacebookAccount.
 
 @param error The error that ocurred.
 */
typedef void (^CCUnlinkFacebookAccountErrorCallback)(CCUnlinkFacebookAccountError *error);

/*!
 The ChilliConnect Player Accounts module. Provides the means to create new
 players, log in to existing accounts and modify account data.

 This is thread-safe.
 */
@interface CCPlayerAccounts : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCPlayerAccounts
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)playerAccountsWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

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

 @return The initialised CCPlayerAccounts.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Creates a new, anonymous ChilliConnect player account for a specific game.
 UserName, DisplayName, Email and Password details can be provided but are not
 required. Will return a ChilliConnectID and ChilliConnectSecret that uniquely
 identifies the newly created player. These details can be used to login to the
 players account via the LogInUsingChilliConnect method.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)createPlayerWithDesc:(CCCreatePlayerRequestDesc *)desc successCallback:(CCCreatePlayerResponseCallback)successCallback errorCallback:(CCCreatePlayerErrorCallback)errorCallback;		

/*!
 Login to the system using a ChilliConnectID and a ChilliConnectSecret. Returns an
 ConnectAccessToken that is tied to the player and should be used to authenticate
 on subsequent requests.
 
 @param chilliConnectId The player's ChilliConnectID.
 @param chilliConnectSecret The player's ChilliConnectSecret.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)logInUsingChilliConnect:(NSString *)chilliConnectId chilliConnectSecret:(NSString *)chilliConnectSecret successCallback:(CCLogInUsingChilliConnectResponseCallback)successCallback errorCallback:(CCLogInUsingChilliConnectErrorCallback)errorCallback;        

/*!
 Login to the sytem using an Email and Password. Returns an ConnectAccessToken
 that is tied to the player and should be used to authenticate on subsequent
 requests. Also returns the ChilliConnectID and ChilliConnectSecret of the logged
 in player that can be used to generate new ConnectAccessTokens via the
 LogInUsingChilliConnect method without requiring the player to explicitly
 reauthenticate.
 
 @param email The player's Email.
 @param password The player's Password.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)logInUsingEmail:(NSString *)email password:(NSString *)password successCallback:(CCLogInUsingEmailResponseCallback)successCallback errorCallback:(CCLogInUsingEmailErrorCallback)errorCallback;        

/*!
 Login to the sytem using a FacebookAccessToken. Returns an ConnectAccessToken
 that is tied to the player and should be used to authenticate on subsequent
 requests. Also returns the ChilliConnectID and ChilliConnectSecret of the logged
 in player that can be used to generate new ConnectAccessTokens via the
 LogInUsingChilliConnect method without requiring the player to explicitly
 reauthenticate.
 
 @param facebookAccessToken Access Token provided from the Facebook API.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)logInUsingFacebook:(NSString *)facebookAccessToken successCallback:(CCLogInUsingFacebookResponseCallback)successCallback errorCallback:(CCLogInUsingFacebookErrorCallback)errorCallback;        

/*!
 Login to the sytem using an UserName and Password. Returns an ConnectAccessToken
 that is tied to the player and should be used to authenticate on subsequent
 requests. Also returns the ChilliConnectID and ChilliConnectSecret of the logged
 in player that can be used to generate new ConnectAccessTokens via the
 LogInUsingChilliConnect method without requiring the player to explicitly
 reauthenticate.
 
 @param userName The player's Username.
 @param password The player's Password.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)logInUsingUserName:(NSString *)userName password:(NSString *)password successCallback:(CCLogInUsingUserNameResponseCallback)successCallback errorCallback:(CCLogInUsingUserNameErrorCallback)errorCallback;        

/*!
 Updates the details of the currently logged in Player.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)setPlayerDetailsWithDesc:(CCSetPlayerDetailsRequestDesc *)desc successCallback:(CCSetPlayerDetailsResponseCallback)successCallback errorCallback:(CCSetPlayerDetailsErrorCallback)errorCallback;		

/*!
 Returns the details of the currently logged in Player.
 
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getPlayerDetailsWithSuccessCallback:(CCGetPlayerDetailsResponseCallback)successCallback errorCallback:(CCGetPlayerDetailsErrorCallback)errorCallback;

/*!
 Associate a player account with a Facebook account. Each player can only be
 associated with a single Facebook account and a Facebook account can only be
 associated with a single player per game. If the player is already associated
 with a Facebook account an error will be returned, unless the Replace flag is
 provided, in which case the association will be updated. If the Facebook account
 is already associated with another player within this game, an error will be
 returned along with the ChilliConnectID and ChilliConnectSecret for the
 associated player within the data parameter of the response body. If the Update
 flag is provided, the existing association will be removed and Facebook account
 associated with the current ChilliConnect account.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)linkFacebookAccountWithDesc:(CCLinkFacebookAccountRequestDesc *)desc successCallback:(CCLinkFacebookAccountResponseCallback)successCallback errorCallback:(CCLinkFacebookAccountErrorCallback)errorCallback;		

/*!
 Retrieve a boolean indicating if a player's Facebook Access Token is Valid or
 not.
 
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)verifyFacebookTokenWithSuccessCallback:(CCVerifyFacebookTokenResponseCallback)successCallback errorCallback:(CCVerifyFacebookTokenErrorCallback)errorCallback;

/*!
 Find the ChilliConnectID's of players associated with provided FacebookID's.
 Returns an array of objects for each FacebookID that was found providing the
 FacebookName, ChilliConnectID, UserName and DisplayName of the associated player.
 
 @param facebookIds An array of FacebookIDs to look up.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)lookupFacebookPlayers:(NSArray *)facebookIds successCallback:(CCLookupFacebookPlayersResponseCallback)successCallback errorCallback:(CCLookupFacebookPlayersErrorCallback)errorCallback;        

/*!
 Find the ChilliConnectID's of players associated with provided UserName's.
 Returns an array of objects for each UserName that was found providing the
 ChilliConnectID, UserName and DisplayName of the associated player.
 
 @param userNames An array of UserNames to look up.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)lookupUserNames:(NSArray *)userNames successCallback:(CCLookupUserNamesResponseCallback)successCallback errorCallback:(CCLookupUserNamesErrorCallback)errorCallback;        

/*!
 Get back a players ChilliConnect registered Facebook friends along with their
 current Facebook profile pictures.
 
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getFacebookFriendsWithSuccessCallback:(CCGetFacebookFriendsResponseCallback)successCallback errorCallback:(CCGetFacebookFriendsErrorCallback)errorCallback;

/*!
 Remove an associate between a player and a Facebook account previously created
 via the LinkFacebookAccount method.
 
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)unlinkFacebookAccountWithSuccessCallback:(CCUnlinkFacebookAccountResponseCallback)successCallback errorCallback:(CCUnlinkFacebookAccountErrorCallback)errorCallback;

@end

NS_ASSUME_NONNULL_END
