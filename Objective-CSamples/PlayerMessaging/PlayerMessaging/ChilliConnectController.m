//
//  ChilliConnectController.m
//  PlayerMessaging
//
//  Created by Robert Henning on 25/04/2017.
//  Copyright © 2017 Chilli Technologies. All rights reserved.
//

#import "ChilliConnectController.h"

#import "SAMKeychain.h"

#import <ChilliConnect/ChilliConnect.h>

#import <FBSDKCoreKit/FBSDKCoreKit.h>
#import <FBSDKLoginKit/FBSDKLoginKit.h>

@interface ChilliConnectController()

@property CCChilliConnectSdk* chilliConnect;
@property LoginState loginState;
@property CCGetFacebookFriendsResponse* fbFriendsResponse;

@end

@implementation ChilliConnectController

static NSString* const k_ChilliConnectIDKey = @"id";
static NSString* const k_ChilliConnectSecretKey = @"secret";
static NSString* const k_ChilliConnectKeychainService = @"ChilliConnect";
static NSString* const k_ChilliConnectKeychainAccount = @"User";

- (id) init
{
    self = [super init];
    return self;
}

#pragma mark - App lifecycle

/* Called before the app enter the background. */
- (void) prepareToEnterBackground
{
    [self saveCredentials];
}

/* Called before the app enters the foreground. */
- (void) prepareToEnterForeground
{
    if(!self.chilliConnect)
    {
        self.chilliConnect = [CCChilliConnectSdk chilliConnectSdkWithAppToken:@"<INSERT GAME TOKEN HERE>" verboseLogging:NO];
    }
    
    [self loadCredentials];
}

#pragma mark - Login and User Creation

/* Returns the current login state */
- (LoginState) getLoginState
{
    return self.loginState;
}

/* Login using Facebook. Before we can do this we first need to create a new annoymous Chilli Connect account and link it with a FB account. For this example account creation and linking with FB happens if we this call returns an error. */
- (void) loginUsingFacebook:(ChilliConnectControllerCallback)callback
{
    if(self.loginState == kLoggedInFacebook)
    {
        NSLog(@"Already logged in.");
        return;
    }
    
    CCLogInUsingFacebookResponseCallback successCallback = ^(CCLogInUsingFacebookRequest* request, CCLogInUsingFacebookResponse* response)
    {
        if([self hasCredentials])
        {
            if([self compareCredentials:response.chilliConnectId secret:response.chilliConnectSecret])
            {
                self.loginState = kLoggedInFacebook;
                self.facebookName = response.facebookName;
                callback(YES, nil);
            }
            else
            {
                callback(NO, @"Facebook login succeeded but Chilli Connect credentials to not match!");
            }
        }
        else
        {
            [self setCredentials:response.chilliConnectId secret:response.chilliConnectSecret];
            self.loginState = kLoggedInFacebook;
            self.facebookName = response.facebookName;
            callback(YES, nil);
        }
    };
    
    CCLogInUsingFacebookErrorCallback errorCallback = ^(CCLogInUsingFacebookRequest* request, CCLogInUsingFacebookError* error)
    {
        NSLog(@"Facebook login failed with error: %@", error.errorDescription);
        callback(NO, error.errorDescription);
    };
    
    CCPlayerAccounts* playerAccounts = self.chilliConnect.playerAccounts;
    [playerAccounts logInUsingFacebook:[FBSDKAccessToken currentAccessToken].tokenString successCallback:successCallback errorCallback:errorCallback];
}

/* Links the currently logged in Chilli Connect user to the current Facebook account */
- (void) linkFacebookAccount:(ChilliConnectControllerCallback)callback
{
    CCLinkFacebookAccountRequestDesc* description = [CCLinkFacebookAccountRequestDesc alloc];
    description.facebookAccessToken = [FBSDKAccessToken currentAccessToken].tokenString;
    description.replace = [NSNumber numberWithBool:YES];
    description.update = [NSNumber numberWithBool:YES];
    
    CCLinkFacebookAccountResponseCallback successCallback = ^(CCLinkFacebookAccountRequest* request, CCLinkFacebookAccountResponse* response)
    {
        [self saveCredentials];
        self.loginState = kLoggedInFacebook;
        self.facebookName = response.facebookName;
        callback(YES, nil);
    };
    
    CCLinkFacebookAccountErrorCallback errorCallback = ^(CCLinkFacebookAccountRequest* request, CCLinkFacebookAccountError* error)
    {
        callback(NO, error.errorDescription);
    };
    
    CCPlayerAccounts* playerAccounts = self.chilliConnect.playerAccounts;
    [playerAccounts linkFacebookAccountWithDesc:description successCallback:successCallback errorCallback:errorCallback];
}

