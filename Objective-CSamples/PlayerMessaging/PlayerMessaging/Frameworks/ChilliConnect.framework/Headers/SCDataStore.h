//
//  Created by Ian Copland on 2015-10-15
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

#import "ForwardDeclarations.h"

NS_ASSUME_NONNULL_BEGIN

/*!
 Provides the means to store values with an associated key that will persist
 accross the current session.
 
 This is thread-safe.
 
 @author Ian Copland
 */
@interface SCDataStore : NSObject {
@private
    NSMutableDictionary *_dataStore;
}

@property (readonly) NSString *name;

/*!
 A factory method for creating a new instance of data store with the given
 name.

 @return The new data store instance.
 */
+ (instancetype)dataStore;


/*!
 Initialises an instance of SCDataStore.
 
 @return The initialised instance.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

/*!
 Reads the value from the data store with the given key. If the key does
 not exist or it is of the wrong type then an error will occur.
 
 @param key The string key for the requested data.
 
 @return The output value.
 */
- (BOOL)boolForKey:(NSString *)key;

/*!
 Reads the value from the data store with the given key. If the key does
 not exist or it is of the wrong type then an error will occur.
 
 @param key The string key for the requested data.
 
 @return The output value.
 */
- (int32_t)intForKey:(NSString *)key;

/*!
 Reads the value from the data store with the given key. If the key does
 not exist or it is of the wrong type then an error will occur.
 
 @param key The string key for the requested data.
 
 @return The output value.
 */
- (int64_t)longForKey:(NSString *)key;

/*!
 Reads the value from the data store with the given key. If the key does
 not exist or it is of the wrong type then an error will occur.
 
 @param key The string key for the requested data.
 
 @return The output value.
 */
- (float)floatForKey:(NSString *)key;

/*!
 Reads the value from the data store with the given key. If the key does
 not exist or it is of the wrong type then an error will occur.
 
 @param key The string key for the requested data.
 
 @return The output value. Will never be nil.
 */
- (NSString *)stringForKey:(NSString *)key;

/*!
 Reads the value from the data store with the given key. If the key does
 not exist or it is of the wrong type then an error will occur.
 
 @param key The string key for the requested data.
 
 @return The output value. Will never be nil.
 */
- (NSDate *)dateForKey:(NSString *)key;

/*!
 Reads the value from the data store with the given key. If the key does
 not exist or it is of the wrong type then an error will occur.
 
 @param key The string key for the requested data.
 
 @return The output value. Will never be nil.
 */
- (NSDictionary *)jsonObjectForKey:(NSString *)key;

/*!
 Stores a new value in the data store.
 
 If data already exists for the given key, it will be overwritten.
 
 @param value The value to set.
 @param key The string key that should be used.
 */
- (void)setBool:(BOOL)value forKey:(NSString *)key;

/*!
 Stores a new value in the data store.
 
 If data already exists for the given key, it will be overwritten.
 
 @param value The value to set.
 @param key The string key that should be used.
 */
- (void)setInt:(int32_t)value forKey:(NSString *)key;

/*!
 Stores a new value in the data store.
 
 If data already exists for the given key, it will be overwritten.
 
 @param value The value to set.
 @param key The string key that should be used.
 */
- (void)setLong:(int64_t)value forKey:(NSString *)key;

/*!
 Stores a new value in the data store.
 
 If data already exists for the given key, it will be overwritten.
 
 @param value The value to set.
 @param key The string key that should be used.
 */
- (void)setFloat:(float)value forKey:(NSString *)key;

/*!
 Stores a new value in the data store.
 
 If data already exists for the given key, it will be overwritten.
 
 @param value The value to set.
 @param key The string key that should be used.
 */
- (void)setString:(NSString *)value forKey:(NSString *)key;

/*!
 Stores a new value in the data store.
 
 If data already exists for the given key, it will be overwritten.
 
 @param value The value to set.
 @param key The string key that should be used.
 */
- (void)setDate:(NSDate *)value forKey:(NSString *)key;

/*!
 Stores a new value in the data store.
 
 If data already exists for the given key, it will be overwritten.
 
 @param value The value to set.
 @param key The string key that should be used.
 */
- (void)setJsonObject:(NSDictionary *)value forKey:(NSString *)key;

@end

NS_ASSUME_NONNULL_END
