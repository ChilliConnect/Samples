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
 A block describing a successful callback from validateAmazonIap.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCValidateAmazonIapResponseCallback)(CCValidateAmazonIapRequest *request, CCValidateAmazonIapResponse *response);	
		
/*!
 A block describing a successful callback from validateAppleIap.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCValidateAppleIapResponseCallback)(CCValidateAppleIapRequest *request, CCValidateAppleIapResponse *response);	
		
/*!
 A block describing a successful callback from validateGoogleIap.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCValidateGoogleIapResponseCallback)(CCValidateGoogleIapRequest *request, CCValidateGoogleIapResponse *response);	
	
/*!
 A block describing an error callback from validateAmazonIap.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCValidateAmazonIapErrorCallback)(CCValidateAmazonIapRequest *request, CCValidateAmazonIapError *error);
	
/*!
 A block describing an error callback from validateAppleIap.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCValidateAppleIapErrorCallback)(CCValidateAppleIapRequest *request, CCValidateAppleIapError *error);
	
/*!
 A block describing an error callback from validateGoogleIap.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCValidateGoogleIapErrorCallback)(CCValidateGoogleIapRequest *request, CCValidateGoogleIapError *error);

/*!
 The ChillConnect In-App Purchase Validation module. Provides the means to
 validate in-app purchases using Amazon Receipt Validation Service, Apple AppStore
 and Google Play Store.

 This is thread-safe.
 */
@interface CCInAppPurchase : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCInAppPurchase
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)inAppPurchaseWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties. with the given logger, task scheduler 
 and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The initialised CCInAppPurchase.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Validate a Receipt from a successful purchase on an Amazon device.
 
 @param receiptId ReceiptID returned from the Amazon App Store as a result of a successful
        purchase. See the Amazon Documentation at	
        'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/implementing-iap-2.0'	
        for more information to on how to access this value from your app.	
 @param userId UserID returned from the Amazon App Store as a result of a successful purchase.
        See the Amazon Documentation at	
        'https://developer.amazon.com/public/apis/earn/in-app-purchasing/docs-v2/implementing-iap-2.0'	
        for more information to on how to access this value from your app.	
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)validateAmazonIapWithReceiptId:(NSString *)receiptId userId:(NSString *)userId successCallback:(CCValidateAmazonIapResponseCallback)successCallback errorCallback:(CCValidateAmazonIapErrorCallback)errorCallback;

/*!
 Validate a Receipt from a successful purchase on an Apple device.
 
 @param receipt Receipt data returned from the App Store as a result of a successful purchase.
        This should be <code>base64</code> encoded.	
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)validateAppleIapWithReceipt:(NSString *)receipt successCallback:(CCValidateAppleIapResponseCallback)successCallback errorCallback:(CCValidateAppleIapErrorCallback)errorCallback;

/*!
 Validate a Receipt from a successful purchase on a Google device.
 
 @param purchaseData A JSON encoded string returned from a successful in app billing purchase. See the
        Google Documentation at	
        'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'	
        on how to access this value from your app.	
 @param purchaseDataSignature A signature of the PurchaseData returned from a successful in app billing
        purchase. See the Google Documentation at	
        'http://developer.android.com/google/play/billing/billing_integrate.html#Purchase'	
        on how to access this value from your app.	
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)validateGoogleIapWithPurchaseData:(NSString *)purchaseData purchaseDataSignature:(NSString *)purchaseDataSignature successCallback:(CCValidateGoogleIapResponseCallback)successCallback errorCallback:(CCValidateGoogleIapErrorCallback)errorCallback;

@end

NS_ASSUME_NONNULL_END
