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
 A mutable description of a CCAddIapEventRequest.

 This is not thread-safe and should typically only be used to create new instances
 of CCAddIapEventRequest.
 */
@interface CCAddIapEventRequestDesc : NSObject

/// A string identifying the item that the player purchased.
@property (nonatomic) NSString *item;
	
/// The amount of local currency paid by the player for the IAP.
@property (nonatomic) float localCost;
	
/// The local currency with which the player purchased the IAP. This must be a valid
/// ISO-4217 currency code.
@property (nonatomic) NSString *localCurrency;
	
/// A number representing the player's in game level.
@property (nonatomic, nullable) NSNumber *userGrade;
	
/// A string indicating the test group the player belongs to.
@property (nonatomic, nullable) NSString *testGroup;
	
/// What offer, if any, the IAP was purchased under.
@property (nonatomic, nullable) NSString *offer;

/*!
 A convenience factory method for creating new instances of CCAddIapEventRequestDesc
 with the given properties.
 
 @param item A string identifying the item that the player purchased.
 @param localCost The amount of local currency paid by the player for the IAP.
 @param localCurrency The local currency with which the player purchased the IAP. This must be a valid
        ISO-4217 currency code.	

 @return The new instance.
 */
+ (instancetype)addIapEventRequestDescWithItem:(NSString *)item localCost:(float)localCost localCurrency:(NSString *)localCurrency;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param item A string identifying the item that the player purchased.
 @param localCost The amount of local currency paid by the player for the IAP.
 @param localCurrency The local currency with which the player purchased the IAP. This must be a valid
        ISO-4217 currency code.	

 @return The initialised description.
 */
- (instancetype)initWithItem:(NSString *)item localCost:(float)localCost localCurrency:(NSString *)localCurrency NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
