//
//  Created by Ian Copland on 2016-01-20
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

/*!
 A callback for serialising containers. This will be called for each element in the
 container and expects a json type to be returned which can be included in the
 output serialised container.
 
 @param element An element found in the container which is currently being serialised.
        This can be of any type.
 
 @return The serialised element. This must be a valid JSON type, i.e, NSNumber,
         NSString, NSArray, NSDictionary or NSNull.
 */
typedef _Nonnull id (^SCSerialiseElementCallback)(id element);

/*!
 A callback for deserialising containers. This will be called for each element in the
 JSON container and expects the deserialised type to be returned which can be included
 in the output deserialised container.
 
 @param element An element found in the container which is currently being deserialised.
        This must be a valid JSON type, i.e, NSNumber, NSString, NSArray, NSDictionary or NSNull.
 
 @return The deserialised element. This can be any type.
 */
typedef _Nonnull id (^SCDeserialiseElementCallback)(id element);

/*!
 A convenience class which provides serialisation and deserialisation of
 non-standard types to JSON format.
 
 This is stateless and is therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCJsonSerialisation : NSObject

/*!
 Serialises a date object to string.
 
 @param date The date object to be serialised.
 
 @return The serialised date as a string.
 */
+ (NSString *)serialiseDate:(NSDate *)date;

/*!
 Serialised a list to a Json compatible NSArray. A callback is provided for
 serialising each element in the input list.
 
 @param list The list to be deserialised.
 @param elementCallback The callback for serialising the elements in the list.
 
 @return The serialised list.
 */
+ (NSArray *)serialiseList:(NSArray *)list elementCallback:(SCSerialiseElementCallback)elementCallback;

/*!
 Serialised a map to a Json compatible NSDictionary. A callback is provided for
 serialising each element in the input map.
 
 @param map The map to be deserialised.
 @param elementCallback The callback for serialising the elements in the map.
 
 @return The serialised map.
 */
+ (NSDictionary *)serialiseMap:(NSDictionary *)map elementCallback:(SCSerialiseElementCallback)elementCallback;

/*!
 Deserialises a date string to a NSDate object. If the string is not a valid
 date string then this will error.
 
 @param dateString The date string to be deserialised.
 
 @return The serialised date as a string.
 */
+ (NSDate *)deserialiseDate:(NSString *)dateString;

/*!
 Deserialised a list from a Json compatible NSArray. A callback is provided for
 deserialising each element in the NSArray.
 
 @param array The Json compatible NSArray to be deserialised.
 @param elementCallback The callback for deserialising the elements in the array.
 
 @return The deserialised list.
 */
+ (NSArray *)deserialiseList:(NSArray *)array elementCallback:(SCDeserialiseElementCallback)elementCallback;

/*!
 Deserialised a map from a Json compatible NSDictionary. A callback is provided for
 deserialising each element in the NSDictionary.
 
 @param dictionary The Json compatible NSDictionary to be deserialised.
 @param elementCallback The callback for deserialising the elements in the dictionary.
 
 @return The deserialised map.
 */
+ (NSDictionary *)deserialiseMap:(NSDictionary *)dictionary elementCallback:(SCDeserialiseElementCallback)elementCallback;

@end

NS_ASSUME_NONNULL_END