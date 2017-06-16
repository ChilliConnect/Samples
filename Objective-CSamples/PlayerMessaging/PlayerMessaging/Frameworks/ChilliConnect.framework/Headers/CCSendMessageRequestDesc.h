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
 A mutable description of a CCSendMessageRequest.

 This is not thread-safe and should typically only be used to create new instances
 of CCSendMessageRequest.
 */
@interface CCSendMessageRequestDesc : NSObject

/// ChilliConnectID of the Player to send the message to.
@property (nonatomic) NSString *to;
	
/// ChilliConnectID of the Player to send the message from.
@property (nonatomic, nullable) NSString *from;
	
/// A title or summary for the message.
@property (nonatomic, nullable) NSString *title;
	
/// The message body to send.
@property (nonatomic, nullable) NSString *text;
	
/// Custom data to be sent with the message.
@property (nonatomic, nullable) SCMultiTypeValue *data;
	
/// An array list of Tags.
@property (nonatomic, nullable) NSArray *tags;
	
/// Number of seconds until the message expires. Default: 7776000 (90 days). Maximum:
/// 7776000 (90 days).
@property (nonatomic, nullable) NSNumber *expiry;
	
/// Items that are going to be generated and sent to the recipient.
@property (nonatomic, nullable) CCMessageGifts *gifts;
	
/// Items that are to be transferred from the sender to the recipient. Items are
/// deducted from the sender's account upon sending. Note: It is invalid to populate
/// this parameter if there is no sender (From is not given).
@property (nonatomic, nullable) CCMessageTransfer *transfer;

/*!
 A convenience factory method for creating new instances of CCSendMessageRequestDesc
 with the given properties.
 
 @param to ChilliConnectID of the Player to send the message to.

 @return The new instance.
 */
+ (instancetype)sendMessageRequestDescWithTo:(NSString *)to;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param to ChilliConnectID of the Player to send the message to.

 @return The initialised description.
 */
- (instancetype)initWithTo:(NSString *)to NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
