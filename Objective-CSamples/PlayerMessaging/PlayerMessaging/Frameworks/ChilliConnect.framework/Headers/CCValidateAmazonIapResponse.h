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
 A container for information on the response from a CCValidateAmazonIapRequest.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCValidateAmazonIapResponse : NSObject

/// True if the receipt data was successfully verified and has not been seen verified
/// before by ChilliConnect. Otherwise, false. In the case of false, the Status field
/// contains additional information on reason the receipt could not be verified.
@property (readonly) BOOL valid;
	
/// Detailed status for the receipt. This can be one of: Valid: The purchase was
/// valid; InvalidVerificationFailed: The Amazon Receipt Verification Service
/// returned that the provided receipt data was not valid; InvalidSeenBefore: The
/// Receipt has previously been sent to ChilliConnect by the same player and
/// validated; InvalidVerifiedForAnotherPlayer: The Receipt has previously been sent
/// to ChilliConnect by a different player and validated.
@property (readonly) NSString *status;
	
/// Contains detailed information about the response from the Amazon Receipt
/// Verification Service.
@property (readonly) CCReceiptVerificationService *receiptVerificationService;

/*!
 Convenience factory method for creating new instances of CCValidateAmazonIapRequest.
 with the given dictionary.
 
 @param dictionary The dictionary containing the contents of a response body. This is 
        typically generated from Json.

 @return The new instance.
 */
+ (instancetype)validateAmazonIapResponseWithDictionary:(NSDictionary *)dictionary;

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
