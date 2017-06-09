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

#import "SCImmediateServerRequest.h"

#import "ForwardDeclarations.h"

NS_ASSUME_NONNULL_BEGIN

/*!
 A container for all information that will be sent to the server during a
 lookupUserNames api call.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCLookupUserNamesRequest : NSObject<SCImmediateServerRequest>

/// The url the request will be sent to.
@property (readonly) NSURL *url;

/// The HTTP request method that should be used.
@property (readonly) SCHttpRequestMethod httpRequestMethod;

/// A valid session ConnectAccessToken obtained through one of the login endpoints.
@property (readonly) NSString *connectAccessToken;
	
/// An array of UserNames to look up.
@property (readonly) NSArray *userNames;

/*!
 A convenience factory method for creating new instances of CCLookupUserNames
 with the given properties.
 
 @param userNames An array of UserNames to look up.
 @param connectAccessToken A valid session ConnectAccessToken obtained through one of the login endpoints.

 @return The new instance.
 */
+ (instancetype)lookupUserNamesRequestWithUserNames:(NSArray *)userNames connectAccessToken:(NSString *)connectAccessToken;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param userNames An array of UserNames to look up.
 @param connectAccessToken A valid session ConnectAccessToken obtained through one of the login endpoints.

 @return The initialised request.
 */
- (instancetype)initWithUserNames:(NSArray *)userNames connectAccessToken:(NSString *)connectAccessToken NS_DESIGNATED_INITIALIZER;

/*!
 Serialises all header properties. The output will be a dictionary containing the
 extra header key-value pairs in addition the standard headers sent with all server
 requests. Will return an empty dictionary if there are no headers.
 
 @return The header dictionary.
 */
- (NSDictionary *)serialiseHeaders;

/*!
 Serialises all body properties. The output will be a dictionary containing the body
 of the request in a form that can easily be converted to Json. Will return an empty
 dictionary if there is no body.
 
 @return The body dictionary.
 */
- (NSDictionary *)serialiseBody;

@end

NS_ASSUME_NONNULL_END
