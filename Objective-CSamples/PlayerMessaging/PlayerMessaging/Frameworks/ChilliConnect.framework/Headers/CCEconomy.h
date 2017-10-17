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
 A block describing a successful callback from getCurrencyBalance.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetCurrencyBalanceResponseCallback)(CCGetCurrencyBalanceRequest *request, CCGetCurrencyBalanceResponse *response);	
		
/*!
 A block describing a successful callback from setCurrencyBalance.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCSetCurrencyBalanceResponseCallback)(CCSetCurrencyBalanceRequest *request, CCSetCurrencyBalanceResponse *response);	
		
/*!
 A block describing a successful callback from convertCurrency.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCConvertCurrencyResponseCallback)(CCConvertCurrencyRequest *request, CCConvertCurrencyResponse *response);	
		
/*!
 A block describing a successful callback from addCurrencyBalance.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCAddCurrencyBalanceResponseCallback)(CCAddCurrencyBalanceRequest *request, CCAddCurrencyBalanceResponse *response);	
		
/*!
 A block describing a successful callback from removeCurrencyBalance.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCRemoveCurrencyBalanceResponseCallback)(CCRemoveCurrencyBalanceRequest *request, CCRemoveCurrencyBalanceResponse *response);	
		
/*!
 A block describing a successful callback from getInventory.
 
 @param response The response from the server.
 */
typedef void (^CCGetInventoryResponseCallback)(CCGetInventoryResponse *response);	
		
/*!
 A block describing a successful callback from getInventoryForKeys.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetInventoryForKeysResponseCallback)(CCGetInventoryForKeysRequest *request, CCGetInventoryForKeysResponse *response);	
		
/*!
 A block describing a successful callback from getInventoryForItemIds.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetInventoryForItemIdsResponseCallback)(CCGetInventoryForItemIdsRequest *request, CCGetInventoryForItemIdsResponse *response);	
		
/*!
 A block describing a successful callback from addInventoryItem.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCAddInventoryItemResponseCallback)(CCAddInventoryItemRequest *request, CCAddInventoryItemResponse *response);	
		
/*!
 A block describing a successful callback from updateInventoryItem.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCUpdateInventoryItemResponseCallback)(CCUpdateInventoryItemRequest *request, CCUpdateInventoryItemResponse *response);	
		
/*!
 A block describing a successful callback from removeInventoryItem.
 
 @param request The original request.
 */
typedef void (^CCRemoveInventoryItemResponseCallback)(CCRemoveInventoryItemRequest *request);	
		
/*!
 A block describing a successful callback from makeVirtualPurchase.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCMakeVirtualPurchaseResponseCallback)(CCMakeVirtualPurchaseRequest *request, CCMakeVirtualPurchaseResponse *response);	
		
/*!
 A block describing a successful callback from redeemAmazonIap.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCRedeemAmazonIapResponseCallback)(CCRedeemAmazonIapRequest *request, CCRedeemAmazonIapResponse *response);	
		
/*!
 A block describing a successful callback from redeemAppleIap.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCRedeemAppleIapResponseCallback)(CCRedeemAppleIapRequest *request, CCRedeemAppleIapResponse *response);	
		
/*!
 A block describing a successful callback from redeemGoogleIap.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCRedeemGoogleIapResponseCallback)(CCRedeemGoogleIapRequest *request, CCRedeemGoogleIapResponse *response);	
		
/*!
 A block describing a successful callback from getConversionDefinitions.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetConversionDefinitionsResponseCallback)(CCGetConversionDefinitionsRequest *request, CCGetConversionDefinitionsResponse *response);	
		
/*!
 A block describing a successful callback from getCurrencyDefinitions.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetCurrencyDefinitionsResponseCallback)(CCGetCurrencyDefinitionsRequest *request, CCGetCurrencyDefinitionsResponse *response);	
		
/*!
 A block describing a successful callback from getInventoryDefinitions.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetInventoryDefinitionsResponseCallback)(CCGetInventoryDefinitionsRequest *request, CCGetInventoryDefinitionsResponse *response);	
		
/*!
 A block describing a successful callback from getMetadataDefinitions.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetMetadataDefinitionsResponseCallback)(CCGetMetadataDefinitionsRequest *request, CCGetMetadataDefinitionsResponse *response);	
		
/*!
 A block describing a successful callback from getRealMoneyPurchaseDefinitions.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetRealMoneyPurchaseDefinitionsResponseCallback)(CCGetRealMoneyPurchaseDefinitionsRequest *request, CCGetRealMoneyPurchaseDefinitionsResponse *response);	
		
/*!
 A block describing a successful callback from getVirtualPurchaseDefinitions.
 
 @param request The original request.
 @param response The response from the server.
 */
