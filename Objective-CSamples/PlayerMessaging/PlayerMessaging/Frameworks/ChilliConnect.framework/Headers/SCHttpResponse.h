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
 An enum describing the possible responses from a Http Request.
 */
typedef NS_ENUM(NSUInteger, SCHttpResult) {
    
    /// Signifies a successful Http Request.
    SCHttpResultSuccess,
    
    /// This will be set if the request timed out.
    SCHttpResultTimeout,
    
    /// This will be set if no connection to the server could be established.
    SCHttpResultCouldNotConnect
};

/*!
 An immutable data container for information on the result of an HTTP request. This
 should be created using a SCHttpResponseDesc.
 
 This is immutable after construction and is therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCHttpResponse : NSObject

/// The result of a http request.
@property (readonly) SCHttpResult result;

/// The http response code of a http request. If a connection could not be estabilished
/// this will be 0.
@property (readonly) NSUInteger httpResponseCode;

/// The key-value headers returned by the server. If no connection could be established
/// this will be an empty dictionary.
@property (readonly) NSDictionary *headers;

/// The body of the response from the server. If no connection could be established this
/// will be an empty NSData.
@property (readonly) NSData *body;

/*!
 A factory method for creating a new instance of a HTTP response.
 
 @param desc The description which should be used to create the response.
 
 @return The new HTTP response instance.
 */
+ (instancetype)httpResponseWithDesc:(SCHttpResponseDesc *)desc;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises the HTTP response.
 
 @param desc The description which should be used to create the response.
 
 @return The initialised HTTP response instance.
 */
- (instancetype)initWithDesc:(SCHttpResponseDesc *)desc NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
