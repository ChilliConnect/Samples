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
#import "SCHttpResponse.h"

NS_ASSUME_NONNULL_BEGIN

/*!
 A data container for information on the result of an HTTP request. This is typically 
 used in the creation of an SCHttpResponseDesc instance.
 
 This is not thread-safe and therefore should not be accessed from multiple threads 
 at the same time.
 
 @author Ian Copland
 */
@interface SCHttpResponseDesc : NSObject<NSCopying>

/// The result of a http request.
@property (nonatomic) SCHttpResult result;

/// The http response code of a http request. If a connection could not be estabilished
/// this will be 0.
@property (nonatomic) NSUInteger httpResponseCode;

/// The key-value headers returned by the server. If no connection could be established
/// this will be an empty dictionary.
@property (nonatomic) NSDictionary *headers;

/// The body of the response from the server. If no connection could be established this
/// will be an empty NSData.
@property (nonatomic) NSData *body;

/*!
 A factory method for creating a new instance of a HTTP response description.
 
 @return The new HTTP response description instance.
 */
+ (instancetype)httpResponseDescWithResult:(SCHttpResult)result;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises the mutable HTTP response description.
 
 @return The initialised mutable HTTP response description instance.
 */
- (instancetype)initWithResult:(SCHttpResult)result NS_DESIGNATED_INITIALIZER;

/*!
 Creates a duplicate of this object in the requested zone.
 
 @param zone The zone in which to create the duplicate.
 
 @return The new instance.
 */
- (instancetype)copyWithZone:(nullable NSZone *)zone;

@end

NS_ASSUME_NONNULL_END
