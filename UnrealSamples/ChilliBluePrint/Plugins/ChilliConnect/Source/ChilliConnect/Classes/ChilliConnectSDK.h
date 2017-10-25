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

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* CreatePlayer(FCreatePlayerRequest request, FDelegateOnCreatePlayerSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* LoginUsingChilliConnect(FLoginUsingChilliConnectRequest request, FDelegateOnLoginUsingChilliConnectSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* SetPlayerData(FSetPlayerDataRequest request, FDelegateOnSetPlayerDataSuccess onSuccess, FDelegateOnError onError);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		static UChilliConnectSDK* GetPlayerData(FGetPlayerDataRequest request, FDelegateOnGetPlayerDataSuccess onSuccess, FDelegateOnError onError);

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
	FDelegateOnError OnError;
	
	FDelegateOnHttpRequestProcessed OnHttpRequestProcessed;

	FString RequestBody;
	FString RequestUrl;
	bool bIsAuthenticated;

	TSharedPtr<FJsonObject> DeserialiseHttpResponse(FHttpResponsePtr HttpResponse);

	void OnHttpRequestComplete(FHttpRequestPtr Request, FHttpResponsePtr Response, bool bWasSuccessful);

	/** Specific request response handlers, implement FDelegateOnHttpRequestProcessed */
	void OnCreatePlayerComplete(UChilliConnectJson* response);
	void OnSetPlayerDataComplete(UChilliConnectJson* response);
	void OnLoginUsingChilliConnectComplete(UChilliConnectJson* response);
	void OnGetPlayerDataComplete(UChilliConnectJson* response);

};
