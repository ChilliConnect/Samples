//
//  Created by Ian Copland on 2015-11-05
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Limited
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

/*!
 A convenience class that provides a type-safe way of getting values from a 
 dictionary.
 
 This is stateless and is therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCDictionary : NSObject

/*!
 Reads a bool from the dictionary for the given key. If the key doesn't exist
 or the value is of the wrong type, then this will error.
 
 @param key The key to look up.
 @param dictionary The dictionary to look up.
 
 @return The output value.
 */
+ (BOOL)boolForKey:(NSString *)key dictionary:(NSDictionary *)dictionary;

/*!
 Reads an int from the dictionary for the given key. If the key doesn't exist
 or the value is of the wrong type, then this will error.
 
 @param key The key to look up.
 @param dictionary The dictionary to look up.
 
 @return The output value.
 */
+ (int32_t)intForKey:(NSString *)key dictionary:(NSDictionary *)dictionary;

/*!
 Reads a long from the dictionary for the given key. If the key doesn't exist
 or the value is of the wrong type, then this will error.
 
 @param key The key to look up.
 @param dictionary The dictionary to look up.
 
 @return The output value.
 */
+ (int64_t)longForKey:(NSString *)key dictionary:(NSDictionary *)dictionary;

/*!
 Reads a float from the dictionary for the given key. If the key doesn't exist
 or the value is of the wrong type, then this will error.
 
 @param key The key to look up.
 @param dictionary The dictionary to look up.
 
 @return The output value.
 */
+ (float)floatForKey:(NSString *)key dictionary:(NSDictionary *)dictionary;

/*!
 Reads a string from the dictionary for the given key. If the key doesn't exist
 or the value is of the wrong type, then this will error.
 
 @param key The key to look up.
 @param dictionary The dictionary to look up.
 
 @return The output value.
 */
+ (NSString *)stringForKey:(NSString *)key dictionary:(NSDictionary *)dictionary;

/*!
 Reads a date from the dictionary for the given key. If the key doesn't exist
 or the value is of the wrong type, then this will error.
 
 @param key The key to look up.
 @param dictionary The dictionary to look up.
 
 @return The output value.
 */
+ (NSDate *)dateForKey:(NSString *)key dictionary:(NSDictionary *)dictionary;

/*!
 Reads a Json Object from the dictionary for the given key. If the key doesn't exist
 or the value is of the wrong type, then this will error.
 
 @param key The key to look up.
 @param dictionary The dictionary to look up.
 
 @return The output value.
 */
+ (NSDictionary *)jsonObjectForKey:(NSString *)key dictionary:(NSDictionary *)dictionary;

@end

NS_ASSUME_NONNULL_END