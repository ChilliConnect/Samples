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
 A data container class describing the properties of a HTTP GET request, including 
 the target web address and information on headers. This is typically used for 
 constructing new instances of SCHttpGetRequest.
 
 This is not thread safe and should not be accessed from multiple threads at the
 same time.
 
 @author Ian Copland
 */
@interface SCHttpGetRequestDesc : NSObject<NSCopying>

/// describes the url that the request is targeting.
@property (nonatomic) NSURL *url;

/// Contains the key-value headers which should be send as part of the request.
@property (nonatomic) NSDictionary *headers;

/// The number of seconds before the request will timeout. Defaults to 15.
@property (nonatomic) NSUInteger timeoutSeconds;

/*!
 A factory method for creating a new instance of a HTTP GET request
 description.
 
 @param url The url that the request is targeting.
 
 @return The new HTTP GET request description instance.
 */
+ (instancetype)httpGetRequestDescWithUrl:(NSURL *)url;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises the HTTP GET request description.
 
 @param url The url that the request is targeting.
 
 @return The initialised HTTP GET request description instance.
 */
- (instancetype)initWithUrl:(NSURL *)url NS_DESIGNATED_INITIALIZER;

/*!
 Creates a duplicate of this object in the requested zone.
 
 @param zone The zone in which to create the duplicate.
 
 @return The new instance.
 */
- (instancetype)copyWithZone:(nullable NSZone *)zone;

@end

NS_ASSUME_NONNULL_END

