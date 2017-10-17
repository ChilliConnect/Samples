//
//  Created by Ian Copland on 2015-09-16
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Limited
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

NS_ASSUME_NONNULL_BEGIN

/*!
 A basic implementation of the Immediate Server Request protocol with no headers
 or body which takes the target URL as an input.
 
 This is immutable and therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCBasicServerRequest : NSObject<SCImmediateServerRequest>

/// The full URL that the request is targeting.
@property (readonly) NSURL *url;

/// The HTTP request method that should be used.
@property (readonly) SCHttpRequestMethod httpRequestMethod;

/*!
 A factory method for creating a new basic request with the given URL.
 
 @param url The target url for the request.
 @param httpRequestMethod The HTTP request method that should be used.
 
 @return The new basic request instance.
 */
+ (instancetype)basicServerRequestWithUrl:(NSURL *)url httpRequestMethod:(SCHttpRequestMethod)httpRequestMethod;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises a basc request with the given URL.
 
 @param url The target url for the request.
 @param httpRequestMethod The HTTP request method that should be used.
 
 @return The initialised test request instance.
 */
- (instancetype)initWithUrl:(NSURL *)url httpRequestMethod:(SCHttpRequestMethod)httpRequestMethod NS_DESIGNATED_INITIALIZER;

/*!
 @return An empty dictionary.
 */
- (NSDictionary *)serialiseHeaders;

/*!
 @return An empty dictionary.
 */
- (NSDictionary *)serialiseBody;

@end

NS_ASSUME_NONNULL_END
