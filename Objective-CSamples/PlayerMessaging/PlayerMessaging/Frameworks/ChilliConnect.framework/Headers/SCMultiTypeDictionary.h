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
@class SCMultiTypeValue;

/*!
 A block which is used when enumerating over a dictionary.
 
 @param key The current key.
 @param value The current value.
 @param stop If set to true, enumeration will stop.
 */
typedef void (^SCMultiTypeDictionaryEnumerator)(NSString *key, SCMultiTypeValue *value, BOOL *stop);

/*!
 An immutable dictionary which can contain values of certain heterogeneous types. 
 The key is always a string. The dictionary can be serialised to a json compliant
 NSDictionary<string, id>.
 
 Typically this is generated via a SCMultiTypeDictionaryBuilder; alternatively it 
 can be generated from a json compliant NSDictionary<string, id>.
 
 As this is immutable, it is thread-safe.
 
 @author Ian Copland
 */
@interface SCMultiTypeDictionary : NSObject<NSFastEnumeration>

/// The number of entries in the dictionary.
@property (readonly, nonatomic) NSUInteger count;

/// A new array containing all of the keys in the dictionary.
@property (readonly, nonatomic) NSArray<NSString *> *allKeys;

/// A new array containing all of the values in the dictionary.
@property (readonly, nonatomic) NSArray<SCMultiTypeValue *> *allValues;

/*!
 Creates a new empty SCMultiTypeDictionary instance.
 
 @return The new instance.
 */
+ (SCMultiTypeDictionary *) multiTypeDictionary;

/*!
 Creates a new SCMultiTypeDictionary from the given list of SCMultiTypeValues. 
 Typically this isn't called directly, instead this is generated via a 
 SCMultiTypeDictionaryBuilder.
 
 @param values The values this SCMultiTypeDictionary should contain.
 
 @return The new instance.
 */
+ (SCMultiTypeDictionary *) multiTypeDictionaryWithValues:(NSDictionary<NSString *, SCMultiTypeValue *> *)values;

/*!
 Initialises a new SCMultiTypeDictionary from the given Json compliant dictionary. 
 The dictionary must contain only Json compatible types: NSNull, NSNumber, NSString, 
 NSArray<id> or NSDictionary<NSString *, id>. Contained Arrays or Dictionaries must 
 also contain only Json compatible types. If the type is invalid an error will occur.
 
 @param dictionary A json compatible dictionary.
 
 @return The new instance.
 */
+ (SCMultiTypeDictionary *) multiTypeDictionaryWithJson:(NSDictionary<NSString *, id> *)dictionary;

/*!
 Initialises a new empty SCMultiTypeDictionary instance.
 
 @return The initialised empty SCMultiTypeDictionary.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

/*!
 Initialises a new SCMultiTypeDictionary instance with the given values.
 
 @param values The values this SCMultiTypeDictionary should contain.
 
 @return The initialised SCMultiTypeDictionary.
 */
- (instancetype)initWithValues:(NSDictionary<NSString *, SCMultiTypeValue *> *)values NS_DESIGNATED_INITIALIZER;

/*!
 Returns whether or not the given key exists in the dictionary.
 
 @param key The key to lookup.
 
 @return Whether or not the key exists.
 */
- (BOOL)containsKey:(NSString *)key;

/*!
 Returns the SCMultiTypeValue value for the given key. If the key doesn't exist then 
 an error will occur.
 
 @param key The key to lookup.
 
 @return The value for the given key.
 */
- (SCMultiTypeValue *)objectForKey:(NSString *)key;

/*!
 Returns the NSNumber value for the given key. If the key doesn't exist, or the value is the
 wrong type, then an error will occur.
 
 @param key The key to lookup.
 
 @return The value for the given key.
 */
- (NSNumber *)numberForKey:(NSString *)key;

/*!
 Returns the string value for the given key. If the key doesn't exist, or the value is the
 wrong type, then an error will occur.
 
 @param key The key to lookup.
 
 @return The value for the given key.
 */
- (NSString *)stringForKey:(NSString *)key;

/*!
 Returns the array value for the given key. If the key doesn't exist, or the value is the
 wrong type, then an error will occur.
 
 @param key The key to lookup.
 
 @return The value for the given key.
 */
- (SCMultiTypeArray *)arrayForKey:(NSString *)key;

/*!
 Returns the dictionary value for the given key. If the key doesn't exist, or the value is the
 wrong type, then an error will occur.
 
 @param key The key to lookup.
 
 @return The value for the given key.
 */
- (SCMultiTypeDictionary *)dictionaryForKey:(NSString *)key;

/*!
 Serialises the dictionary this describes to a Json compliant NSDictionary<NSString *, id>.
 All elements in the dictinoary with be Json compatible types: NSNull, NSNumber, NSString,
 NSArray<id> or NSDictionary<NSString *, id>.
 
 @return The serialised Dictionary.
 */
- (NSDictionary<NSString *, id> *)serialise;

/*!
 Evaluates whether or not this and the given SCMultiTypeDictionary are equal in value.
 
 @param value The value to check for equality.
 
 @return Whether or not they are equal.
 */
- (BOOL)isEqualToMultiTypeDictionary:(SCMultiTypeDictionary *)value;

/*!
 Evaluates whether or not this and the given object are equal in value.
 
 @param obj The object to check for equality.
 
 @return Whether or not they are equal.
 */
- (BOOL)isEqual:(id)obj;

/*!
 @return The hash code of the array.
 */
- (NSUInteger)hash;

/*!
 Enable enumeration over the keys of the dictionary.
 
 @param state The context information.
 @param stackbuf The array of keys which should be iterated over.
 @param len The number of keys.
 
 @return The count.
 */
- (NSUInteger)countByEnumeratingWithState:(NSFastEnumerationState *)state objects:(_Null_unspecified __unsafe_unretained id * _Nonnull)stackbuf count:(NSUInteger)len;

/*!
 Fast enumeration of a dictionary only yields the entries key, to then get the value
 a second lookup is required. To avoid this, enumeration can be handled be providing
 a callback block.
 
 @param block The callback block.
 */
- (void)enumerateKeysAndObjectsUsingBlock:(SCMultiTypeDictionaryEnumerator)block;

@end

NS_ASSUME_NONNULL_END