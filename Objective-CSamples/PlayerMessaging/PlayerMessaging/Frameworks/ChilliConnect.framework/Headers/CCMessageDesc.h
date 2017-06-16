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
 A mutable description of a CCMessage.

 This is not thread-safe and should typically only be used to create new instances
 of CCMessage.
 */
@interface CCMessageDesc : NSObject

/// Identifier for the message.
@property (nonatomic) NSString *messageId;
	
/// Details of the player that sent the message.
@property (nonatomic) CCMessageSender *from;
	
/// Date when the message was sent (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
@property (nonatomic) NSDate *sentOn;
	
/// Has the message been read.
@property (nonatomic) BOOL read;
	
/// Date when the message was read (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
@property (nonatomic, nullable) NSDate *readOn;
	
/// Have the message rewards been redeemed.
@property (nonatomic, nullable) NSNumber *redeemed;
	
/// Date when the message rewards were redeemed (UTC). Format: ISO8601 e.g.
/// 2016-01-12T11:08:23.
@property (nonatomic, nullable) NSDate *redeemedOn;
	
/// An array list of Tags for the message.
@property (nonatomic, nullable) NSArray *tags;
	
/// Number of seconds until the message expires.
@property (nonatomic, nullable) NSNumber *expiry;
	
/// A title or summary for the message.
@property (nonatomic, nullable) NSString *title;
	
/// The message body.
@property (nonatomic, nullable) NSString *text;
	
/// Custom data for the message.
@property (nonatomic, nullable) SCMultiTypeValue *data;
	
/// The rewards that may be redeemed by the recipient of the message.
@property (nonatomic, nullable) CCMessageReward *rewards;

/*!
 A convenience factory method for creating new instances of CCMessageDesc
 with the given properties.
 
 @param messageId Identifier for the message.
 @param from Details of the player that sent the message.
 @param sentOn Date when the message was sent (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
 @param read Has the message been read.

 @return The new instance.
 */
+ (instancetype)messageDescWithMessageId:(NSString *)messageId from:(CCMessageSender *)from sentOn:(NSDate *)sentOn read:(BOOL)read;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param messageId Identifier for the message.
 @param from Details of the player that sent the message.
 @param sentOn Date when the message was sent (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
 @param read Has the message been read.

 @return The initialised description.
 */
- (instancetype)initWithMessageId:(NSString *)messageId from:(CCMessageSender *)from sentOn:(NSDate *)sentOn read:(BOOL)read NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
