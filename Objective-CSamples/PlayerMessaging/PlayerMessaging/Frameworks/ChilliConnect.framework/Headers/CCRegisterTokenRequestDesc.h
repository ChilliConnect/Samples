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
 A mutable description of a CCRegisterTokenRequest.

 This is not thread-safe and should typically only be used to create new instances
 of CCRegisterTokenRequest.
 */
@interface CCRegisterTokenRequestDesc : NSObject

/// The push notification service the device token belongs to. Must be one of APNS,
/// GCM or ADM.
@property (nonatomic) NSString *service;
	
/// The Push Token. Note: for APNS (iOS) base64 encode the NSData object to form this
/// string.
@property (nonatomic) NSString *deviceToken;
	
/// If true, will clear any previously stored Push Tokens for this Player and
/// Service. Defaults to false.
@property (nonatomic, nullable) NSNumber *overwrite;

/*!
 A convenience factory method for creating new instances of CCRegisterTokenRequestDesc
 with the given properties.
 
 @param service The push notification service the device token belongs to. Must be one of APNS,
        GCM or ADM.	
 @param deviceToken The Push Token. Note: for APNS (iOS) base64 encode the NSData object to form this
        string.	

 @return The new instance.
 */
+ (instancetype)registerTokenRequestDescWithService:(NSString *)service deviceToken:(NSString *)deviceToken;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param service The push notification service the device token belongs to. Must be one of APNS,
        GCM or ADM.	
 @param deviceToken The Push Token. Note: for APNS (iOS) base64 encode the NSData object to form this
        string.	

 @return The initialised description.
 */
- (instancetype)initWithService:(NSString *)service deviceToken:(NSString *)deviceToken NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
