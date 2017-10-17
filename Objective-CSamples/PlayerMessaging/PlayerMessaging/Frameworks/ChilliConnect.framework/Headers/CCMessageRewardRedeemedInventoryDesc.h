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
 A mutable description of a CCMessageRewardRedeemedInventory.

 This is not thread-safe and should typically only be used to create new instances
 of CCMessageRewardRedeemedInventory.
 */
@interface CCMessageRewardRedeemedInventoryDesc : NSObject

/// An instance identifier for the item added to the player's inventory.
@property (nonatomic) NSString *itemId;
	
/// The name of the item.
@property (nonatomic) NSString *name;
	
/// The key of the item.
@property (nonatomic) NSString *key;
	
/// The data associated with this item instance.
@property (nonatomic, nullable) SCMultiTypeValue *instanceData;

/*!
 A convenience factory method for creating new instances of CCMessageRewardRedeemedInventoryDesc
 with the given properties.
 
 @param itemId An instance identifier for the item added to the player's inventory.
 @param name The name of the item.
 @param key The key of the item.

 @return The new instance.
 */
+ (instancetype)messageRewardRedeemedInventoryDescWithItemId:(NSString *)itemId name:(NSString *)name key:(NSString *)key;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param itemId An instance identifier for the item added to the player's inventory.
 @param name The name of the item.
 @param key The key of the item.

 @return The initialised description.
 */
- (instancetype)initWithItemId:(NSString *)itemId name:(NSString *)name key:(NSString *)key NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
