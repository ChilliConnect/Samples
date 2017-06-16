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
#import "SCHttpRequestMethod.h"

NS_ASSUME_NONNULL_BEGIN

/*!
 The protocol for all immediate server request objects. All server requests will
 implement this protocol, providing a common interface for calling them.
 
 @author Ian Copland
 */
@protocol SCImmediateServerRequest <NSObject>

/// The full URL that the request is targeting.
@property (readonly) NSURL *url;

/// The HTTP request method that should be used.
@property (readonly) SCHttpRequestMethod httpRequestMethod;

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
 dictionary if there is no body. Will always be empty if the GET HTTP request method
 is used.
 
 @return The body dictionary.
 */
- (NSDictionary *)serialiseBody;

@end

NS_ASSUME_NONNULL_END
