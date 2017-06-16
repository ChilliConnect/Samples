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
 A container for information on a single Metrics event.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCMetricsEvent : NSObject

/// The type of custom event. This should map to a custom event type defined within
/// the ChilliConnect dashboard.
@property (readonly) NSString *type;
	
/// Date that indicates the local, device time that the event occurred. Format:
/// ISO8601 e.g. 2016-01-12T11:08:23.
@property (readonly) NSDate *date;
	
/// A number representing the player's in game level.
@property (readonly, nullable) NSNumber *userGrade;
	
/// A string indicating the test group this player belongs to.
@property (readonly, nullable) NSString *testGroup;
	
/// Object containing Key-Value pairs that map on to the custom event parameter
/// definitions for this event. All parameters are considered optional - however, any
/// parameters submitted for a custom event must be defined otherwise the event will
/// be considered invalid and not be processed.
@property (readonly, nullable) NSDictionary *parameters;
	
/// The number of times this event occurred. If not provided, this will be defaulted
/// to 1.
@property (readonly, nullable) NSNumber *count;

/*!
 A convenience factory method for creating new instances of CCMetricsEvent
 with the given description.
 
 @param desc The description to build the new instance from.

 @return The new instance.
 */
+ (instancetype)metricsEventWithDesc:(CCMetricsEventDesc *)desc;

/*!
 Convenience factory method for creating new instances of CCMetricsEvent
 from the contents of the given Json dictionary.
 
 @param dictionary The properties of the object in dictionary form. Typically this
        is created from Json.
 
 @return The new instance.
 */
+ (instancetype)metricsEventWithJson:(NSDictionary *)dictionary;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given description.
 
 @param desc The description to build the new instance from.

 @return The initialised object.
 */
- (instancetype)initWithDesc:(CCMetricsEventDesc *)desc NS_DESIGNATED_INITIALIZER;

/*!
 Initialise with the contents of the given dictionary.
 
 @param dictionary The properties of the object in dictionary form. Typically this
        is created from Json.
 
 @return The initialised object.
 */
- (instancetype)initWithDictionary:(NSDictionary *)dictionary NS_DESIGNATED_INITIALIZER;

/*!
 Serialises all properties. The output will be a dictionary containing the objects 
 properties in a form that can easily be converted to Json. 
 
 @return The serialised object in dictionary form. 
 */
 - (NSDictionary *)serialise;
 
@end

NS_ASSUME_NONNULL_END
