//
//  Created by Ian Copland on 2015-08-27
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
 Provides the means to log messages to the console. There are three types of
 messages that can be logged: verbose, warnings and errors. warnings and errors
 will always be printed, however verbose messages will be omitted unless the
 logger was created with the verbose flag set.
 
 This is immutable after construction and is therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCLogging : NSObject

/// Describes whether or not verbose logging is enabled.
@property (readonly) BOOL verboseLoggingEnabled;

/*!
 Creates a new instance of SCLogging with verbose logging either enabled or
 disabled.
 
 @param verboseEnabled Whether or not verbose logging should be enabled.
 
 @return The new Logging instance.
 */
+ (instancetype)loggingWithVerboseLogging:(BOOL)verboseEnabled;

/*!
 Default init method cannot be called for this class.
 */
- (instancetype) __unavailable init;

/*!
 Initialises with verbose logging either enabled or disabled.
 
 @param verboseEnabled Whether or not verbose logging should be enabled.
 
 @return The new Logging instance.
 */
- (instancetype)initWithVerboseLogging:(BOOL)verboseEnabled NS_DESIGNATED_INITIALIZER;

/*!
 Logs a verbose message to standard out if verbose logging is enabled.
 
 @param message The message that should be logged.
 */
- (void)logVerboseMessage:(NSString *)message;

/*!
 Logs a warning message to the error output stream.
 
 @param message The message that should be logged.
 */
- (void)logWarningMessage:(NSString *)message;

/*!
 Logs an error message to the error output stream.
 
 @param message The message that should be logged.
 */
- (void)logErrorMessage:(NSString *)message;

@end

NS_ASSUME_NONNULL_END