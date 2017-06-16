//
//  Created by Ian Copland on 2015-11-03
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
 A convenience class which provides a means to convert between an NSDate object
 and ISO-8601 format date strings.
 
 This is stateless and is therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCDateFormatter : NSObject

/*!
 Creates a new instance of NSDate from a string. The string must represent a
 IS0-8601 date.
 
 @param dateString A string representation of an ISO-8601 date.
 
 @return The new NSDate instance.
 */
+ (NSDate *)dateFromString:(NSString *)dateString;

/*!
 Converts the given NSDate to a date string in ISO-8601 format.
 
 @param date A NSDate instance.
 
 @return The date string in ISO-8601 format.
 */
+ (NSString *)stringFromDate:(NSDate *)date;

@end

NS_ASSUME_NONNULL_END