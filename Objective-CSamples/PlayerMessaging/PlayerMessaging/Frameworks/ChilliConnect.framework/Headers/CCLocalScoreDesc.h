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
 A mutable description of a CCLocalScore.

 This is not thread-safe and should typically only be used to create new instances
 of CCLocalScore.
 */
@interface CCLocalScoreDesc : NSObject

/// The ChilliConnectID of the player.
@property (nonatomic) NSString *chilliConnectId;
	
/// The player's UserName.
@property (nonatomic, nullable) NSString *userName;
	
/// The player's DisplayName.
@property (nonatomic, nullable) NSString *displayName;
	
/// Date that indicates when the score was recorded (UTC). Format: ISO8601 e.g.
/// 2016-01-12T11:08:23.
@property (nonatomic) NSDate *date;
	
/// Any data associated with the score.
@property (nonatomic, nullable) SCMultiTypeValue *data;
	
/// The player's score.
@property (nonatomic) int32_t score;
	
/// The player's rank within the global leaderboard.
@property (nonatomic) int32_t globalRank;
	
/// The total number of scores within the the global leaderboard.
@property (nonatomic) int32_t globalTotal;
	
/// The player's rank within the results returned.
@property (nonatomic) int32_t localRank;

/*!
 A convenience factory method for creating new instances of CCLocalScoreDesc
 with the given properties.
 
 @param chilliConnectId The ChilliConnectID of the player.
 @param date Date that indicates when the score was recorded (UTC). Format: ISO8601 e.g.
        2016-01-12T11:08:23.	
 @param score The player's score.
 @param globalRank The player's rank within the global leaderboard.
 @param globalTotal The total number of scores within the the global leaderboard.
 @param localRank The player's rank within the results returned.

 @return The new instance.
 */
+ (instancetype)localScoreDescWithChilliConnectId:(NSString *)chilliConnectId date:(NSDate *)date score:(int32_t)score globalRank:(int32_t)globalRank globalTotal:(int32_t)globalTotal localRank:(int32_t)localRank;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param chilliConnectId The ChilliConnectID of the player.
 @param date Date that indicates when the score was recorded (UTC). Format: ISO8601 e.g.
        2016-01-12T11:08:23.	
 @param score The player's score.
 @param globalRank The player's rank within the global leaderboard.
 @param globalTotal The total number of scores within the the global leaderboard.
 @param localRank The player's rank within the results returned.

 @return The initialised description.
 */
- (instancetype)initWithChilliConnectId:(NSString *)chilliConnectId date:(NSDate *)date score:(int32_t)score globalRank:(int32_t)globalRank globalTotal:(int32_t)globalTotal localRank:(int32_t)localRank NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
