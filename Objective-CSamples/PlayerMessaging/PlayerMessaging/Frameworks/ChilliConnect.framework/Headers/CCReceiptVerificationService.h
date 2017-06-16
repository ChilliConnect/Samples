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
 Contains detailed information about the response from the Amazon Receipt
 Verification Service.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCReceiptVerificationService : NSObject

/// The HTTP status returned from the Receipt Verification Service as described in
/// the Amazon Documentation at
/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS
/// Response Syntax'.
@property (readonly) int32_t httpCode;
	
/// A textual description of the returned HTTP status code.
@property (readonly) NSString *httpCodeDescription;
	
/// The full response from the Receipt Verification Service verification service as a
/// JSON encoded string. See the Amazon Documentation at
/// 'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS
/// Response Syntax' for more information.
@property (readonly) NSString *responseJson;

/*!
 A convenience factory method for creating new instances of CCReceiptVerificationService
 with the given properties.
 
 @param httpCode The HTTP status returned from the Receipt Verification Service as described in
        the Amazon Documentation at	
        'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS	
        Response Syntax'.	
 @param httpCodeDescription A textual description of the returned HTTP status code.
 @param responseJson The full response from the Receipt Verification Service verification service as a
        JSON encoded string. See the Amazon Documentation at	
        'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS	
        Response Syntax' for more information.	

 @return The new instance.
 */
+ (instancetype)receiptVerificationServiceWithHttpCode:(int32_t)httpCode httpCodeDescription:(NSString *)httpCodeDescription responseJson:(NSString *)responseJson;

/*!
 Convenience factory method for creating new instances of CCReceiptVerificationService
 from the contents of the given Json dictionary.
 
 @param dictionary The properties of the object in dictionary form. Typically this
        is created from Json.
 
 @return The new instance.
 */
+ (instancetype)receiptVerificationServiceWithJson:(NSDictionary *)dictionary;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param httpCode The HTTP status returned from the Receipt Verification Service as described in
        the Amazon Documentation at	
        'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS	
        Response Syntax'.	
 @param httpCodeDescription A textual description of the returned HTTP status code.
 @param responseJson The full response from the Receipt Verification Service verification service as a
        JSON encoded string. See the Amazon Documentation at	
        'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/verifying-receipts-in-iap-2.0#RVS	
        Response Syntax' for more information.	

 @return The initialised object.
 */
- (instancetype)initWithHttpCode:(int32_t)httpCode httpCodeDescription:(NSString *)httpCodeDescription responseJson:(NSString *)responseJson NS_DESIGNATED_INITIALIZER;

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
