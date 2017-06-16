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
 A mutable description of a CCSetPlayerDataRequest.

 This is not thread-safe and should typically only be used to create new instances
 of CCSetPlayerDataRequest.
 */
@interface CCSetPlayerDataRequestDesc : NSObject

/// The Custom Data Key to set value of. Maximum length is 50 characters.
@property (nonatomic) NSString *key;
	
/// The value the Custom Data Key should be set to. When serialised the maximum size
/// is 7kb.
@property (nonatomic) SCMultiTypeValue *value;
	
/// To enable conflict checking provide the previous WriteLock value, or "1" for the
/// initial write. If this value does not match the data store a
/// PlayerDataWriteConflict will be issued. If you don't wish to use conflict
/// checking don't provide this parameter - data will be written with no checking.
@property (nonatomic, nullable) NSString *writeLock;

/*!
 A convenience factory method for creating new instances of CCSetPlayerDataRequestDesc
 with the given properties.
 
 @param key The Custom Data Key to set value of. Maximum length is 50 characters.
 @param value The value the Custom Data Key should be set to. When serialised the maximum size
        is 7kb.	

 @return The new instance.
 */
+ (instancetype)setPlayerDataRequestDescWithKey:(NSString *)key value:(SCMultiTypeValue *)value;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param key The Custom Data Key to set value of. Maximum length is 50 characters.
 @param value The value the Custom Data Key should be set to. When serialised the maximum size
        is 7kb.	

 @return The initialised description.
 */
- (instancetype)initWithKey:(NSString *)key value:(SCMultiTypeValue *)value NS_DESIGNATED_INITIALIZER;

@end

NS_ASSUME_NONNULL_END
