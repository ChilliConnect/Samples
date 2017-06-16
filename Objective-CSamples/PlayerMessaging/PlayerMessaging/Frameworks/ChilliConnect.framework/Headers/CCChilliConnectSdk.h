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
 The manager object for the ChilliConnect SDK. The SDK is broken up into 
 different sections, or modules, for each collection of features. Each of the
 modules are accessible as properties.

 This is thread-safe.
 */
@interface CCChilliConnectSdk : NSObject

/// The ChilliConnect Player Accounts module. Provides the means to create new
/// players, log in to existing accounts and modify account data.
@property (readonly) CCPlayerAccounts *playerAccounts;

/// The ChilliConnect Cloud Data module. Provides the means to store custom data
/// against Player Accounts for retrieval.
@property (readonly) CCCloudData *cloudData;

/// The ChillConnect Leaderboards module. Provides the means to add to and query from
/// leaderboards.
@property (readonly) CCLeaderboards *leaderboards;

/// The ChilliConnect Cloud Code module. Provides the means to create excute custom
/// server side scripts.
@property (readonly) CCCloudCode *cloudCode;

/// The ChillConnect Push Notifications module. Provides the means to send push
/// messages to players using Amazon Device Messaging, Apple Push Notification
/// Service and Google Cloud Messaging.
@property (readonly) CCPushNotifications *pushNotifications;

/// The ChillConnect In-App Purchase Validation module. Provides the means to
/// validate in-app purchases using Amazon Receipt Validation Service, Apple AppStore
/// and Google Play Store.
@property (readonly) CCInAppPurchase *inAppPurchase;

/// Bring back a list of requested DLC packages along with their contained files.
/// When a request provides multiple Tags, only packages that have all Tags will be
/// returned.
@property (readonly) CCDlc *dlc;

/// The ChillConnect Metrics module. This provides the means to log metrics events
/// with the server.
@property (readonly) CCMetrics *metrics;

/// The ChillConnect Economy Management module. Provides the means to retrieve and
/// modify player currencies and inventory.
@property (readonly) CCEconomy *economy;

/// The ChilliConnect Messaging module. Enables sending of messages, and gifting
/// economy items to players.
@property (readonly) CCMessaging *messaging;

/*!
 Convenience factory method for creating new instances of CCChilliConnectSdk
 with the given App Token.
 
 @param appToken The App Token.
 @param verboseLogging Whether or not to enable verbose logging. This is typically only
        enabled while debugging.

 @return The new instance.
 */
+ (instancetype)chilliConnectSdkWithAppToken:(NSString *)appToken verboseLogging:(BOOL)verboseLogging;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given App Token.
 
 @param appToken The App Token.
 @param verboseLogging Whether or not to enable verbose logging. This is typically only
        enabled while debugging.

 @return The initialised object.
 */
- (instancetype)initWithAppToken:(NSString *)appToken verboseLogging:(BOOL)verboseLogging NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
