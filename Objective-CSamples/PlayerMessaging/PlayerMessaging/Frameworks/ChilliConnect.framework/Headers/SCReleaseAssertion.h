//
//  Created by Ian Copland on 2015-11-05
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
 A convenience function for user assertions. Use of this is preferred over direct
 user of the SCUserAssertion class.
 
 Checks that the given assumption is true. If not, an InvalidStateException will be
 thrown.
 
 @param assumption The assumption that will be confirmed.
 @param message The message that will be given if the assumption is incorrect.
 */
void SCReleaseAssert(BOOL assumption, NSString *message);

/*!
 NSAssert(...) is useful for testing the assumptions in code, while ensuring these
 assumptions are not checked during a release build. In a framework it can be useful 
 to check assumptions for things that the API user can affect, however this can be
 problematic as the API user is using the release version of the framework.
 
 SCReleaseAssertion provides an alternate to NSAssert which can be used in this case as
 it will not be omitted from release builds. Failure will still throw an exception
 but only includes information relevant to the user, i.e line numbers are omitted.
 
 This is not intended to be a replacement for NSAssert. It should be used in cases
 where the assertion will be affected by API user input, any asserts checking internal
 logic should still use NSAssert.
 
 The SCReleaseAssert(...) C function is preferred over direct use of this class.
 
 This is stateless and is therefore thread-safe.
 
 @author Ian Copland
 */
@interface SCReleaseAssertion : NSObject

/*!
 Checks that the given assumption is true. If not, an InvalidStateException will be
 thrown.
 
 @param assumption The assumption that will be confirmed.
 @param message The message that will be given if the assumption is incorrect.
 */
+ (void)check:(BOOL)assumption message:(NSString *)message;

@end

NS_ASSUME_NONNULL_END