/* Login using saved Chilli Connect credentials */
- (void) login:(ChilliConnectControllerCallback)callback
{
    CCLogInUsingChilliConnectResponseCallback successCallback = ^(CCLogInUsingChilliConnectRequest* request)
    {
        if([self compareCredentials:request.chilliConnectId secret:request.chilliConnectSecret])
        {
            NSLog(@"Succesfully logged in user %@", self.chilliConnectId);
            self.loginState = kLoggedInChilliConnect;
            callback(YES, nil);
        }
        else
        {
            callback(NO, @"Failed to login as credentials do not match.");
        }
    };
    
    CCLogInUsingChilliConnectErrorCallback errorCallback = ^(CCLogInUsingChilliConnectRequest* request, CCLogInUsingChilliConnectError* error)
    {
        NSLog(@"An error occurred while logging in: %@", error.errorDescription);
        callback(NO, error.errorDescription);
    };
    
    [self loginChilliConnect:successCallback errorCallback:errorCallback];
}

/* Creates a new Chilli Connect user */
- (void) createNewUser:(ChilliConnectControllerCallback)callback
{
    CCCreatePlayerResponseCallback successCallback = ^(CCCreatePlayerRequest* request, CCCreatePlayerResponse* response)
    {
        [self setCredentials:response.chilliConnectId secret:response.chilliConnectSecret];
        NSLog(@"Player created with ChilliConnectId: %@ and Secret: %@", self.chilliConnectId, self.chilliConnectSecret);
        self.loginState = kLoggedOut;
        callback(YES, nil);
    };
    
    CCCreatePlayerErrorCallback errorCallback = ^(CCCreatePlayerRequest* request, CCCreatePlayerError* error)
    {
        NSLog(@"An error occurred while creating a new player: %@", error.errorDescription);
        callback(NO, error.errorDescription);
    };
    
    [self createChilliConnectUser:@"PlayerMessagingUser" successCallback:successCallback errorCallback:errorCallback];
}

#pragma mark - Credentials

/* Returns if Chilli Connect credentials (id and secret) exist */
- (BOOL) hasCredentials
{
    return self.chilliConnectId != nil && self.chilliConnectSecret != nil;
}

/* Delete Chilli Connect user id and secret from the keychain */
- (BOOL) deleteCredentials
{
    if(![self hasCredentials])
    {
        if(![self loadCredentials])
        {
            NSLog(@"Unable to delete Chilli Connect credentials as they don't exist.");
            return NO;
        }
    }
    
    NSError* error = nil;
    if([SAMKeychain deletePasswordForService:k_ChilliConnectKeychainService account:k_ChilliConnectKeychainAccount error:&error])
    {
        NSLog(@"Error deleting Chilli Connect credentials from keychain. %@", [error localizedDescription]);
        return NO;
    }
    
    return YES;
}

#pragma mark - Facebook

/* Returns the current players Facebook name */
- (NSString*) getMyFacebookName
{
    return self.facebookName;
}

/* Returns if an array of Facebook friends has already been retrieved. */
- (BOOL) hasFacebookFriends
{
    return self.fbFriendsResponse != nil && self.fbFriendsResponse.friends != nil && self.fbFriendsResponse.friends.count > 0;
}

/* Returns the currently cached list for Facebook friends. The users Chilli Connect account must be linked to Facebook and a request the fetch the list of Facebook friends must have returned successfully. */
- (NSArray*) getCachedFacebookFriends
{
    if([self hasFacebookFriends])
    {
        return self.fbFriendsResponse.friends;
    }
    
    return nil;
}

/* Retrieves a list of Facebook friends via Chilli Connect. The users Chilli Connect account must be linked to Facebook. */
- (void) fetchFacebookFriends:(ChilliConnectControllerCallback)callback
{
    CCPlayerAccounts* playerAccounts = self.chilliConnect.playerAccounts;

    CCGetFacebookFriendsResponseCallback successCallback = ^(CCGetFacebookFriendsResponse* response)
    {
        unsigned long friendsCount = response.friends != nil ? response.friends.count : 0;
        NSLog(@"Successfully fetched %lu Facebook friends",friendsCount);
        self.fbFriendsResponse = response;
        callback(YES, nil);
    };

    CCGetFacebookFriendsErrorCallback errorCallback = ^(CCGetFacebookFriendsError* error)
    {
        callback(NO, error.errorDescription);
    };

    [playerAccounts getFacebookFriendsWithSuccessCallback:successCallback errorCallback:errorCallback];
}

#pragma mark - Economy

