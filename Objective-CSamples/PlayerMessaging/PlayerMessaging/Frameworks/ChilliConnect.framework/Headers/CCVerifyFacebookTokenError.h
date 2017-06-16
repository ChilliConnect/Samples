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
 CCVerifyFacebookTokenRequest.
 */
typedef NS_ENUM(NSInteger, CCVerifyFacebookTokenErrorCode) {
	
	/// A connection could not be established.
	CCVerifyFacebookTokenErrorCodeCouldNotConnect = -2,
	
	/// The request timed out.
	CCVerifyFacebookTokenErrorCodeTimeout = -1,
	
	/// An unexpected, fatal error has occured on the server. 
	CCVerifyFacebookTokenErrorCodeUnexpectedError = 1,
	
	/// Expired Connect Access Token. The Connect Access Token used to authenticate with
	/// the server has expired and should be renewed.
	CCVerifyFacebookTokenErrorCodeExpiredConnectAccessToken = 1003,
	
	/// Invalid Connect Access Token. The Connect Access Token was not valid and cannot
	/// be used to authenticate requests.
	CCVerifyFacebookTokenErrorCodeInvalidConnectAccessToken = 1004,
	
	/// Player Not Linked To Facebook. The current player has no linked Facebook account.
	CCVerifyFacebookTokenErrorCodePlayerNotLinkedToFacebook = 2005
};

/*!
 A container for information on any errors that occur during a
 CCVerifyFacebookTokenRequest

 This is immutable after construction and is therefore thread safe.
 */
@interface CCVerifyFacebookTokenError : NSObject

/// A code describing the error that has occurred.
@property (readonly) CCVerifyFacebookTokenErrorCode errorCode;

/// A description of the error that as occurred.
@property (readonly) NSString *errorDescription;

/// Additional, error specific information.
@property (readonly) SCMultiTypeValue *errorData;

/*!
 Convenience factory method for creating new instances of CCVerifyFacebookTokenError
 with the given server response.
 
 @param serverResponse The server response. Must represent an error otherwise this will
 assert.

 @return The new instance.
 */
+ (instancetype)verifyFacebookTokenErrorWithServerResponse:(SCServerResponse *)serverResponse;

/*!
 Convenience factory method for creating new instances of CCVerifyFacebookTokenError
 with the given error code.
 
 @param errorCode A code describing the error that has occurred.

 @return The new instance.
 */
+ (instancetype)verifyFacebookTokenErrorWithErrorCode:(CCVerifyFacebookTokenErrorCode)errorCode;

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
- (instancetype)initWithErrorCode:(CCVerifyFacebookTokenErrorCode)errorCode errorData:(SCMultiTypeValue *)errorData NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
