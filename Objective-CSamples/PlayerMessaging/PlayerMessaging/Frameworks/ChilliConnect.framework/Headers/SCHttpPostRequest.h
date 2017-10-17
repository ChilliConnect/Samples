//
//  Created by Ian Copland on 2015-09-01
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
 A basic data container class describing the properties of a HTTP POST request,
 including the target web address, information on headers and the body data. This 
 should be created using a SCHttpPostRequestDesc.
 
 This is immutable and therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCHttpPostRequest : NSObject

/// describes the url that the request is targetting.
@property (readonly) NSURL *url;

/// Contains the key-value headers which should be send as part of the request.
@property (readonly) NSDictionary *headers;

/// Contains the data that will be sent in the body.
@property (readonly) NSData *body;

/// Describes the content type of the request body. If no content type is supplied
/// the default will be used.
@property (readonly) NSString *contentType;

/// The number of seconds before the request will timeout.
@property (readonly) NSUInteger timeoutSeconds;

/*!
 A factory method for creating a new instance of a HTTP POST request.
 
 @param desc The description which should be used to initialise the request.
 
 @return The new HTTP POST request instance.
 */
+ (instancetype)httpPostRequestWithDesc:(SCHttpPostRequestDesc *)desc;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises the HTTP POST request.
 
 @param desc The description which should be used to initialise the request.
 
 @return The initialised HTTP POST request instance.
 */
- (instancetype)initWithWithDesc:(SCHttpPostRequestDesc *)desc NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
