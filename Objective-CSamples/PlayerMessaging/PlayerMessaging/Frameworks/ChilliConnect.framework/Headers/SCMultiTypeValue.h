//
//  Created by Ian Copland on 2016-02-01
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

/*!
 An enum describing the various possible types that can be contained in a 
 MultiTypeValue.
 */
typedef NS_ENUM(NSUInteger, SCMultiTypeValueType) {
    
    // The contained value is of type NSNull.
    SCMultiTypeValueTypeNull,
    
    /// The contained value is of type NSNumber.
    SCMultiTypeValueTypeNumber,
    
    /// The contained value is of type NSString.
    SCMultiTypeValueTypeString,
    
    /// The contained value is of type SCMultiTypeArray.
    SCMultiTypeValueTypeArray,
    
    /// The contained value is of type SCMultiTypeDictionary.
    SCMultiTypeValueTypeDictionary
};

/*!
 A container for a single value which can be one of several different types. This 
 is required to allow some basic storage of heterogeneous types, and generic Json
 serialisation of those types.
 
 This is immutable and therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCMultiTypeValue : NSObject

/// Describes the type of value this contains.
@property (readonly) SCMultiTypeValueType type;

/// Whether or not the contained value is an NSNull.
@property (readonly, nonatomic) BOOL isNull;

/// Whether or not the contained value is an NSNumber.
@property (readonly, nonatomic) BOOL isNumber;

/// Whether or not the contained value is an NSString.
@property (readonly, nonatomic) BOOL isString;

/// Whether or not the contained value is a SCMultiTypeArray.
@property (readonly, nonatomic) BOOL isArray;

/// Whether or not the contained value is a SCMultiTypeDictionary.
@property (readonly, nonatomic) BOOL isDictionary;

/// The contained value as an NSNumber. If the type is not an NSNumber, an error will
/// occur.
@property (readonly, nonatomic) NSNumber *numberValue;

/// The contained value as an NSString. If the type is not an NSString, an error
/// will occur.
@property (readonly, nonatomic) NSString *stringValue;

/// The contained value as a SCMultiTypeArray. If the type is not a SCMultiTypeArray,
/// an error will occur.
@property (readonly, nonatomic) SCMultiTypeArray *arrayValue;

/// The contained value as a SCMultiTypeDictionary. If the type is not a
/// SCMultiTypeDictionary, an error will occur.
@property (readonly, nonatomic) SCMultiTypeDictionary *dictionaryValue;

/*!
 Creates a new instance of SCMultiTypeValue which represents a null object.
 
 @return The new instance.
 */
+ (SCMultiTypeValue *)null;

/*!
 Creates a new SCMultiTypeValue instance containing the given value.
 
 @param value The value that the new instance should contain.
 
 @return The new instance.
 */
+ (SCMultiTypeValue *)multiTypeValueWithNumber:(NSNumber *)value;

/*!
 Creates a new SCMultiTypeValue instance containing the given value.
 
 @param value The value that the new instance should contain.
 
 @return The new instance.
 */
+ (SCMultiTypeValue *)multiTypeValueWithString:(NSString *)value;

/*!
 Creates a new SCMultiTypeValue instance containing the given value.
 
 @param value The value that the new instance should contain.
 
 @return The new instance.
 */
+ (SCMultiTypeValue *)multiTypeValueWithArray:(SCMultiTypeArray *)value;

/*!
 Creates a new SCMultiTypeValue instance containing the given value.
 
 @param value The value that the new instance should contain.
 
 @return The new instance.
 */
+ (SCMultiTypeValue *)multiTypeValueWithDictionary:(SCMultiTypeDictionary *)value;

/*!
 Creates a new SCMultiTypeValue instance with the given Json complient
 type. This must be a type which is compatible with json: NSNull, NSNumber,
 NSString, NSArray<id> or NSDictionary<NSString *, id>. If the type is
 not valid json, then an error will occur.
 
 @param value The json compatible type.
 
 @return The new instance.
 */
+ (SCMultiTypeValue *)multiTypeValueWithJson:(id)value;

/*!
 Initialises the SCMultiTypeValue instance to null.
 
 @return The initialised SCMultiTypeValue.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

/*!
 Initialises the SCMultiTypeValue instance with the given value.
 
 @param value The value that the instance should contain.
 
 @return The initialised SCMultiTypeValue.
 */
- (instancetype)initWithNumber:(NSNumber *)value NS_DESIGNATED_INITIALIZER;

/*!
 Initialises the SCMultiTypeValue instance with the given value.
 
 @param value The value that the instance should contain.
 
 @return The initialised SCMultiTypeValue.
 */
- (instancetype)initWithString:(NSString *)value NS_DESIGNATED_INITIALIZER;

/*!
 Initialises the SCMultiTypeValue instance with the given value.
 
 @param value The value that the instance should contain.
 
 @return The initialised SCMultiTypeValue.
 */
- (instancetype)initWithArray:(SCMultiTypeArray *)value NS_DESIGNATED_INITIALIZER;

/*!
 Initialises the SCMultiTypeValue instance with the given value.
 
 @param value The value that the instance should contain.
 
 @return The initialised SCMultiTypeValue.
 */
- (instancetype)initWithDictionary:(SCMultiTypeDictionary *)value NS_DESIGNATED_INITIALIZER;

/*!
 Serialises the object this describes to a Json compliant type. This will be one of:
 NSNumber, NSString, NSArray<id> or NSDictionary<NSString *, id>. Values stored in an 
 NSArray or NSDictionary will also be one ofthe Json compliant types.
 
 @return The serialised object.
 */
- (id)serialise;

/*!
 Evaluates whether or not this and the given SCMultiTypeValue are equal in value.
 
 @param value The value to check for equality.
 
 @return Whether or not they are equal.
 */
- (BOOL)isEqualToMultiTypeValue:(SCMultiTypeValue *)value;

/*!
 Evaluates whether or not this and the given object are equal in value.
 
 @param obj The object to check for equality.
 
 @return Whether or not they are equal.
 */
- (BOOL)isEqual:(id)obj;

/*!
 @return The hash code for this value.
 */
- (NSUInteger)hash;

@end

NS_ASSUME_NONNULL_END