typedef void (^CCGetVirtualPurchaseDefinitionsResponseCallback)(CCGetVirtualPurchaseDefinitionsRequest *request, CCGetVirtualPurchaseDefinitionsResponse *response);	
	
/*!
 A block describing an error callback from getCurrencyBalance.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetCurrencyBalanceErrorCallback)(CCGetCurrencyBalanceRequest *request, CCGetCurrencyBalanceError *error);
	
/*!
 A block describing an error callback from setCurrencyBalance.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCSetCurrencyBalanceErrorCallback)(CCSetCurrencyBalanceRequest *request, CCSetCurrencyBalanceError *error);
	
/*!
 A block describing an error callback from convertCurrency.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCConvertCurrencyErrorCallback)(CCConvertCurrencyRequest *request, CCConvertCurrencyError *error);
	
/*!
 A block describing an error callback from addCurrencyBalance.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCAddCurrencyBalanceErrorCallback)(CCAddCurrencyBalanceRequest *request, CCAddCurrencyBalanceError *error);
	
/*!
 A block describing an error callback from removeCurrencyBalance.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCRemoveCurrencyBalanceErrorCallback)(CCRemoveCurrencyBalanceRequest *request, CCRemoveCurrencyBalanceError *error);
	
/*!
 A block describing an error callback from getInventory.
 
 @param error The error that ocurred.
 */
typedef void (^CCGetInventoryErrorCallback)(CCGetInventoryError *error);
	
/*!
 A block describing an error callback from getInventoryForKeys.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetInventoryForKeysErrorCallback)(CCGetInventoryForKeysRequest *request, CCGetInventoryForKeysError *error);
	
/*!
 A block describing an error callback from getInventoryForItemIds.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetInventoryForItemIdsErrorCallback)(CCGetInventoryForItemIdsRequest *request, CCGetInventoryForItemIdsError *error);
	
/*!
 A block describing an error callback from addInventoryItem.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCAddInventoryItemErrorCallback)(CCAddInventoryItemRequest *request, CCAddInventoryItemError *error);
	
/*!
 A block describing an error callback from updateInventoryItem.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCUpdateInventoryItemErrorCallback)(CCUpdateInventoryItemRequest *request, CCUpdateInventoryItemError *error);
	
/*!
 A block describing an error callback from removeInventoryItem.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCRemoveInventoryItemErrorCallback)(CCRemoveInventoryItemRequest *request, CCRemoveInventoryItemError *error);
	
/*!
 A block describing an error callback from makeVirtualPurchase.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCMakeVirtualPurchaseErrorCallback)(CCMakeVirtualPurchaseRequest *request, CCMakeVirtualPurchaseError *error);
	
/*!
 A block describing an error callback from redeemAmazonIap.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCRedeemAmazonIapErrorCallback)(CCRedeemAmazonIapRequest *request, CCRedeemAmazonIapError *error);
	
/*!
 A block describing an error callback from redeemAppleIap.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCRedeemAppleIapErrorCallback)(CCRedeemAppleIapRequest *request, CCRedeemAppleIapError *error);
	
/*!
 A block describing an error callback from redeemGoogleIap.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCRedeemGoogleIapErrorCallback)(CCRedeemGoogleIapRequest *request, CCRedeemGoogleIapError *error);
	
/*!
 A block describing an error callback from getConversionDefinitions.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetConversionDefinitionsErrorCallback)(CCGetConversionDefinitionsRequest *request, CCGetConversionDefinitionsError *error);
	
/*!
 A block describing an error callback from getCurrencyDefinitions.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetCurrencyDefinitionsErrorCallback)(CCGetCurrencyDefinitionsRequest *request, CCGetCurrencyDefinitionsError *error);
	
/*!
 A block describing an error callback from getInventoryDefinitions.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetInventoryDefinitionsErrorCallback)(CCGetInventoryDefinitionsRequest *request, CCGetInventoryDefinitionsError *error);
	
/*!
 A block describing an error callback from getMetadataDefinitions.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetMetadataDefinitionsErrorCallback)(CCGetMetadataDefinitionsRequest *request, CCGetMetadataDefinitionsError *error);
	
/*!
 A block describing an error callback from getRealMoneyPurchaseDefinitions.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetRealMoneyPurchaseDefinitionsErrorCallback)(CCGetRealMoneyPurchaseDefinitionsRequest *request, CCGetRealMoneyPurchaseDefinitionsError *error);
	
/*!
 A block describing an error callback from getVirtualPurchaseDefinitions.
 
 @param request The original request.
 @param error The error that ocurred.
 */
