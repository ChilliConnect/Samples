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
 An enum describing the possible responses from a Server Request.
 */
typedef NS_ENUM(NSUInteger, SCServerResult) {
    
    /// Signifies a successful server Request.
    SCServerResultSuccess,
    
    /// This will be set if the request timed out.
    SCServerResultTimeout,
    
    /// This will be set if no connection to the server could be established.
    SCServerResultCouldNotConnect
};

/*!
 The response from a request made through the server request system. This takes the
 HTTP response and converts the data to an easier to work with format for later
 converting to a server API call specific response. This converting the body to a 
 dictionary (i.e JSON format) and doesn't expose headers.
 
 This is immutable, meaning it is thread-safe.
 
 @author Ian Copland
 */
@interface SCServerResponse : NSObject

/// The result of the server request.
@property (readonly) SCServerResult result;

/// The http response code of a server request. If a connection could not be estabilished
/// this will be 0.
@property (readonly) NSUInteger httpResponseCode;

/// The body of the response from the server, in json form (represented as a dictionary)
/// If no connection could be established this will be an empty NSData.
@property (readonly) NSDictionary *body;

/*!
 A factory method for creating a new instance of a server response.
 
 @param httpResponse The HTTP response that this will be created from.
 
 @return The new server response instance.
 */
+ (instancetype)serverResponseWithHttpResponse:(SCHttpResponse *)httpResponse;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises the server response.
 
 @param httpResponse The HTTP response that this will be initialised from.
 
 @return The initialised server response instance.
 */
- (instancetype)initWithHttpResponse:(SCHttpResponse *)httpResponse NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