/* Returns the current balance of coins for the current player */
- (void) getCoinsBalances:(ChilliConnectControllerBalancesCallback)callback
{
    NSArray* keys = [NSArray array];
    keys = [keys arrayByAddingObject:@"COINS"];
    
    CCGetCurrencyBalanceRequestDesc* requestDesc = [CCGetCurrencyBalanceRequestDesc getCurrencyBalanceRequestDesc];
    requestDesc.keys = keys;
    
    CCGetCurrencyBalanceResponseCallback successCallback = ^(CCGetCurrencyBalanceRequest *request, CCGetCurrencyBalanceResponse *response)
    {
        NSLog(@"Request for economy balances was sucecssful.");
        callback(YES, nil, response.balances);
    };
    
    CCGetCurrencyBalanceErrorCallback errorCallback = ^(CCGetCurrencyBalanceRequest *request, CCGetCurrencyBalanceError *error)
    {
        NSLog(@"%@",error.errorDescription);
        callback(NO, error.errorDescription, nil);
    };
    
    CCEconomy* economy = self.chilliConnect.economy;
    [economy getCurrencyBalanceWithDesc:requestDesc successCallback:successCallback errorCallback:errorCallback];
}

#pragma mark - Messaging and Cloud Code

/* Sends a message */
- (void) sendMessageWithScriptKey:(NSString*)scriptKey dictionary:(NSDictionary*)dictionary callback:(ChilliConnectControllerCallback)callback
{
    CCCloudCode* cloudCode = self.chilliConnect.cloudCode;

    CCRunScriptRequestDesc* requestDesc = [CCRunScriptRequestDesc runScriptRequestDescWithKey:scriptKey];
    requestDesc.params = dictionary;
    
    CCRunScriptResponseCallback successCallback = ^(CCRunScriptRequest* request, CCRunScriptResponse* response)
    {
        NSLog(@"Message sent successfully.");
        callback(YES, nil);
    };

    CCRunScriptErrorCallback errorCallback = ^(CCRunScriptRequest* request, CCRunScriptError* error)
    {
        callback(NO, error.errorDescription);
    };

    [cloudCode runScriptWithDesc:requestDesc successCallback:successCallback errorCallback:errorCallback];
}

/* Get messages */
- (void) getMessages:(ChilliConnectControllerGetMessagesCallback)callback
{
    CCMessaging* messaging = self.chilliConnect.messaging;
    
    CCGetMessagesRequestDesc* requestDesc = [[CCGetMessagesRequestDesc alloc] init];
    // We are going to stick with default values for CCGetMessagesRequestDesc apart from requesting full message data which includes rewards.
    requestDesc.fullMessages = [NSNumber numberWithBool:YES];
    
    CCGetMessagesResponseCallback successCallback = ^(CCGetMessagesRequest *request, CCGetMessagesResponse *response)
    {
        NSLog(@"Getting messages returned successfully.");
        callback(YES, nil, response.messages);
    };
    
    CCGetMessagesErrorCallback errorCallback = ^(CCGetMessagesRequest *request, CCGetMessagesError *error)
    {
        NSLog(@"Getting messages failed.");
        callback(NO, error.errorDescription, nil);
    };
    
    NSLog(@"Reqesting messages...");
    [messaging getMessagesWithDesc:requestDesc successCallback:successCallback errorCallback:errorCallback];
}

/* Redeems reward from a message */
- (void) redeemMessageReward:(NSString*)messageId callback:(ChilliConnectControllerCallback)callback
{
    CCRedeemMessageRewardsRequestDesc* redeemMessageRewardsDesc = [CCRedeemMessageRewardsRequestDesc redeemMessageRewardsRequestDescWithMessageId:messageId];
    redeemMessageRewardsDesc.markAsRead = [NSNumber numberWithBool:YES];
    
    CCRedeemMessageRewardsResponseCallback successCallback = ^(CCRedeemMessageRewardsRequest *request, CCRedeemMessageRewardsResponse *response)
    {
        // You should do more checks here to ensure the redeemed amount(s) in the response match the value(s) you expected to redeem.
        callback(YES, nil);
    };
    
    CCRedeemMessageRewardsErrorCallback errorCallback = ^(CCRedeemMessageRewardsRequest *request, CCRedeemMessageRewardsError *error)
    {
        if(error.errorCode == CCRedeemMessageRewardsErrorCodeMessageRewardsAlreadyRedeemed)
        {
            [self markMessageAsRead:messageId];
        }
        callback(NO, error.description);
    };
    
    CCMessaging* messaging = self.chilliConnect.messaging;
    [messaging redeemMessageRewardsWithDesc:redeemMessageRewardsDesc successCallback:successCallback errorCallback:errorCallback];
}

/* Marks a message as read */
- (void) markMessageAsRead:(NSString*)messageId
{
    CCMarkMessageAsReadResponseCallback successCallback = ^(CCMarkMessageAsReadRequest *request)
    {
        NSLog(@"Marked message with id \"%@\" as read.",messageId);
    };
    
    CCMarkMessageAsReadErrorCallback errorCallback = ^(CCMarkMessageAsReadRequest *request, CCMarkMessageAsReadError *error)
    {
        NSLog(@"%@",error.errorDescription);
    };
    
    CCMessaging* messaging = self.chilliConnect.messaging;
    [messaging markMessageAsReadWithMessageId:messageId successCallback:successCallback errorCallback:errorCallback];
}

