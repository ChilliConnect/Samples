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
 A container for information on the response from a CCConvertCurrencyRequest.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCConvertCurrencyResponse : NSObject

/// The amount of the From Currency that was converted. The Amount converted will be
/// rounded down to the nearest multiple defined in the Currency Conversion rule so
/// could be less than the Amount submitted. For example, if the Currency Conversion
/// defines a rule that states 10 of CurrencyOne can be converted to 1 of
/// CurrencyTwo, and a conversion request is submitted with an Amount of 24, only 20
/// of CurrencyOne will be converted to 2 of CurrencyTwo.
@property (readonly) int32_t amountConverted;
	
/// The final balance of the currency converted to.
@property (readonly) CCCurrencyBalance *toBalance;
	
/// The final balance of the currency converted to.
@property (readonly) CCCurrencyBalance *fromBalance;

/*!
 Convenience factory method for creating new instances of CCConvertCurrencyRequest.
 with the given dictionary.
 
 @param dictionary The dictionary containing the contents of a response body. This is 
        typically generated from Json.

 @return The new instance.
 */
+ (instancetype)convertCurrencyResponseWithDictionary:(NSDictionary *)dictionary;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given dictionary.
 
 @param dictionary The dictionary containing the contents of a response body. This is 
        typically generated from Json.

 @return The initialised response.
 */
- (instancetype)initWithDictionary:(NSDictionary *)dictionary NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
