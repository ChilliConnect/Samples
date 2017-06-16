//
//  Created by Ian Copland on 2015-09-03
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
 A block describing a callback from an immediate server request containing both 
 the original request and the response from the server.
 
 @param request The input request.
 @param response The response from the server.
 */
typedef void (^SCImmediateServerRequestCallback)(id<SCImmediateServerRequest> request, SCServerResponse *response);

/*!
 Manages all server requests. Holds on to active requests for their entire lifetime
 and provides logging of the different event types.
 
 This is immutable, meaning it is thread-safe.
 
 @author Ian Copland
 */
@interface SCServerRequestSystem : NSObject {
    SCTaskScheduler *_taskScheduler;
    SCHttpSystem *_httpSystem;
}

/*!
 A factory method for creating a new instance of the server request system.
 
 @param taskScheduler The task scheduler.
 @param httpSystem The http request system.
 
 @return The new server request system instance.
 */
+ (instancetype)serverRequestSystemWithTaskScheduler:(SCTaskScheduler *)taskScheduler httpSystem:(SCHttpSystem *)httpSystem;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises the http system.
 
 @param taskScheduler The task scheduler.
 @param httpSystem The http request system.
 
 @return The initialised HTTP system.
 */
- (instancetype)initWithTaskScheduler:(SCTaskScheduler *)taskScheduler httpSystem:(SCHttpSystem *)httpSystem NS_DESIGNATED_INITIALIZER;

/*!
 Performs a server request with the given request parameters. This is performed
 asynchronously.
 
 @param request The request that should be performed, which must adhere to the
        immediate request protocol.
 @param callback The callback containing the response. The callback will be on a
        background thread.
 */
- (void)sendImmediateRequest:(id<SCImmediateServerRequest>)request withCallback:(SCImmediateServerRequestCallback)callback;

@end

NS_ASSUME_NONNULL_END
