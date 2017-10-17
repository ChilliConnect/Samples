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
 A mutable description of a CCAddScoreRequest.

 This is not thread-safe and should typically only be used to create new instances
 of CCAddScoreRequest.
 */
@interface CCAddScoreRequestDesc : NSObject

/// The score to post.
@property (nonatomic) int32_t score;
	
/// The Key that identifies the leaderboard.
@property (nonatomic) NSString *key;
	
/// Any arbitrary data to associated with the score When serialised the maximum size
/// is 7kb. Note that Data will only be stored when the score is updated. For example
/// a Leaderboard configured with Update Type Highest will only be written to with a
/// score that is higher than the currently saved score.
@property (nonatomic, nullable) SCMultiTypeValue *data;

/*!
 A convenience factory method for creating new instances of CCAddScoreRequestDesc
 with the given properties.
 
 @param score The score to post.
 @param key The Key that identifies the leaderboard.

 @return The new instance.
 */
+ (instancetype)addScoreRequestDescWithScore:(int32_t)score key:(NSString *)key;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param score The score to post.
 @param key The Key that identifies the leaderboard.

 @return The initialised description.
 */
- (instancetype)initWithScore:(int32_t)score key:(NSString *)key NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
