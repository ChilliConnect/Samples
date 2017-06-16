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
 A container used to describe a message.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCMessage : NSObject

/// Identifier for the message.
@property (readonly) NSString *messageId;
	
/// Details of the player that sent the message.
@property (readonly) CCMessageSender *from;
	
/// Date when the message was sent (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
@property (readonly) NSDate *sentOn;
	
/// Has the message been read.
@property (readonly) BOOL read;
	
/// Date when the message was read (UTC). Format: ISO8601 e.g. 2016-01-12T11:08:23.
@property (readonly, nullable) NSDate *readOn;
	
/// Have the message rewards been redeemed.
@property (readonly, nullable) NSNumber *redeemed;
	
/// Date when the message rewards were redeemed (UTC). Format: ISO8601 e.g.
/// 2016-01-12T11:08:23.
@property (readonly, nullable) NSDate *redeemedOn;
	
/// An array list of Tags for the message.
@property (readonly, nullable) NSArray *tags;
	
/// Number of seconds until the message expires.
@property (readonly, nullable) NSNumber *expiry;
	
/// A title or summary for the message.
@property (readonly, nullable) NSString *title;
	
/// The message body.
@property (readonly, nullable) NSString *text;
	
/// Custom data for the message.
@property (readonly, nullable) SCMultiTypeValue *data;
	
/// The rewards that may be redeemed by the recipient of the message.
@property (readonly, nullable) CCMessageReward *rewards;

/*!
 A convenience factory method for creating new instances of CCMessage
 with the given description.
 
 @param desc The description to build the new instance from.

 @return The new instance.
 */
+ (instancetype)messageWithDesc:(CCMessageDesc *)desc;

/*!
 Convenience factory method for creating new instances of CCMessage
 from the contents of the given Json dictionary.
 
 @param dictionary The properties of the object in dictionary form. Typically this
        is created from Json.
 
 @return The new instance.
 */
+ (instancetype)messageWithJson:(NSDictionary *)dictionary;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given description.
 
 @param desc The description to build the new instance from.

 @return The initialised object.
 */
- (instancetype)initWithDesc:(CCMessageDesc *)desc NS_DESIGNATED_INITIALIZER;

/*!
 Initialise with the contents of the given dictionary.
 
 @param dictionary The properties of the object in dictionary form. Typically this
        is created from Json.
 
 @return The initialised object.
 */
- (instancetype)initWithDictionary:(NSDictionary *)dictionary NS_DESIGNATED_INITIALIZER;

/*!
 Serialises all properties. The output will be a dictionary containing the objects 
 properties in a form that can easily be converted to Json. 
 
 @return The serialised object in dictionary form. 
 */
 - (NSDictionary *)serialise;
 
@end

NS_ASSUME_NONNULL_END
