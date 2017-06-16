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
 A mutable description of a CCPlayerInventoryItem.

 This is not thread-safe and should typically only be used to create new instances
 of CCPlayerInventoryItem.
 */
@interface CCPlayerInventoryItemDesc : NSObject

/// The inventory item identifier.
@property (nonatomic) NSString *itemId;
	
/// The inventory item key.
@property (nonatomic) NSString *key;
	
/// The inventory item name. Note: this may be empty if the Economy Inventory Item
/// definition no longer exists.
@property (nonatomic) NSString *name;
	
/// The data associated with this item instance.
@property (nonatomic, nullable) SCMultiTypeValue *instanceData;
	
/// The identifier for the last write for this item in the player's inventory.
@property (nonatomic) NSString *writeLock;

/*!
 A convenience factory method for creating new instances of CCPlayerInventoryItemDesc
 with the given properties.
 
 @param itemId The inventory item identifier.
 @param key The inventory item key.
 @param name The inventory item name. Note: this may be empty if the Economy Inventory Item
        definition no longer exists.	
 @param writeLock The identifier for the last write for this item in the player's inventory.

 @return The new instance.
 */
+ (instancetype)playerInventoryItemDescWithItemId:(NSString *)itemId key:(NSString *)key name:(NSString *)name writeLock:(NSString *)writeLock;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param itemId The inventory item identifier.
 @param key The inventory item key.
 @param name The inventory item name. Note: this may be empty if the Economy Inventory Item
        definition no longer exists.	
 @param writeLock The identifier for the last write for this item in the player's inventory.

 @return The initialised description.
 */
- (instancetype)initWithItemId:(NSString *)itemId key:(NSString *)key name:(NSString *)name writeLock:(NSString *)writeLock NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
