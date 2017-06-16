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

#import "ForwardDeclarations.h"

NS_ASSUME_NONNULL_BEGIN

/*!
 A mutable description of a CCLinkFacebookAccountRequest.

 This is not thread-safe and should typically only be used to create new instances
 of CCLinkFacebookAccountRequest.
 */
@interface CCLinkFacebookAccountRequestDesc : NSObject

/// Access token provided from the Facebook API.
@property (nonatomic) NSString *facebookAccessToken;
	
/// When set to true, any existing Facebook association for the current use will be
/// replaced. Otherwise, an error will be returned. Default false.
@property (nonatomic, nullable) NSNumber *replace;
	
/// When set to true will remove any existing associations this Facebook account
/// already has with another player of this game. Otherwise, an error will be
/// returned. Default false.
@property (nonatomic, nullable) NSNumber *update;

/*!
 A convenience factory method for creating new instances of CCLinkFacebookAccountRequestDesc
 with the given properties.
 
 @param facebookAccessToken Access token provided from the Facebook API.

 @return The new instance.
 */
+ (instancetype)linkFacebookAccountRequestDescWithFacebookAccessToken:(NSString *)facebookAccessToken;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param facebookAccessToken Access token provided from the Facebook API.

 @return The initialised description.
 */
- (instancetype)initWithFacebookAccessToken:(NSString *)facebookAccessToken NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
