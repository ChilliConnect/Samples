//
//  Created by Ian Copland on 2015-08-31
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

#import "ForwardDeclarations.h"

NS_ASSUME_NONNULL_BEGIN

/*!
 A block describing a callback from a HTTP GET request containing both the original
 request and the response from the server.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^SCGetHttpRequestCallback)(SCHttpGetRequest *request, SCHttpResponse *response);

/*!
 A block describing a callback from a HTTP POST request containing both the original
 request and the response from the server.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^SCPostHttpRequestCallback)(SCHttpPostRequest *request, SCHttpResponse *response);

/*!
 Provides a means to make both GET and POST requests to the given web-server. 
 Requests can be secure (using HTTPS) and make use of persistent connections
 (keep-alive).
 
 This is thread-safe.
 
 @author Ian Copland
 */
@interface SCHttpSystem : NSObject {
@private
    SCTaskScheduler *_taskScheduler;
    NSMutableArray *_connections;
}

/*!
 A factory method for creating a new instance of the HTTP system.
 
 @return The new HTTP system instance.
 */
+ (instancetype)httpSystemWithTaskScheduler:(SCTaskScheduler *)taskScheduler;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises the http system.
 
 @return The initialised HTTP system.
 */
- (instancetype)initWithTaskScheduler:(SCTaskScheduler *)taskScheduler NS_DESIGNATED_INITIALIZER;

/*!
 Makes a HTTP GET request with the given request parameters. This is performed
 asynchronously, with the callback block run on a background thread.
 
 @param request The description of the HTTP GET request.
 @param callback The callback from the request, which returns the HTTP response.
 */
- (void)sendGetRequest:(SCHttpGetRequest *)request withCallback:(SCGetHttpRequestCallback)callback;

/*!
 Makes a HTTP POST request with the given request parameters. This is performed
 asynchronously, with the callback block run on a background thread.
 
 @param request The description of the HTTP POST request.
 @param callback The callback from the request, which returns the HTTP response.
 */
- (void)sendPostRequest:(SCHttpPostRequest *)request withCallback:(SCPostHttpRequestCallback)callback;


@end

NS_ASSUME_NONNULL_END
