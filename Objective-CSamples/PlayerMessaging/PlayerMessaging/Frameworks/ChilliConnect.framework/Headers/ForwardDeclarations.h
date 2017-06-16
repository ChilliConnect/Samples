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

NS_ASSUME_NONNULL_BEGIN

@class CCChilliConnectSdk;

@class CCFacebookPlayer;
@class CCFacebookPlayerDesc;
@class CCPlayer;
@class CCPlayerDesc;
@class CCFacebookPlayerWithProfileImage;
@class CCFacebookPlayerWithProfileImageDesc;
@class CCGlobalScore;
@class CCGlobalScoreDesc;
@class CCLocalScore;
@class CCLocalScoreDesc;
@class CCFacebookScore;
@class CCFacebookScoreDesc;
@class CCMetricsEvent;
@class CCMetricsEventDesc;
@class CCPlayerData;
@class CCPlayerDataDesc;
@class CCChilliConnectPlayerData;
@class CCChilliConnectPlayerDataDesc;
@class CCFacebookPlayerData;
@class CCFacebookPlayerDataDesc;
@class CCReceiptVerificationService;
@class CCDlcPackage;
@class CCAppStore;
@class CCDlcPackageFile;
@class CCCurrencyBalance;
@class CCCurrencyBalanceDesc;
@class CCPlayerInventoryItem;
@class CCPlayerInventoryItemDesc;
@class CCPurchaseExchange;
@class CCPurchaseCurrencyExchange;
@class CCPurchaseInventoryExchange;
@class CCConversionDefinition;
@class CCConversionDefinitionDesc;
@class CCConversionRuleDefinition;
@class CCCurrencyDefinition;
@class CCCurrencyDefinitionDesc;
@class CCInventoryDefinition;
@class CCInventoryDefinitionDesc;
@class CCRealMoneyPurchaseDefinition;
@class CCRealMoneyPurchaseDefinitionDesc;
@class CCVirtualPurchaseDefinition;
@class CCVirtualPurchaseDefinitionDesc;
@class CCPurchaseExchangeDefinition;
@class CCPurchaseCurrencyExchangeDefinition;
@class CCPurchaseInventoryExchangeDefinition;
@class CCMetadataDefinition;
@class CCMetadataDefinitionDesc;
@class CCMessageTransfer;
@class CCMessageSendCurrency;
@class CCMessageGifts;
@class CCMessageSendInventory;
@class CCMessage;
@class CCMessageDesc;
@class CCMessageSender;
@class CCMessageSenderDesc;
@class CCMessageReward;
@class CCMessageRewardCurrency;
@class CCMessageRewardInventory;
@class CCMessageRewardInventoryDesc;
@class CCMessageRewardRedeemed;
@class CCMessageRewardRedeemedCurrency;
@class CCMessageRewardRedeemedInventory;
@class CCMessageRewardRedeemedInventoryDesc;
@class CCCollectionDataObject;
@class CCCollectionDataObjectDesc;
@class CCSortField;

