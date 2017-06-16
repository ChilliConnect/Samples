//
//  ChilliConnectController.h
//  PlayerMessaging
//
//  Created by Robert Henning on 25/04/2017.
//  Copyright © 2017 Chilli Technologies. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <ChilliConnect/ChilliConnect.h>

/*!
 A block describing a callback success or failure.
 
 @param success If the call was successful.
 @param errorDescription Description if the call failed.
 */
typedef void (^ChilliConnectControllerCallback)(BOOL success, NSString* errorDescription);

/*!
 A block describing a callback success or failure when requesting economy balance.
 
 @param success If the call was successful.
 @param errorDescription Description if the call failed.
 @param balances Array of balances for the current player.
 */
typedef void (^ChilliConnectControllerBalancesCallback)(BOOL success, NSString* errorDescription, NSArray* balances);

/*!
 A block describing a callback success or failure when requesting messages.
 
 @param success If the call was successful.
 @param errorDescription Description if the call failed.
 @param messages Array of messages revieved if call was successful.
 */
typedef void (^ChilliConnectControllerGetMessagesCallback)(BOOL success, NSString* errorDescription, NSArray* messages);

typedef NS_ENUM(NSUInteger, LoginState)
{
    kLoggedOut,
    kLoggedInChilliConnect,
    kLoggedInFacebook
};

@interface ChilliConnectController : NSObject

@property NSString* chilliConnectId;
@property NSString* chilliConnectSecret;
@property NSString* facebookName;

#pragma mark - App lifecycle
/* Called before the app enter the background. */
- (void) prepareToEnterBackground;
/* Called before the app enters the foreground. */
- (void) prepareToEnterForeground;

#pragma mark - Login and User Creation
/* Returns the current login state */
- (LoginState) getLoginState;
/* Login using Facebook. Before we can do this we first need to create a new annoymous Chilli Connect account and link it with a FB account. For this example account creation and linking with FB happens if we this call returns an error. */
- (void) loginUsingFacebook:(ChilliConnectControllerCallback)callback;
/* Links the currently logged in Chilli Connect user to the current Facebook account */
- (void) linkFacebookAccount:(ChilliConnectControllerCallback)callback;
/* Login using saved Chilli Connect credentials */
- (void) login:(ChilliConnectControllerCallback)callback;
/* Creates a new Chilli Connect user */
- (void) createNewUser:(ChilliConnectControllerCallback)callback;

#pragma mark - Credentials
/* Returns if Chilli Connect credentials (id and secret) exist */
- (BOOL) hasCredentials;
/* Delete Chilli Connect user id and secret from the keychain */
- (BOOL) deleteCredentials;

#pragma mark - Facebook
/* Returns the current players Facebook name */
- (NSString*) getMyFacebookName;
/* Returns if an array of Facebook friends has already been retrieved. */
- (BOOL) hasFacebookFriends;
/* Returns the currently cached list for Facebook friends. The users Chilli Connect account must be linked to Facebook and a request the fetch the list of Facebook friends must have returned successfully. */
- (NSArray*) getCachedFacebookFriends;
/* Retrieves a list of Facebook friends via Chilli Connect. The users Chilli Connect account must be linked to Facebook. */
- (void) fetchFacebookFriends:(ChilliConnectControllerCallback)callback;

#pragma mark - Economy
/* Returns the current amount of coins for the current player */
- (void) getCoinsBalances:(ChilliConnectControllerBalancesCallback)callback;

#pragma mark - Cloud messaging
/* Sends a message */
- (void) sendMessageWithScriptKey:(NSString*)scriptKey dictionary:(NSDictionary*)dictionary callback:(ChilliConnectControllerCallback)callback;
/* Get messages */
- (void) getMessages:(ChilliConnectControllerGetMessagesCallback)callback;
/* Redeems reward from a message */
- (void) redeemMessageReward:(NSString*)messageId callback:(ChilliConnectControllerCallback)callback;

@end
