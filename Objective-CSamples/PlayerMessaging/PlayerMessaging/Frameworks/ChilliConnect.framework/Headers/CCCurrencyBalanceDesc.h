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
 A mutable description of a CCCurrencyBalance.

 This is not thread-safe and should typically only be used to create new instances
 of CCCurrencyBalance.
 */
@interface CCCurrencyBalanceDesc : NSObject

/// The currency name.
@property (nonatomic) NSString *name;
	
/// The currency key.
@property (nonatomic) NSString *key;
	
/// The player's balance.
@property (nonatomic) int32_t balance;
	
/// The current value of the WriteLock for this Currency Key. To enable conflict
/// checking, the returned WriteLock can be provided to the Set Currency Balance,
/// Withdraw Currency, Deposit Currency and Convert Currency calls on subsequent
/// update attempts.
@property (nonatomic, nullable) NSString *writeLock;

/*!
 A convenience factory method for creating new instances of CCCurrencyBalanceDesc
 with the given properties.
 
 @param name The currency name.
 @param key The currency key.
 @param balance The player's balance.

 @return The new instance.
 */
+ (instancetype)currencyBalanceDescWithName:(NSString *)name key:(NSString *)key balance:(int32_t)balance;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param name The currency name.
 @param key The currency key.
 @param balance The player's balance.

 @return The initialised description.
 */
- (instancetype)initWithName:(NSString *)name key:(NSString *)key balance:(int32_t)balance NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
