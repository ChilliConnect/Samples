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
 A mutable description of a CCFacebookPlayer.

 This is not thread-safe and should typically only be used to create new instances
 of CCFacebookPlayer.
 */
@interface CCFacebookPlayerDesc : NSObject

/// The player's ChilliConnectID.
@property (nonatomic) NSString *chilliConnectId;
	
/// The player's UserName.
@property (nonatomic, nullable) NSString *userName;
	
/// The player's DisplayName.
@property (nonatomic, nullable) NSString *displayName;
	
/// The player's Facebook ID.
@property (nonatomic) NSString *facebookId;
	
/// The player's Facebook Name.
@property (nonatomic) NSString *facebookName;

/*!
 A convenience factory method for creating new instances of CCFacebookPlayerDesc
 with the given properties.
 
 @param chilliConnectId The player's ChilliConnectID.
 @param facebookId The player's Facebook ID.
 @param facebookName The player's Facebook Name.

 @return The new instance.
 */
+ (instancetype)facebookPlayerDescWithChilliConnectId:(NSString *)chilliConnectId facebookId:(NSString *)facebookId facebookName:(NSString *)facebookName;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param chilliConnectId The player's ChilliConnectID.
 @param facebookId The player's Facebook ID.
 @param facebookName The player's Facebook Name.

 @return The initialised description.
 */
- (instancetype)initWithChilliConnectId:(NSString *)chilliConnectId facebookId:(NSString *)facebookId facebookName:(NSString *)facebookName NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
