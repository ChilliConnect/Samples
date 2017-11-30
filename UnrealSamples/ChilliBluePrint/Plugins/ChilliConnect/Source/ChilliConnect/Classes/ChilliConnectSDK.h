#pragma once

#include "ChilliConnectPrivatePCH.h"
#include "OnlineBlueprintCallProxyBase.h"
#include "ChilliConnectObjects.h"
#include "ChilliConnectJson.h"
#include "ChilliConnectSDK.generated.h"

UCLASS(Blueprintable, BlueprintType)
class UChilliConnectSDK : public UOnlineBlueprintCallProxyBase
{
	GENERATED_UCLASS_BODY()

	virtual void Activate() override;

	/** Generic Error Delegate */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnError, FChilliConnectErrorResponse, onError);

	/** Create Player */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnCreatePlayerSuccess, FCreatePlayerResponse, onSuccess);

	/** Login Player With ChilliConnectID and Secret */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnLoginUsingChilliConnectSuccess, FLoginUsingChilliConnectResponse, onSuccess);

	/** Set Player Data*/
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnSetPlayerDataSuccess, FSetPlayerDataResponse, onSuccess);

	/** Get Player Data*/
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnGetPlayerDataSuccess, FGetPlayerDataResponse, onSuccess);

	/** Get Currency Balance*/
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnGetCurrencyBalanceSuccess, FGetCurrencyBalanceResponse, onSuccess);

	/** Set Currency Balance*/
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnSetCurrencyBalanceSuccess, FSetCurrencyBalanceResponse, onSuccess);
	
	/** Get Inventory */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnGetInventorySuccess, FGetInventoryResponse, onSuccess);

	/** Add Inventory Item */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnAddInventoryItemSuccess, FAddInventoryItemResponse, onSuccess);

	/** Remove Inventory Item */
	DECLARE_DYNAMIC_DELEGATE(FDelegateOnRemoveInventoryItemSuccess);

	/** Redeem Google IAP */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnRedeemGoogleIapSuccess, FRedeemIapResponse, onSuccess);

	/** Redeem Apple IAP */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnRedeemAppleIapSuccess, FRedeemIapResponse, onSuccess);

	/** Get Metadata Definitions */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnGetMetadataDefinitionsSuccess, FGetMetadataDefinitionsResponse, onSuccess);

	/** Register Push Token */
	DECLARE_DYNAMIC_DELEGATE(FDelegateOnRegisterPushTokenSuccess);

	/** Get Dlc */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnGetDlcUsingTagsSuccess, FGetDlcUsingTagsResponse, onSuccess);

	/** Get Virtual Purchase Definitions */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnGetVirtualPurchaseDefinitionsSuccess, FGetVirtualPurchaseDefinitionsResponse, onSuccess);

	/** Make Virtual Purchase */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnMakeVirtualPurchaseSuccess, FMakeVirtualPurchaseResponse, onSuccess);

	/** Add Collection Object */
	DECLARE_DYNAMIC_DELEGATE_OneParam(FDelegateOnAddCollectionObjectSuccess, FAddCollectionObjectResponse, onSuccess);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* CreatePlayer(FCreatePlayerRequest request, FDelegateOnCreatePlayerSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* LoginUsingChilliConnect(FLoginUsingChilliConnectRequest request, FDelegateOnLoginUsingChilliConnectSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* SetPlayerData(FSetPlayerDataRequest request, FDelegateOnSetPlayerDataSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* GetPlayerData(FGetPlayerDataRequest request, FDelegateOnGetPlayerDataSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* GetCurrencyBalance(FGetCurrencyBalanceRequest request, FDelegateOnGetCurrencyBalanceSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* SetCurrencyBalance(FSetCurrencyBalanceRequest request, FDelegateOnSetCurrencyBalanceSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* GetInventory(FDelegateOnGetInventorySuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* AddInventoryItem(FAddInventoryItemRequest Request, FDelegateOnAddInventoryItemSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* RemoveInventoryItem(FRemoveInventoryItemRequest Request, FDelegateOnRemoveInventoryItemSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* RedeemGoogleIap(FRedeemGoogleIapRequest Request, FDelegateOnRedeemGoogleIapSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* RedeemAppleIap(FRedeemAppleIapRequest Request, FDelegateOnRedeemAppleIapSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* GetMetadataDefinitions(FGetMetadataDefinitionsRequest Request, FDelegateOnGetMetadataDefinitionsSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* RegisterPushToken(FRegisterPushTokenRequest Request, FDelegateOnRegisterPushTokenSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* GetDlcUsingTags(FGetDlcUsingTagsRequest Request, FDelegateOnGetDlcUsingTagsSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* GetVirtualPurchaseDefinitions(FGetVirtualPurchaseDefinitionsRequest Request, FDelegateOnGetVirtualPurchaseDefinitionsSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* MakeVirtualPurchase(FMakeVirtualPurchaseRequest Request, FDelegateOnMakeVirtualPurchaseSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* AddCollectionObject(FAddCollectionObjectRequest Request, FDelegateOnAddCollectionObjectSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static void SetGameToken(FString GameToken);

	private:

	static FString GetJsonRequestBody(TSharedPtr<FJsonObject> Json);

	DECLARE_DELEGATE_OneParam(FDelegateOnHttpRequestProcessed, UChilliConnectJson*);

	/** Delegate references */
	FDelegateOnCreatePlayerSuccess OnCreatePlayerSuccess;
	FDelegateOnLoginUsingChilliConnectSuccess OnLoginUsingChilliConnectSuccess;
	FDelegateOnSetPlayerDataSuccess OnSetPlayerDataSuccess;
	FDelegateOnGetPlayerDataSuccess OnGetPlayerDataSuccess;
	FDelegateOnGetCurrencyBalanceSuccess OnGetCurrencyBalanceSuccess;
	FDelegateOnSetCurrencyBalanceSuccess OnSetCurrencyBalanceSuccess;
	FDelegateOnGetInventorySuccess OnGetInventorySuccess;
	FDelegateOnAddInventoryItemSuccess OnAddInventoryItemSuccess;
	FDelegateOnRemoveInventoryItemSuccess OnRemoveInventoryItemSuccess;
	FDelegateOnRedeemGoogleIapSuccess OnRedeemGoogleIapSuccess;
	FDelegateOnRedeemAppleIapSuccess OnRedeemAppleIapSuccess;
	FDelegateOnGetMetadataDefinitionsSuccess OnGetMetadataDefinitionsSuccess;
	FDelegateOnRegisterPushTokenSuccess OnRegisterPushTokenSuccess;
	FDelegateOnGetDlcUsingTagsSuccess OnGetDlcUsingTagsSuccess;
	FDelegateOnGetVirtualPurchaseDefinitionsSuccess OnGetVirtualPurchaseDefinitionsSuccess;
	FDelegateOnMakeVirtualPurchaseSuccess OnMakeVirtualPurchaseSuccess;
	FDelegateOnAddCollectionObjectSuccess OnAddCollectionObjectSuccess;

	FDelegateOnError OnError;
	
	FDelegateOnHttpRequestProcessed OnHttpRequestProcessed;

	FString RequestBody;
	FString RequestUrl;
	bool bIsAuthenticated;

	TSharedPtr<FJsonObject> DeserialiseHttpResponse(FHttpResponsePtr HttpResponse);

	void OnHttpRequestComplete(FHttpRequestPtr Request, FHttpResponsePtr Response, bool bWasSuccessful);

	FRedeemIapResponse GetIapResponse(TSharedPtr<FJsonObject> Json);

	/** Specific request response handlers, implement FDelegateOnHttpRequestProcessed */
	void OnCreatePlayerComplete(UChilliConnectJson* response);
	void OnSetPlayerDataComplete(UChilliConnectJson* response);
	void OnLoginUsingChilliConnectComplete(UChilliConnectJson* response);
	void OnGetPlayerDataComplete(UChilliConnectJson* response);
	void OnGetCurrencyBalanceComplete(UChilliConnectJson* response);
	void OnSetCurrencyBalanceComplete(UChilliConnectJson* response);
	void OnGetInventoryComplete(UChilliConnectJson* response);
	void OnAddInventoryItemComplete(UChilliConnectJson* response);
	void OnRemoveInventoryItemComplete(UChilliConnectJson* response);
	void OnRedeemGoogleIapComplete(UChilliConnectJson* response);
	void OnRedeemAppleIapComplete(UChilliConnectJson* response);
	void OnGetMetadataDefinitionsComplete(UChilliConnectJson* response);
	void OnRegisterPushTokenComplete(UChilliConnectJson* response);
	void OnGetDlcUsingTagsComplete(UChilliConnectJson* response);
	void OnGetVirtualPurchaseDefinitionsComplete(UChilliConnectJson* response);
	void OnMakeVirtualPurchaseComplete(UChilliConnectJson* response);
	void OnAddCollectionObjectComplete(UChilliConnectJson* response);

};