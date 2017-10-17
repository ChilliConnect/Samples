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

@class SCMultiTypeDictionary;
@class SCMultiTypeValue;

/*!
 An immutable array which can contain values of certain heterogeneous types. The 
 array can be serialised to a json compliant NSArray<id>.
 
 Typically this is generated via a SCMultiTypeArrayBuilder; alternatively it can 
 be generated from a json compliant NSArray<id>.
 
 As this is immutable, it is thread-safe.
 
 @author Ian Copland
 */
@interface SCMultiTypeArray : NSObject<NSFastEnumeration>

/// The number of elements in the array.
@property (readonly, nonatomic) NSUInteger count;

/*!
 Creates a new empty SCMultiTypeArray instance.
 
 @return The new instance.
 */
+ (SCMultiTypeArray *) multiTypeArray;

/*!
 Creates a new SCMultiTypeArray from the given list of SCMultiTypeValues. Typically this
 isn't called directly, instead this is generated via a SCMultiTypeArrayBuilder.
 
 @param values The values this SCMultiTypeArray should contain.
 
 @return The new instance.
 */
+ (SCMultiTypeArray *) multiTypeArrayWithValues:(NSArray<SCMultiTypeValue *> *)values;

/*!
 Initialises a new SCMultiTypeArray from the given Json compliant array. The array must
 contain only Json compatible types: NSNull, NSNumber, NSString, NSArray<id> or
 NSDictionary<NSString *, id>. Contained Arrays or Dictionaries must also contain
 only Json compatible types. If the type is invalid an error will occur.
 
 @param array A json compatible array.
 
 @return The new instance.
 */
+ (SCMultiTypeArray *) multiTypeArrayWithJson:(NSArray<id> *)array;

/*!
 Initialises a new empty SCMultiTypeArray instance.
 
 @return The initialised empty SCMultiTypeArray.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

/*!
 Initialises a new SCMultiTypeArray instance with the given values.
 
 @param values The values this SCMultiTypeArray should contain.
 
 @return The initialised SCMultiTypeArray.
 */
- (instancetype)initWithValues:(NSArray<SCMultiTypeValue *> *)values NS_DESIGNATED_INITIALIZER;

/*!
 @param value The value.
 
 @return Whether or not the array contains the given value.
 */
- (BOOL)containsObject:(SCMultiTypeValue *)value;

/*!
 @param value The value.
 
 @return Whether or not the array contains the given value.
 */
- (BOOL)containsNumber:(NSNumber *)value;

/*!
 @param value The value.
 
 @return Whether or not the array contains the given value.
 */
- (BOOL)containsString:(NSString *)value;

/*!
 @param value The value.
 
 @return Whether or not the array contains the given value.
 */
- (BOOL)containsArray:(SCMultiTypeArray *)value;

/*!
 @param value The value.
 
 @return Whether or not the array contains the given value.
 */
- (BOOL)containsDictionary:(SCMultiTypeDictionary *)value;

/*!
 Returns the value at the requested index. If the index is out of bounds
 an error will occur.
 
 @param index The index of the value.
 
 @return The value.
 */
- (SCMultiTypeValue *)objectAtIndex:(int32_t)index;

/*!
 Returns the NSNumber at the requested index. If the index is out of bounds or
 the value is the wrong type, an error will occur.
 
 @param index The index of the value.
 
 @return The value.
 */
- (NSNumber *)numberAtIndex:(int32_t)index;

/*!
 Returns the string at the requested index. If the index is out of bounds or
 the value is the wrong type, an error will occur.
 
 @param index The index of the value.
 
 @return The value.
 */
- (NSString *)stringAtIndex:(int32_t)index;

/*!
 Returns the array at the requested index. If the index is out of bounds or
 the value is the wrong type, an error will occur.
 
 @param index The index of the value.
 
 @return The value.
 */
- (SCMultiTypeArray *)arrayAtIndex:(int32_t)index;

/*!
 Returns the dictionary at the requested index. If the index is out of bounds or
 the value is the wrong type, an error will occur.
 
 @param index The index of the value.
 
 @return The value.
 */
- (SCMultiTypeDictionary *)dictionaryAtIndex:(int32_t)index;

/*!
 Serialises the array this describes to a Json compliant NSArray<id>. All elements
 in the array with be Json compatible types: NSNull, NSNumber, NSString, 
 NSArray<id> or NSDictionary<NSString *, id>.
 
 @return The serialised Array.
 */
- (NSArray<id> *)serialise;

/*!
 Evaluates whether or not this and the given SCMultiTypeArray are equal in value.
 
 @param value The value to check for equality.
 
 @return Whether or not they are equal.
 */
- (BOOL)isEqualToMultiTypeArray:(SCMultiTypeArray *)value;

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
 Enable enumeration over the array.
 
 @param state The context information.
 @param stackbuf The array of objects which should be iterated over.
 @param len The number of objects.
 
 @return The count.
 */
- (NSUInteger)countByEnumeratingWithState:(NSFastEnumerationState *)state objects:(_Null_unspecified __unsafe_unretained id * _Nonnull)stackbuf count:(NSUInteger)len;

@end

NS_ASSUME_NONNULL_END