@class CCPlayerAccounts;
@class CCCreatePlayerError;
@class CCCreatePlayerRequest;
@class CCCreatePlayerRequestDesc;
@class CCCreatePlayerResponse;
@class CCLogInUsingChilliConnectError;
@class CCLogInUsingChilliConnectRequest;
@class CCLogInUsingEmailError;
@class CCLogInUsingEmailRequest;
@class CCLogInUsingEmailResponse;
@class CCLogInUsingFacebookError;
@class CCLogInUsingFacebookRequest;
@class CCLogInUsingFacebookResponse;
@class CCLogInUsingUserNameError;
@class CCLogInUsingUserNameRequest;
@class CCLogInUsingUserNameResponse;
@class CCSetPlayerDetailsError;
@class CCSetPlayerDetailsRequest;
@class CCSetPlayerDetailsRequestDesc;
@class CCSetPlayerDetailsResponse;
@class CCGetPlayerDetailsError;
@class CCGetPlayerDetailsResponse;
@class CCLinkFacebookAccountError;
@class CCLinkFacebookAccountRequest;
@class CCLinkFacebookAccountRequestDesc;
@class CCLinkFacebookAccountResponse;
@class CCVerifyFacebookTokenError;
@class CCVerifyFacebookTokenResponse;
@class CCLookupFacebookPlayersError;
@class CCLookupFacebookPlayersRequest;
@class CCLookupFacebookPlayersResponse;
@class CCLookupUserNamesError;
@class CCLookupUserNamesRequest;
@class CCLookupUserNamesResponse;
@class CCGetFacebookFriendsError;
@class CCGetFacebookFriendsResponse;
@class CCUnlinkFacebookAccountError;
@class CCUnlinkFacebookAccountResponse;
@class CCCloudData;
@class CCSetPlayerDataError;
@class CCSetPlayerDataRequest;
@class CCSetPlayerDataRequestDesc;
@class CCSetPlayerDataResponse;
@class CCGetPlayerDataError;
@class CCGetPlayerDataRequest;
@class CCGetPlayerDataResponse;
@class CCGetPlayerDataForChilliConnectIdsError;
@class CCGetPlayerDataForChilliConnectIdsRequest;
@class CCGetPlayerDataForChilliConnectIdsRequestDesc;
@class CCGetPlayerDataForChilliConnectIdsResponse;
@class CCGetPlayerDataForFacebookFriendsError;
@class CCGetPlayerDataForFacebookFriendsRequest;
@class CCGetPlayerDataForFacebookFriendsRequestDesc;
@class CCGetPlayerDataForFacebookFriendsResponse;
@class CCDeletePlayerDataError;
@class CCDeletePlayerDataRequest;
@class CCDeletePlayerDataRequestDesc;
@class CCAddCollectionObjectError;
@class CCAddCollectionObjectRequest;
@class CCAddCollectionObjectResponse;
@class CCUpdateCollectionObjectError;
@class CCUpdateCollectionObjectRequest;
@class CCUpdateCollectionObjectResponse;
@class CCDeleteCollectionObjectError;
@class CCDeleteCollectionObjectRequest;
@class CCQueryCollectionError;
@class CCQueryCollectionRequest;
@class CCQueryCollectionRequestDesc;
@class CCQueryCollectionResponse;
@class CCLeaderboards;
@class CCAddScoreError;
@class CCAddScoreRequest;
@class CCAddScoreRequestDesc;
@class CCAddScoreResponse;
@class CCGetPlayerScoreError;
@class CCGetPlayerScoreRequest;
@class CCGetPlayerScoreResponse;
@class CCGetScoresAroundPlayerError;
@class CCGetScoresAroundPlayerRequest;
@class CCGetScoresAroundPlayerResponse;
@class CCGetScoresForChilliConnectIdsError;
@class CCGetScoresForChilliConnectIdsRequest;
@class CCGetScoresForChilliConnectIdsRequestDesc;
@class CCGetScoresForChilliConnectIdsResponse;
@class CCGetScoresForFacebookFriendsError;
@class CCGetScoresForFacebookFriendsRequest;
@class CCGetScoresForFacebookFriendsRequestDesc;
@class CCGetScoresForFacebookFriendsResponse;
@class CCGetScoresError;
@class CCGetScoresRequest;
@class CCGetScoresRequestDesc;
@class CCGetScoresResponse;
@class CCCloudCode;
@class CCRunScriptError;
@class CCRunScriptRequest;
@class CCRunScriptRequestDesc;
@class CCRunScriptResponse;
@class CCPushNotifications;
@class CCRegisterTokenError;
@class CCRegisterTokenRequest;
@class CCRegisterTokenRequestDesc;
@class CCUnregisterTokenError;
@class CCUnregisterTokenRequest;
@class CCSetPushGroupsError;
@class CCSetPushGroupsRequest;
@class CCInAppPurchase;
@class CCValidateAmazonIapError;
@class CCValidateAmazonIapRequest;
@class CCValidateAmazonIapResponse;
@class CCValidateAppleIapError;
@class CCValidateAppleIapRequest;
@class CCValidateAppleIapResponse;
@class CCValidateGoogleIapError;
@class CCValidateGoogleIapRequest;
@class CCValidateGoogleIapResponse;
@class CCDlc;
@class CCGetDlcUsingTagError;
@class CCGetDlcUsingTagRequest;
@class CCGetDlcUsingTagResponse;
@class CCMetrics;
@class CCGenerateUuidError;
@class CCGenerateUuidResponse;
@class CCStartSessionError;
@class CCStartSessionRequest;
@class CCAddEventError;
@class CCAddEventRequest;
@class CCAddEventRequestDesc;
@class CCAddEventsError;
@class CCAddEventsRequest;
@class CCAddIapEventError;
@class CCAddIapEventRequest;
@class CCAddIapEventRequestDesc;
@class CCEndSessionError;
@class CCEconomy;
@class CCGetCurrencyBalanceError;
@class CCGetCurrencyBalanceRequest;
@class CCGetCurrencyBalanceRequestDesc;
@class CCGetCurrencyBalanceResponse;
@class CCSetCurrencyBalanceError;
@class CCSetCurrencyBalanceRequest;
@class CCSetCurrencyBalanceRequestDesc;
@class CCSetCurrencyBalanceResponse;
@class CCConvertCurrencyError;
@class CCConvertCurrencyRequest;
@class CCConvertCurrencyRequestDesc;
@class CCConvertCurrencyResponse;
@class CCAddCurrencyBalanceError;
@class CCAddCurrencyBalanceRequest;
@class CCAddCurrencyBalanceRequestDesc;
@class CCAddCurrencyBalanceResponse;
@class CCRemoveCurrencyBalanceError;
@class CCRemoveCurrencyBalanceRequest;
@class CCRemoveCurrencyBalanceRequestDesc;
@class CCRemoveCurrencyBalanceResponse;
@class CCGetInventoryError;
@class CCGetInventoryResponse;
@class CCGetInventoryForKeysError;
@class CCGetInventoryForKeysRequest;
@class CCGetInventoryForKeysResponse;
@class CCGetInventoryForItemIdsError;
@class CCGetInventoryForItemIdsRequest;
@class CCGetInventoryForItemIdsResponse;
@class CCAddInventoryItemError;
@class CCAddInventoryItemRequest;
@class CCAddInventoryItemRequestDesc;
@class CCAddInventoryItemResponse;
@class CCUpdateInventoryItemError;
@class CCUpdateInventoryItemRequest;
@class CCUpdateInventoryItemRequestDesc;
@class CCUpdateInventoryItemResponse;
@class CCRemoveInventoryItemError;
@class CCRemoveInventoryItemRequest;
@class CCRemoveInventoryItemRequestDesc;
@class CCMakeVirtualPurchaseError;
@class CCMakeVirtualPurchaseRequest;
@class CCMakeVirtualPurchaseRequestDesc;
@class CCMakeVirtualPurchaseResponse;
@class CCRedeemAmazonIapError;
@class CCRedeemAmazonIapRequest;
@class CCRedeemAmazonIapResponse;
@class CCRedeemAppleIapError;
@class CCRedeemAppleIapRequest;
@class CCRedeemAppleIapResponse;
@class CCRedeemGoogleIapError;
@class CCRedeemGoogleIapRequest;
@class CCRedeemGoogleIapResponse;
@class CCGetConversionDefinitionsError;
@class CCGetConversionDefinitionsRequest;
@class CCGetConversionDefinitionsRequestDesc;
@class CCGetConversionDefinitionsResponse;
@class CCGetCurrencyDefinitionsError;
@class CCGetCurrencyDefinitionsRequest;
@class CCGetCurrencyDefinitionsRequestDesc;
@class CCGetCurrencyDefinitionsResponse;
@class CCGetInventoryDefinitionsError;
@class CCGetInventoryDefinitionsRequest;
@class CCGetInventoryDefinitionsRequestDesc;
@class CCGetInventoryDefinitionsResponse;
@class CCGetMetadataDefinitionsError;
@class CCGetMetadataDefinitionsRequest;
@class CCGetMetadataDefinitionsRequestDesc;
@class CCGetMetadataDefinitionsResponse;
@class CCGetRealMoneyPurchaseDefinitionsError;
@class CCGetRealMoneyPurchaseDefinitionsRequest;
@class CCGetRealMoneyPurchaseDefinitionsRequestDesc;
@class CCGetRealMoneyPurchaseDefinitionsResponse;
@class CCGetVirtualPurchaseDefinitionsError;
@class CCGetVirtualPurchaseDefinitionsRequest;
@class CCGetVirtualPurchaseDefinitionsRequestDesc;
@class CCGetVirtualPurchaseDefinitionsResponse;
@class CCMessaging;
@class CCSendMessageError;
@class CCSendMessageRequest;
@class CCSendMessageRequestDesc;
@class CCSendMessageResponse;
@class CCSendMessageFromPlayerError;
@class CCSendMessageFromPlayerRequest;
@class CCSendMessageFromPlayerRequestDesc;
@class CCSendMessageFromPlayerResponse;
@class CCGetMessageError;
@class CCGetMessageRequest;
@class CCGetMessageRequestDesc;
@class CCGetMessageResponse;
@class CCGetMessagesError;
@class CCGetMessagesRequest;
@class CCGetMessagesRequestDesc;
@class CCGetMessagesResponse;
@class CCRedeemMessageRewardsError;
@class CCRedeemMessageRewardsRequest;
@class CCRedeemMessageRewardsRequestDesc;
@class CCRedeemMessageRewardsResponse;
@class CCMarkMessageAsReadError;
@class CCMarkMessageAsReadRequest;
@class CCDeleteMessageError;
@class CCDeleteMessageRequest;

@class SCDataStore;

@class SCHttpGetRequest;
@class SCHttpGetRequestDesc;
@class SCHttpPostRequest;
@class SCHttpPostRequestDesc;
@class SCHttpResponse;
@class SCHttpResponseDesc;
@class SCHttpSystem;

@class SCLogging;

@class SCMultiTypeArray;
@class SCMultiTypeArrayBuilder;
@class SCMultiTypeDictionary;
@class SCMultiTypeDictionaryBuilder;
@class SCMultiTypeValue;

@class SCBasicServerRequest;
@protocol SCImmediateServerRequest;
@class SCServerRequestSystem;
@class SCServerResponse;

@class SCTaskScheduler;

NS_ASSUME_NONNULL_END