typedef void (^CCGetVirtualPurchaseDefinitionsErrorCallback)(CCGetVirtualPurchaseDefinitionsRequest *request, CCGetVirtualPurchaseDefinitionsError *error);

/*!
 The ChillConnect Economy Management module. Provides the means to retrieve and
 modify player currencies and inventory.

 This is thread-safe.
 */
@interface CCEconomy : NSObject {
	SCLogging *_logging;
	SCTaskScheduler *_taskScheduler;
	SCServerRequestSystem *_serverRequestSystem;
	SCDataStore *_dataStore;
}

/*!
 A convenience factory method for creating new instances of CCEconomy
 with the given logger, task scheduler and server request system.
 
 @param logging Provides basic logging functionality.
 @param taskScheduler The system which allows scheduling of tasks on different 
        threads.
 @param serverRequestSystem The system which processes all server requests.
 @param dataStore A persistent data store used for DataStore property types.

 @return The new instance.
 */
+ (instancetype)economyWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore;

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

 @return The initialised CCEconomy.
 */
- (instancetype)initWithLogging:(SCLogging *)logging taskScheduler:(SCTaskScheduler *)taskScheduler serverRequestSystem:(SCServerRequestSystem *)serverRequestSystem dataStore:(SCDataStore *)dataStore NS_DESIGNATED_INITIALIZER;

