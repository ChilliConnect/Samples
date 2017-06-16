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
 An enum describing each of the possible error codes that can be returned from a
 CCRedeemMessageRewardsRequest.
 */
typedef NS_ENUM(NSInteger, CCRedeemMessageRewardsErrorCode) {
	
	/// A connection could not be established.
	CCRedeemMessageRewardsErrorCodeCouldNotConnect = -2,
	
	/// The request timed out.
	CCRedeemMessageRewardsErrorCodeTimeout = -1,
	
	/// An unexpected, fatal error has occured on the server. 
	CCRedeemMessageRewardsErrorCodeUnexpectedError = 1,
	
	/// Invalid Request. One of more of the provided fields were not correctly formatted.
	/// The data property of the response body will contain specific error messages for
	/// each field.
	CCRedeemMessageRewardsErrorCodeInvalidRequest = 1007,
	
	/// Temporary Service Error. A temporary error is preventing the request from being
	/// processed.
	CCRedeemMessageRewardsErrorCodeTemporaryServiceError = 1008,
	
	/// Expired Connect Access Token. The Connect Access Token used to authenticate with
	/// the server has expired and should be renewed.
	CCRedeemMessageRewardsErrorCodeExpiredConnectAccessToken = 1003,
	
	/// Invalid Connect Access Token. The Connect Access Token was not valid and cannot
	/// be used to authenticate requests.
	CCRedeemMessageRewardsErrorCodeInvalidConnectAccessToken = 1004,
	
	/// Method Disabled. Public access to this method has been disabled on the
	/// ChilliConnect Dashboard.
	CCRedeemMessageRewardsErrorCodeMethodDisabled = 1011,
	
	/// Message Not Found. The message specified was not found.
	CCRedeemMessageRewardsErrorCodeMessageNotFound = 11001,
	
	/// Message Rewards Already Redeemed. The message rewards have already been redeemed.
	CCRedeemMessageRewardsErrorCodeMessageRewardsAlreadyRedeemed = 11002,
	
	/// Currency Maximum Exceeded. Honouring the request would exceed the maximum as
	/// defined by the Currency.
	CCRedeemMessageRewardsErrorCodeCurrencyMaximumExceeded = 10201
};

/*!
 A container for information on any errors that occur during a
 CCRedeemMessageRewardsRequest

 This is immutable after construction and is therefore thread safe.
 */
@interface CCRedeemMessageRewardsError : NSObject

/// A code describing the error that has occurred.
@property (readonly) CCRedeemMessageRewardsErrorCode errorCode;

/// A description of the error that as occurred.
@property (readonly) NSString *errorDescription;

/// Additional, error specific information.
@property (readonly) SCMultiTypeValue *errorData;

/*!
 Convenience factory method for creating new instances of CCRedeemMessageRewardsError
 with the given server response.
 
 @param serverResponse The server response. Must represent an error otherwise this will
 assert.

 @return The new instance.
 */
+ (instancetype)redeemMessageRewardsErrorWithServerResponse:(SCServerResponse *)serverResponse;

/*!
 Convenience factory method for creating new instances of CCRedeemMessageRewardsError
 with the given error code.
 
 @param errorCode A code describing the error that has occurred.

 @return The new instance.
 */
+ (instancetype)redeemMessageRewardsErrorWithErrorCode:(CCRedeemMessageRewardsErrorCode)errorCode;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given error code.
 
 @param errorCode A code describing the error that has occurred.
 @param errorData A dictionary of extra information on the specific error that 
        occurred.

 @return The new instance.
 */
- (instancetype)initWithErrorCode:(CCRedeemMessageRewardsErrorCode)errorCode errorData:(SCMultiTypeValue *)errorData NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
