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
 A mutable description of a CCCreatePlayerRequest.

 This is not thread-safe and should typically only be used to create new instances
 of CCCreatePlayerRequest.
 */
@interface CCCreatePlayerRequestDesc : NSObject

/// The UserName of the new player account. If provided, this must be unique across
/// all players within the game, contain only alpha, numeric, underscore or dash
/// characters, and a minimum size of 3 characters, maximum 50.
@property (nonatomic, nullable) NSString *userName;
	
/// A non-unique DisplayName that can be used to identify the Player within the game.
/// If provided it can contain only alpha, numeric, underscore or dash characters,
/// and a minimum size of 3 characters, maximum 50.
@property (nonatomic, nullable) NSString *displayName;
	
/// Email address to be associated with the new player account. If provided, this
/// must be unique across all players within the game.
@property (nonatomic, nullable) NSString *email;
	
/// Password to be assigned to the new player account. If provided must be greater
/// than 3 and less than 50 characters in length.
@property (nonatomic, nullable) NSString *password;

/*!
 A convenience factory method for creating new instances of CCCreatePlayerRequestDesc.

 @return The new instance.
 */
+ (instancetype)createPlayerRequestDesc;

/*!
 Initialises the description.

 @return The initialised description.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
