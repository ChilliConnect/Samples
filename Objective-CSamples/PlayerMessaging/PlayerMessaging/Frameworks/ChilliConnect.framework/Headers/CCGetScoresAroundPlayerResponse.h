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
 A container for information on the response from a CCGetScoresAroundPlayerRequest.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCGetScoresAroundPlayerResponse : NSObject

/// The current score page.
@property (readonly) int32_t page;
	
/// The player's rank within the global leaderboard. If the player does not have a
/// ranking within the leaderboard, this value will be -1.
@property (readonly) int32_t rank;
	
/// The index of the player's score within the returned Scores array. If the player
/// does not have a ranking within the leaderboard, this value will be -1.
@property (readonly) int32_t index;
	
/// The number of scores returned per page. The number of scores in a particular
/// response may be less than the page size if the last page is being returned.
@property (readonly) int32_t pageSize;
	
/// The total number of scores within the leaderboard.
@property (readonly) int32_t total;
	
/// A list of Scores within the supplied range.
@property (readonly) NSArray *scores;

/*!
 Convenience factory method for creating new instances of CCGetScoresAroundPlayerRequest.
 with the given dictionary.
 
 @param dictionary The dictionary containing the contents of a response body. This is 
        typically generated from Json.

 @return The new instance.
 */
+ (instancetype)getScoresAroundPlayerResponseWithDictionary:(NSDictionary *)dictionary;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given dictionary.
 
 @param dictionary The dictionary containing the contents of a response body. This is 
        typically generated from Json.

 @return The initialised response.
 */
- (instancetype)initWithDictionary:(NSDictionary *)dictionary NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
