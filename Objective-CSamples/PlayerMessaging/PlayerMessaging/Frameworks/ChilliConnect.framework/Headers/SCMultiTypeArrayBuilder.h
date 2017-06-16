//
//  Created by Ian Copland on 2016-02-02
//
//  The MIT License (MIT)
//
//  Copyright (c) 2016 Tag Games Limited
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

NS_ASSUME_NONNULL_BEGIN

@class SCMultiTypeArray;
@class SCMultiTypeDictionary;
@class SCMultiTypeValue;

/*!
 Provides the means to construct a new SCMultiTypeArray instance. Certain heterogeneous 
 types can be added to the array description; the constructed SCMultiTypeArray will 
 then contain these objects. As this is purely for construction of a SCMultiTypeArray, 
 other typical array functionality is omitted.
 
 This is mutable and therefore not thread-safe. It should never be accessed from 
 multiple threads at the same time.
 
 @author Ian Copland
 */
@interface SCMultiTypeArrayBuilder : NSObject

/*!
 Creates a new SCMultiTypeArrayBuilder instance.
 
 @return The new instance.
 */
+ (SCMultiTypeArrayBuilder *)multiTypeArrayBuilder;

/*!
 Initialises the SCMultiTypeArrayBuilder instance.
 
 @return The initialised SCMultiTypeArrayBuilder.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

/*!
 Adds a new value to the array description.
 
 @param value The value which should be added.
 */
- (void)addObject:(SCMultiTypeValue *)value;

/*!
 Adds a new value to the array description.
 
 @param value The value which should be added.
 */
- (void)addNumber:(NSNumber *)value;

/*!
 Adds a new value to the array description.
 
 @param value The value which should be added.
 */
- (void)addString:(NSString *)value;

/*!
 Adds a new value to the array description.
 
 @param value The value which should be added.
 */
- (void)addArray:(SCMultiTypeArray *)value;

/*!
 Adds a new value to the array description.
 
 @param value The value which should be added.
 */
- (void)addDictionary:(SCMultiTypeDictionary *)value;

/*!
 Generates a new SCMultiTypeArray from the array description.
 
 @return The new SCMultiTypeArray.
 */
- (SCMultiTypeArray *)build;

@end

NS_ASSUME_NONNULL_END