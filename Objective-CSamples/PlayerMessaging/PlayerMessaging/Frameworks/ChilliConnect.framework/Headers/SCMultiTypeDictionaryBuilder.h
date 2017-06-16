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
 Provides the means to construct a new SCMultiTypeDictionary instance. Certain 
 heterogeneous types can be added to the dictionary description; the constructed 
 SCMultiTypeDictionary will then contain these objects. As this is purely for 
 construction of a SCMultiTypeDictionary, other typical dictionary functionality 
 is omitted.
 
 This is mutable and therefore not thread-safe. It should never be accessed from 
 multiple threads at the same time.
 
 @author Ian Copland
 */
@interface SCMultiTypeDictionaryBuilder : NSObject

/*!
 Creates a new SCMultiTypeDictionaryBuilder instance.
 
 @return The new instance.
 */
+ (SCMultiTypeDictionaryBuilder *)multiTypeDictionaryBuilder;

/*!
 Initialises the SCMultiTypeDictionaryBuilder instance.
 
 @return The initialised SCMultiTypeDictionaryBuilder.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

/*!
 Adds a new value to the dictionary description.
 
 @param value The value which should be added.
 @param key The key this value should be stored under.
 */
- (void)addObject:(SCMultiTypeValue *)value forKey:(NSString *)key;

/*!
 Adds a new value to the dictionary description.
 
 @param value The value which should be added.
 @param key The key this value should be stored under.
 */
- (void)addNumber:(NSNumber *)value forKey:(NSString *)key;

/*!
 Adds a new value to the dictionary description.
 
 @param value The value which should be added.
 @param key The key this value should be stored under.
 */
- (void)addString:(NSString *)value forKey:(NSString *)key;

/*!
 Adds a new value to the dictionary description.
 
 @param value The value which should be added.
 @param key The key this value should be stored under.
 */
- (void)addArray:(SCMultiTypeArray *)value forKey:(NSString *)key;

/*!
 Adds a new value to the dictionary description.
 
 @param value The value which should be added.
 @param key The key this value should be stored under.
 */
- (void)addDictionary:(SCMultiTypeDictionary *)value forKey:(NSString *)key;

/*!
 Generates a new SCMultiTypeDictionary from the dictionary description.
 
 @return The new SCMultiTypeDictionary.
 */
- (SCMultiTypeDictionary *)build;

@end

NS_ASSUME_NONNULL_END