#pragma mark - Credentials (private)

/* Sets Chilli Connect credentials */
- (void) setCredentials:(NSString*)chillConnectId secret:(NSString*)chilliConnectSecret
{
    self.chilliConnectId = chillConnectId;
    self.chilliConnectSecret = chilliConnectSecret;
    NSLog(@"Current UserId:%@",self.chilliConnectId);
}

/*  Returns if the given Chilli Connect Id and Secret match our current credentials */
- (BOOL) compareCredentials:(NSString*)chilliConnectId secret:(NSString*)chilliConnectSecret
{
    return [self.chilliConnectId isEqualToString:chilliConnectId] && [self.chilliConnectSecret isEqualToString:chilliConnectSecret];
}

/* Loads Chilli Connect user id and secret from the keychain if it exists */
- (BOOL) loadCredentials
{
    if([self hasCredentials])
    {
        NSLog(@"Attepting to load Chilli Connect credentials but they already exist.");
        return YES;
    }
    
    NSError* error = nil;
    NSData* credentialsJson = [SAMKeychain passwordDataForService:k_ChilliConnectKeychainService account:k_ChilliConnectKeychainAccount error:&error];
    if(error)
    {
        NSLog(@"Error loading Chilli Connect credentials from keychain. %@", [error localizedDescription]);
        return NO;
    }
    if(!credentialsJson)
    {
        NSAssert(false, @"Failed to load Chilli Connect credentials NSData object from keychain.");
    }
    
    error = nil;
    NSDictionary* jsonDict = [NSJSONSerialization JSONObjectWithData:credentialsJson options:NSJSONReadingMutableContainers error:&error];
    if(error)
    {
        NSLog(@"Error deserialising Chilli Connect credentials. %@", [error localizedDescription]);
        return NO;
    }
    if(!jsonDict)
    {
        NSAssert(false, @"Failed to deserialise Chilli Connect credentials.");
    }
    
    [self setCredentials:[jsonDict valueForKey:k_ChilliConnectIDKey] secret:[jsonDict valueForKey:k_ChilliConnectSecretKey]];
    
    return YES;
}

/* Saves Chilli Connect user id and secret to the keychain. */
- (BOOL) saveCredentials
{
    if(![self hasCredentials])
    {
        NSLog(@"Unable to save Chilli Connect credentials as they don't exist.");
        return NO;
    }
    
    NSData* credentialsJson = [self getCredentialsJSONData];
    if(credentialsJson)
    {
        NSError* error = nil;
        if(![SAMKeychain setPasswordData:credentialsJson forService:k_ChilliConnectKeychainService account:k_ChilliConnectKeychainAccount error:&error])
        {
            NSLog(@"Error saving Chilli Connect credentials to keychain. %@", [error localizedDescription]);
            return NO;
        }
    }
    
    return YES;
}

/* Returns the Chilli Connect user id and secret and JSON in a NSData object */
- (NSData*) getCredentialsJSONData
{
    NSMutableDictionary* jsonDict = [[NSMutableDictionary alloc] init];
    [jsonDict setValue:self.chilliConnectId forKey:k_ChilliConnectIDKey];
    [jsonDict setValue:self.chilliConnectSecret forKey:k_ChilliConnectSecretKey];
    NSError* error = nil;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:jsonDict options:0 error:&error];
    if(!error)
    {
        return jsonData;
    }
    
    return nil;
}

#pragma mark - Login (private)

/* Creates a new Chilli Connect user */
- (void)createChilliConnectUser:(NSString*)userName successCallback:(CCCreatePlayerResponseCallback)successCallback errorCallback:(CCCreatePlayerErrorCallback)errorCallback
{
    CCCreatePlayerRequestDesc* requestDesc = [CCCreatePlayerRequestDesc createPlayerRequestDesc];
    requestDesc.displayName = userName;
    
    CCPlayerAccounts* playerAccounts = self.chilliConnect.playerAccounts;
    [playerAccounts createPlayerWithDesc:requestDesc successCallback:successCallback errorCallback:errorCallback];
}

/* Login using the saved Chilli Connect credentials */
- (void)loginChilliConnect:(CCLogInUsingChilliConnectResponseCallback)successCallback errorCallback:(CCLogInUsingChilliConnectErrorCallback)errorCallback
{
    CCPlayerAccounts* playerAccounts = self.chilliConnect.playerAccounts;
    [playerAccounts logInUsingChilliConnect:self.chilliConnectId chilliConnectSecret:self.chilliConnectSecret successCallback:successCallback errorCallback:errorCallback];
}

@end