/*!
 Returns a list of currency balances for the currently logged in player.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getCurrencyBalanceWithDesc:(CCGetCurrencyBalanceRequestDesc *)desc successCallback:(CCGetCurrencyBalanceResponseCallback)successCallback errorCallback:(CCGetCurrencyBalanceErrorCallback)errorCallback;		

/*!
 Sets the balance of a specified Currency for the currently logged in player.
 Direct access to this method from the SDKs is disabled by default and must be
 enabled from the ChilliConnect dashboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)setCurrencyBalanceWithDesc:(CCSetCurrencyBalanceRequestDesc *)desc successCallback:(CCSetCurrencyBalanceResponseCallback)successCallback errorCallback:(CCSetCurrencyBalanceErrorCallback)errorCallback;		

/*!
 Convert a currency using a defined currency conversion rule.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)convertCurrencyWithDesc:(CCConvertCurrencyRequestDesc *)desc successCallback:(CCConvertCurrencyResponseCallback)successCallback errorCallback:(CCConvertCurrencyErrorCallback)errorCallback;		

/*!
 Add currency for the currently logged in player. Direct access to this method
 from the SDKs is disabled by default and must be enabled from the ChilliConnect
 dashboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)addCurrencyBalanceWithDesc:(CCAddCurrencyBalanceRequestDesc *)desc successCallback:(CCAddCurrencyBalanceResponseCallback)successCallback errorCallback:(CCAddCurrencyBalanceErrorCallback)errorCallback;		

/*!
 Remove currency for the currently logged in player. Direct access to this method
 from the SDKs is disabled by default and must be enabled from the ChilliConnect
 dashboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)removeCurrencyBalanceWithDesc:(CCRemoveCurrencyBalanceRequestDesc *)desc successCallback:(CCRemoveCurrencyBalanceResponseCallback)successCallback errorCallback:(CCRemoveCurrencyBalanceErrorCallback)errorCallback;		

/*!
 Get the inventory of the currently logged in player.
 
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getInventoryWithSuccessCallback:(CCGetInventoryResponseCallback)successCallback errorCallback:(CCGetInventoryErrorCallback)errorCallback;

/*!
 Get the inventory of the currently logged in player for a given set of keys.
 
 @param keys Return only items with these Keys from the player's inventory.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getInventoryForKeysWithKeys:(NSArray *)keys successCallback:(CCGetInventoryForKeysResponseCallback)successCallback errorCallback:(CCGetInventoryForKeysErrorCallback)errorCallback;

/*!
 Get the inventory of the currently logged in player for a given set of Item IDs.
 
 @param itemIds Return only these items witihin the player's inventory.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getInventoryForItemIdsWithItemIds:(NSArray *)itemIds successCallback:(CCGetInventoryForItemIdsResponseCallback)successCallback errorCallback:(CCGetInventoryForItemIdsErrorCallback)errorCallback;

/*!
 Add an item to a player's inventory. Direct access to this method from the SDKs
 is disabled by default and must be enabled from the ChilliConnect dashboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)addInventoryItemWithDesc:(CCAddInventoryItemRequestDesc *)desc successCallback:(CCAddInventoryItemResponseCallback)successCallback errorCallback:(CCAddInventoryItemErrorCallback)errorCallback;		

/*!
 Update the instance data of an item in the currently logged in player's
 inventory. Direct access to this method from the SDKs is disabled by default and
 must be enabled from the ChilliConnect dashboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)updateInventoryItemWithDesc:(CCUpdateInventoryItemRequestDesc *)desc successCallback:(CCUpdateInventoryItemResponseCallback)successCallback errorCallback:(CCUpdateInventoryItemErrorCallback)errorCallback;		

/*!
 Remove an item from the currently logged in player's inventory. Direct access to
 this method from the SDKs is disabled by default and must be enabled from the
 ChilliConnect dashboard.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)removeInventoryItemWithDesc:(CCRemoveInventoryItemRequestDesc *)desc successCallback:(CCRemoveInventoryItemResponseCallback)successCallback errorCallback:(CCRemoveInventoryItemErrorCallback)errorCallback;		

/*!
 Perform a purchase defined by a Virtual Purchase item.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)makeVirtualPurchaseWithDesc:(CCMakeVirtualPurchaseRequestDesc *)desc successCallback:(CCMakeVirtualPurchaseResponseCallback)successCallback errorCallback:(CCMakeVirtualPurchaseErrorCallback)errorCallback;		

/*!
 Validate a Receipt from a successful purchase on an Amazon device and apply the
 rewards to the players account.
 
 @param key The key of the real money purchase that defines the rewards to be applied to the
        players account on successful verification. The real money purchase should	
        specify an amazon product id that matches the product id of the submitted	
        Receipt.	
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
- (void)redeemAmazonIapWithKey:(NSString *)key receiptId:(NSString *)receiptId userId:(NSString *)userId successCallback:(CCRedeemAmazonIapResponseCallback)successCallback errorCallback:(CCRedeemAmazonIapErrorCallback)errorCallback;

/*!
 Validate a Receipt from a successful purchase on an Apple device and apply the
 rewards to the players account.
 
 @param key The key of the real money purchase that defines the rewards to be applied to the
        players account on successful verification. The real money purchase should	
        specify an apple productId that matches the productId of the submitted Receipt.	
 @param receipt Receipt data returned from the App Store as a result of a successful purchase.
        This should be base64 encoded.	
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)redeemAppleIapWithKey:(NSString *)key receipt:(NSString *)receipt successCallback:(CCRedeemAppleIapResponseCallback)successCallback errorCallback:(CCRedeemAppleIapErrorCallback)errorCallback;

/*!
 Validate a Receipt from a successful purchase on a Google device and apply the
 rewards to the players account.
 
 @param key The key of the real money purchase that defines the rewards to be applied to the
        players account on successful verification. The real money purchase should	
        specify a Google productId that matches the productId of the submitted	
        PurchaseData.	
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
- (void)redeemGoogleIapWithKey:(NSString *)key purchaseData:(NSString *)purchaseData purchaseDataSignature:(NSString *)purchaseDataSignature successCallback:(CCRedeemGoogleIapResponseCallback)successCallback errorCallback:(CCRedeemGoogleIapErrorCallback)errorCallback;

/*!
 Get the Economy definitions for any Currency Conversion items.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getConversionDefinitionsWithDesc:(CCGetConversionDefinitionsRequestDesc *)desc successCallback:(CCGetConversionDefinitionsResponseCallback)successCallback errorCallback:(CCGetConversionDefinitionsErrorCallback)errorCallback;		

/*!
 Get the Economy definitions for any Currency items.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getCurrencyDefinitionsWithDesc:(CCGetCurrencyDefinitionsRequestDesc *)desc successCallback:(CCGetCurrencyDefinitionsResponseCallback)successCallback errorCallback:(CCGetCurrencyDefinitionsErrorCallback)errorCallback;		

/*!
 Get the Economy definitions for any Inventory items.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getInventoryDefinitionsWithDesc:(CCGetInventoryDefinitionsRequestDesc *)desc successCallback:(CCGetInventoryDefinitionsResponseCallback)successCallback errorCallback:(CCGetInventoryDefinitionsErrorCallback)errorCallback;		

/*!
 Get the Economy definitions for any Metadata items.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getMetadataDefinitionsWithDesc:(CCGetMetadataDefinitionsRequestDesc *)desc successCallback:(CCGetMetadataDefinitionsResponseCallback)successCallback errorCallback:(CCGetMetadataDefinitionsErrorCallback)errorCallback;		

/*!
 Get the Economy definitions for any Real Money Purchase items.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getRealMoneyPurchaseDefinitionsWithDesc:(CCGetRealMoneyPurchaseDefinitionsRequestDesc *)desc successCallback:(CCGetRealMoneyPurchaseDefinitionsResponseCallback)successCallback errorCallback:(CCGetRealMoneyPurchaseDefinitionsErrorCallback)errorCallback;		

/*!
 Get the Economy definitions for any Virtual Purchase items.
 
 @param desc The request description.
 @param successCallback The block which is called if the request is successful. 
        Provides a container with the response from the server.
 @param errorCallback The block which is called if the request is unsuccessful.
        Provides a container with information on what went wrong.
 */
- (void)getVirtualPurchaseDefinitionsWithDesc:(CCGetVirtualPurchaseDefinitionsRequestDesc *)desc successCallback:(CCGetVirtualPurchaseDefinitionsResponseCallback)successCallback errorCallback:(CCGetVirtualPurchaseDefinitionsErrorCallback)errorCallback;		

@end

NS_ASSUME_NONNULL_END
