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
 A mutable description of a CCMetricsEvent.

 This is not thread-safe and should typically only be used to create new instances
 of CCMetricsEvent.
 */
@interface CCMetricsEventDesc : NSObject

/// The type of custom event. This should map to a custom event type defined within
/// the ChilliConnect dashboard.
@property (nonatomic) NSString *type;
	
/// Date that indicates the local, device time that the event occurred. Format:
/// ISO8601 e.g. 2016-01-12T11:08:23.
@property (nonatomic) NSDate *date;
	
/// A number representing the player's in game level.
@property (nonatomic, nullable) NSNumber *userGrade;
	
/// A string indicating the test group this player belongs to.
@property (nonatomic, nullable) NSString *testGroup;
	
/// Object containing Key-Value pairs that map on to the custom event parameter
/// definitions for this event. All parameters are considered optional - however, any
/// parameters submitted for a custom event must be defined otherwise the event will
/// be considered invalid and not be processed.
@property (nonatomic, nullable) NSDictionary *parameters;
	
/// The number of times this event occurred. If not provided, this will be defaulted
/// to 1.
@property (nonatomic, nullable) NSNumber *count;

/*!
 A convenience factory method for creating new instances of CCMetricsEventDesc
 with the given properties.
 
 @param type The type of custom event. This should map to a custom event type defined within
        the ChilliConnect dashboard.	
 @param date Date that indicates the local, device time that the event occurred. Format:
        ISO8601 e.g. 2016-01-12T11:08:23.	

 @return The new instance.
 */
+ (instancetype)metricsEventDescWithType:(NSString *)type date:(NSDate *)date;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param type The type of custom event. This should map to a custom event type defined within
        the ChilliConnect dashboard.	
 @param date Date that indicates the local, device time that the event occurred. Format:
        ISO8601 e.g. 2016-01-12T11:08:23.	

 @return The initialised description.
 */
- (instancetype)initWithType:(NSString *)type date:(NSDate *)date NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
