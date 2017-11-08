#include "ChilliConnectPrivatePCH.h"
#include "ChilliConnectSDK.h"

UChilliConnectSDK::UChilliConnectSDK(const FObjectInitializer& ObjectInitializer)
	: Super(ObjectInitializer)
{
}

void 
UChilliConnectSDK::SetGameToken(FString GameToken)
{
	IChilliConnect* Config = &(IChilliConnect::Get());
	Config->SetGameToken(GameToken);
}

UChilliConnectSDK* 
UChilliConnectSDK::CreatePlayer(FCreatePlayerRequest request, FDelegateOnCreatePlayerSuccess onSuccess, FDelegateOnError onError) 
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	RequestInstance->OnCreatePlayerSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnCreatePlayerComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/player/create");
	
	return RequestInstance;
}


UChilliConnectSDK* 
UChilliConnectSDK::LoginUsingChilliConnect(FLoginUsingChilliConnectRequest Request, FDelegateOnLoginUsingChilliConnectSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("ChilliConnectID", *Request.ChilliConnectID);
	Json->SetStringField("ChilliConnectSecret", *Request.ChilliConnectSecret);

	RequestInstance->RequestBody = GetJsonRequestBody(Json);

	RequestInstance->OnLoginUsingChilliConnectSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnLoginUsingChilliConnectComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/player/login/chilli");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::SetPlayerData(FSetPlayerDataRequest Request, FDelegateOnSetPlayerDataSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("Key", *Request.Key);
	Json->SetObjectField("Value", Request.Value->GetJsonObject());
	if (Request.WriteLock.Len() > 0) {
		Json->SetStringField("WriteLock", *Request.WriteLock);
	}
	if (Request.Attachment.Len() > 0) {
		Json->SetStringField("Attachement", *Request.Attachment);
	}

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnSetPlayerDataSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnSetPlayerDataComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/data/player/set");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::GetPlayerData(FGetPlayerDataRequest Request, FDelegateOnGetPlayerDataSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);

	TArray <TSharedPtr<FJsonValue>> JsonKeys;
	for (auto& Key : Request.Keys) {
		JsonKeys.Add(MakeShareable(new FJsonValueString(Key)));
	}

	Json->SetArrayField("Keys", JsonKeys);

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnGetPlayerDataSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnGetPlayerDataComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/data/player/get");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::GetCurrencyBalance(FGetCurrencyBalanceRequest Request, FDelegateOnGetCurrencyBalanceSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);

	TArray <TSharedPtr<FJsonValue>> JsonKeys;
	for (auto& Key : Request.Keys) {
		JsonKeys.Add(MakeShareable(new FJsonValueString(Key)));
	}

	Json->SetArrayField("Keys", JsonKeys);

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnGetCurrencyBalanceSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnGetCurrencyBalanceComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/currency/balance/get");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::SetCurrencyBalance(FSetCurrencyBalanceRequest Request, FDelegateOnSetCurrencyBalanceSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("Key", Request.Key);
	Json->SetNumberField("Amount", Request.Amount);

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnSetCurrencyBalanceSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnSetCurrencyBalanceComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/currency/balance/set");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::GetInventory(FDelegateOnGetInventorySuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	RequestInstance->OnGetInventorySuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnGetInventoryComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/inventory/get");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::AddInventoryItem(FAddInventoryItemRequest Request, FDelegateOnAddInventoryItemSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("Key", Request.Key);
	if (Request.InstanceData != nullptr) {
		Json->SetObjectField("InstanceData", Request.InstanceData->GetJsonObject());
	}

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnAddInventoryItemSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnAddInventoryItemComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/inventory/add");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::RemoveInventoryItem(FRemoveInventoryItemRequest Request, FDelegateOnRemoveInventoryItemSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("ItemID", Request.ItemID);
	
	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnRemoveInventoryItemSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnRemoveInventoryItemComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/inventory/remove");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::RedeemGoogleIap(FRedeemGoogleIapRequest Request, FDelegateOnRedeemGoogleIapSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("Key", Request.Key);
	Json->SetStringField("PurchaseData", Request.PurchaseData);
	Json->SetStringField("PurchaseDataSignature", Request.PurchaseDataSignature);

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnRedeemGoogleIapSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnRedeemGoogleIapComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/purchase/redeem/google");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::RedeemAppleIap(FRedeemAppleIapRequest Request, FDelegateOnRedeemAppleIapSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("Key", Request.Key);
	Json->SetStringField("Receipt", Request.Receipt);
	
	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnRedeemAppleIapSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnRedeemAppleIapComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/purchase/redeem/apple");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::GetMetadataDefinitions(FGetMetadataDefinitionsRequest Request, FDelegateOnGetMetadataDefinitionsSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	if (Request.Key.Len() > 0) {
		Json->SetStringField("Key", Request.Key);
	}
	
	if (Request.Tags.Num() > 0) {
		TArray <TSharedPtr<FJsonValue>> JsonTags;
		for (auto& Tag : Request.Tags) {
			JsonTags.Add(MakeShareable(new FJsonValueString(Tag)));
		}

		Json->SetArrayField("Tags", JsonTags);
	}

	Json->SetNumberField("Page", Request.Page);

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnGetMetadataDefinitionsSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnGetMetadataDefinitionsComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/economy/definitions/metadata");

	return RequestInstance;
}


UChilliConnectSDK*
UChilliConnectSDK::RegisterPushToken(FRegisterPushTokenRequest Request, FDelegateOnRegisterPushTokenSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("Service", Request.Service);
	Json->SetStringField("DeviceToken", Request.DeviceToken);
	Json->SetBoolField("Overwrite", Request.Overwrite);

	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnRegisterPushTokenSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnRegisterPushTokenComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/push/register");

	return RequestInstance;
}

UChilliConnectSDK*
UChilliConnectSDK::GetDlcUsingTags(FGetDlcUsingTagsRequest Request, FDelegateOnGetDlcUsingTagsSuccess onSuccess, FDelegateOnError onError)
{
	UChilliConnectSDK* RequestInstance = NewObject<UChilliConnectSDK>();
	if (RequestInstance->IsSafeForRootSet()) {
		RequestInstance->AddToRoot();
	}

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	TArray <TSharedPtr<FJsonValue>> JsonTags;
	for (auto& Tag : Request.Tags) {
		JsonTags.Add(MakeShareable(new FJsonValueString(Tag)));
	}

	Json->SetArrayField("Tags", JsonTags);
	
	RequestInstance->RequestBody = GetJsonRequestBody(Json);
	RequestInstance->OnGetDlcUsingTagsSuccess = onSuccess;
	RequestInstance->OnError = onError;
	RequestInstance->OnHttpRequestProcessed.BindUObject(RequestInstance, &UChilliConnectSDK::OnGetDlcUsingTagsComplete);
	RequestInstance->RequestUrl = TEXT("https://connect.chilliconnect.com/1.0/dlc/tag");

	return RequestInstance;
}

FString
UChilliConnectSDK::GetJsonRequestBody(TSharedPtr<FJsonObject> Json)
{
	FString jsonString;
	TSharedRef<TJsonWriter<>> writer = TJsonWriterFactory<>::Create(&jsonString);
	FJsonSerializer::Serialize(Json.ToSharedRef(), writer);

	return jsonString;
}

void 
UChilliConnectSDK::Activate()
{
	IChilliConnect* Config= &(IChilliConnect::Get());

	TSharedRef<IHttpRequest> Request = FHttpModule::Get().CreateRequest();
	Request->SetVerb(TEXT("POST"));
	Request->SetURL(RequestUrl);
	Request->SetHeader(TEXT("Content-Type"), TEXT("application/json"));
	Request->SetHeader(TEXT("Game-Token"), Config->GetGameToken());
	Request->OnProcessRequestComplete().BindUObject(this, &UChilliConnectSDK::OnHttpRequestComplete);

	if (RequestBody.Len() > 0) {
		Request->SetContentAsString(RequestBody);
	}

	FString AccessToken = Config->GetConnectAccessToken();
	if (AccessToken.Len() > 0) {
		Request->SetHeader(TEXT("Connect-Access-Token"), Config->GetConnectAccessToken());
	}

	UE_LOG(LogChilliConnect, Log, TEXT("Making Request To: %s"), *RequestUrl);

	Request->ProcessRequest();
}

void 
UChilliConnectSDK::OnHttpRequestComplete(FHttpRequestPtr Request, FHttpResponsePtr Response, bool bWasSuccessful) 
{	
	UE_LOG(LogChilliConnect, Log, TEXT("Requested Completed: %s"), *Request->GetURL());
	UE_LOG(LogChilliConnect, Log, TEXT("Response Body: %s"), *Response->GetContentAsString());

	TSharedPtr<FJsonObject> JsonObject = DeserialiseHttpResponse(Response);
	if (!bWasSuccessful) {
		UE_LOG(LogChilliConnect, Log, TEXT("Request Not Succesfull"));
	}

	if (!EHttpResponseCodes::IsOk(Response->GetResponseCode()))
	{
		UE_LOG(LogChilliConnect, Log, TEXT("Request Response Not Ok"));
		
		FChilliConnectErrorResponse Error;
		Error.HttpCode = Response->GetResponseCode();
		if (JsonObject.IsValid())
		{
			if (JsonObject->HasField("Code")) {
				Error.ErrorCode = JsonObject->GetIntegerField("Code");
			}

			if (JsonObject->HasField("Error")) {
				Error.ErrorMessage = JsonObject->GetStringField("Error");
			}
		}
		
		if (OnError.IsBound()) {
			OnError.Execute(Error);
		}
	}
	else {
		
		UE_LOG(LogChilliConnect, Log, TEXT("Request Completed Ok"));

		UChilliConnectJson * ChilliConnectJson = NewObject<UChilliConnectJson>(this);
		ChilliConnectJson->SetJsonObject(JsonObject);

		if (!OnHttpRequestProcessed.IsBound()) {
			UE_LOG(LogChilliConnect, Log, TEXT("No OnHttpRequestProcess Handler Bound"));
		}
		else {
			OnHttpRequestProcessed.Execute(ChilliConnectJson);
		}
	}
}

void
UChilliConnectSDK::OnLoginUsingChilliConnectComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnLoginUsingChilliConnectComplete Processing Response"));

	FLoginUsingChilliConnectResponse Response;
	Response.ConnectAccessToken = json->GetJsonObject()->GetStringField("ConnectAccessToken");
	if (json->GetJsonObject()->HasField("MetricsAccessToken")) {
		Response.MetricsAccessToken = json->GetJsonObject()->GetStringField("MetricsAccessToken");
	}
	
	IChilliConnect* Config = &(IChilliConnect::Get());
	Config->SetConnectAccessToken(Response.ConnectAccessToken);
	if (json->GetJsonObject()->HasField("MetricsAccessToken")) {
		Config->SetMetricsAccessToken(Response.MetricsAccessToken);
	}

	if (!OnLoginUsingChilliConnectSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnLoginUsingChilliConnectSuccess Handler Bound"));
	}
	else {
		OnLoginUsingChilliConnectSuccess.ExecuteIfBound(Response);
	}
}

void 
UChilliConnectSDK::OnCreatePlayerComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnCreatePlayerComplete Processing Response"));

	FCreatePlayerResponse Response;
	Response.ChilliConnectID = json->GetJsonObject()->GetStringField("ChilliConnectID");
	Response.ChilliConnectSecret = json->GetJsonObject()->GetStringField("ChilliConnectSecret");

	if (!OnCreatePlayerSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnCreatePlayerSuccess Handler Bound"));
	}
	else {
		OnCreatePlayerSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnGetPlayerDataComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnGetPlayerDataComplete Processing Response"));

	FGetPlayerDataResponse Response;

	TArray < TSharedPtr < FJsonValue > > Values = json->GetJsonObject()->GetArrayField("Values");
	for (auto& Value : Values) {
		TSharedPtr < FJsonObject > JsonObjectValue = Value->AsObject();

		FGetPlayerDataResponseValue ResponseValue;
		ResponseValue.Key = JsonObjectValue->GetStringField("Key");
		ResponseValue.DateCreated = JsonObjectValue->GetStringField("DateCreated");
		ResponseValue.DateModified = JsonObjectValue->GetStringField("DateModified");
		ResponseValue.HasAttachment = JsonObjectValue->GetBoolField("HasAttachment");
		ResponseValue.WriteLock = JsonObjectValue->GetStringField("WriteLock");
		
		UChilliConnectJson * ValueJson = NewObject<UChilliConnectJson>();
		ValueJson->SetJsonObject(JsonObjectValue->GetObjectField("Value"));
		ResponseValue.Value = ValueJson;
		
		Response.Values.Add(ResponseValue);
	}

	if (!OnGetPlayerDataSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnGetPlayerDataSuccess Handler Bound"));
	}
	else {
		OnGetPlayerDataSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnSetPlayerDataComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnSetPlayerDataComplete Processing Response"));

	FSetPlayerDataResponse Response;
	Response.WriteLock = json->GetJsonObject()->GetStringField("WriteLock");
	
	if (!OnSetPlayerDataSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnSetPlayerDataSuccess Handler Bound"));
	}
	else {
		OnSetPlayerDataSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnGetCurrencyBalanceComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnGetCurrencyBalanceComplete Processing Response"));

	FGetCurrencyBalanceResponse Response;
	TArray < TSharedPtr < FJsonValue > > Values = json->GetJsonObject()->GetArrayField("Balances");
	for (auto& Value : Values) {
		TSharedPtr < FJsonObject > JsonObjectValue = Value->AsObject();

		FGetCurrencyBalanceResponseValue ResponseValue;
		ResponseValue.Key = JsonObjectValue->GetStringField("Key");
		ResponseValue.Name = JsonObjectValue->GetStringField("Name");
		ResponseValue.Balance = JsonObjectValue->GetIntegerField("Balance");
		ResponseValue.WriteLock = JsonObjectValue->GetStringField("WriteLock");

		Response.Values.Add(ResponseValue);
	}

	if (!OnGetCurrencyBalanceSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnGetCurrencyBalanceSuccess Handler Bound"));
	}
	else {
		OnGetCurrencyBalanceSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnSetCurrencyBalanceComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnSetCurrencyBalanceComplete Processing Response"));

	FSetCurrencyBalanceResponse Response;
	
	Response.Key = json->GetJsonObject()->GetStringField("Key");
	Response.Balance = json->GetJsonObject()->GetIntegerField("Balance");
	
	if (!OnSetCurrencyBalanceSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No SetCurrencyBalanceSuccess Handler Bound"));
	}
	else {
		OnSetCurrencyBalanceSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnGetInventoryComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnGetInventoryComplete Processing Response"));

	FGetInventoryResponse Response;
	TArray < TSharedPtr < FJsonValue > > Values = json->GetJsonObject()->GetArrayField("Items");
	for (auto& Value : Values) {
		TSharedPtr < FJsonObject > JsonObjectValue = Value->AsObject();

		FGetInventoryResponseItem ResponseItem;
		ResponseItem.ItemID = JsonObjectValue->GetStringField("ItemID");
		ResponseItem.Key = JsonObjectValue->GetStringField("Key");
		ResponseItem.Name = JsonObjectValue->GetStringField("Name");
		
		if (JsonObjectValue->HasTypedField<EJson::Object>("InstanceData") ) {
			UChilliConnectJson * ValueJson = NewObject<UChilliConnectJson>();
			ValueJson->SetJsonObject(JsonObjectValue->GetObjectField("InstanceData"));
			ResponseItem.InstanceData = ValueJson;
		}
		
		Response.Items.Add(ResponseItem);
	}

	if (!OnGetInventorySuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No GetInventorySuccess Handler Bound"));
	}
	else {
		OnGetInventorySuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnAddInventoryItemComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnAddInventoryItemComplete Processing Response"));

	FAddInventoryItemResponse Response;
	Response.ItemID = json->GetJsonObject()->GetStringField("ItemID");
	
	if (!OnAddInventoryItemSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnAddInventoryItemSuccess Handler Bound"));
	}
	else {
		OnAddInventoryItemSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnRemoveInventoryItemComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnRemoveInventoryItemComplete Processing Response"));

	if (!OnRemoveInventoryItemSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnRemoveInventoryItemSuccess Handler Bound"));
	}
	else {
		OnRemoveInventoryItemSuccess.ExecuteIfBound();
	}
}


void
UChilliConnectSDK::OnRedeemGoogleIapComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnRedeemGoogleIapComplete Processing Response"));

	FRedeemIapResponse Response = GetIapResponse(json->GetJsonObject());
	
	if (!OnRedeemGoogleIapSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnRedeemGoogleIapComplete Handler Bound"));
	}
	else {
		OnRedeemGoogleIapSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnRedeemAppleIapComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnRedeemAppleIapComplete Processing Response"));

	FRedeemIapResponse Response = GetIapResponse(json->GetJsonObject());

	if (!OnRedeemAppleIapSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnRedeemAppleIapSuccess Handler Bound"));
	}
	else {
		OnRedeemAppleIapSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnRegisterPushTokenComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnRegisterPushTokenComplete Processing Response"));

	if (!OnRegisterPushTokenSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnRegisterPushTokenSuccess Handler Bound"));
	}
	else {
		OnRegisterPushTokenSuccess.ExecuteIfBound();
	}
}

void
UChilliConnectSDK::OnGetMetadataDefinitionsComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnGetMetadataDefinitionsComplete Processing Response"));

	FGetMetadataDefinitionsResponse Response;
	Response.Total = json->GetJsonObject()->GetIntegerField("Total");
	Response.Page = json->GetJsonObject()->GetIntegerField("Page");
	Response.PageSize = json->GetJsonObject()->GetIntegerField("PageSize");
	
	TArray < TSharedPtr < FJsonValue > > ItemsJson = json->GetJsonObject()->GetArrayField("Items");
	for (auto& ItemJson : ItemsJson) {

		TSharedPtr < FJsonObject > JsonMetadataResponseItem = ItemJson->AsObject();

		FGetMetadataDefinitionsResponseItem ResponseItem;
		ResponseItem.Key = JsonMetadataResponseItem->GetStringField("Key");
		ResponseItem.Name = JsonMetadataResponseItem->GetStringField("Name");

		if (JsonMetadataResponseItem->HasField("Tags")) {
			TArray < TSharedPtr < FJsonValue > > TagsJson = JsonMetadataResponseItem->GetArrayField("Tags");
			for (auto& TagJson : TagsJson) {
				ResponseItem.Tags.Add(TagJson->AsString());
			}
		}

		if (JsonMetadataResponseItem->HasTypedField<EJson::Object>("CustomData")) {
			UChilliConnectJson * ValueJson = NewObject<UChilliConnectJson>();
			ValueJson->SetJsonObject(JsonMetadataResponseItem->GetObjectField("CustomData"));
			ResponseItem.CustomData = ValueJson;
		}

		Response.Items.Add(ResponseItem);
	}

	if (!OnGetMetadataDefinitionsSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnGetMetadataDefinitionsSuccess Handler Bound"));
	}
	else {
		OnGetMetadataDefinitionsSuccess.ExecuteIfBound(Response);
	}
}

void
UChilliConnectSDK::OnGetDlcUsingTagsComplete(UChilliConnectJson* json)
{
	UE_LOG(LogChilliConnect, Log, TEXT("OnGetDlcUsingTagsComplete Processing Response"));

	FGetDlcUsingTagsResponse Response;
	

	TArray < TSharedPtr < FJsonValue > > PackagesJson = json->GetJsonObject()->GetArrayField("Packages");
	for (auto& PackageJson : PackagesJson) {

		TSharedPtr < FJsonObject > PackageJsonItem = PackageJson->AsObject();

		FGetDlcUsingTagsPackageResponse PackageResponseItem;
		PackageResponseItem.Type = PackageJsonItem->GetStringField("Type");
		PackageResponseItem.Name = PackageJsonItem->GetStringField("Name");
		PackageResponseItem.Checksum = PackageJsonItem->GetStringField("Checksum");
		PackageResponseItem.DateUploaded = PackageJsonItem->GetStringField("DateUploaded");
		PackageResponseItem.Url = PackageJsonItem->GetStringField("Url");
		PackageResponseItem.Size = PackageJsonItem->GetIntegerField("Size");
		
		if (PackageJsonItem->HasField("Files")) {
			TArray < TSharedPtr < FJsonValue > > FilesJson = PackageJsonItem->GetArrayField("Files");
			for (auto& FileJson : FilesJson) {

				TSharedPtr < FJsonObject > FileJsonItem = FileJson->AsObject();

				FGetDlcUsingTagsFileResponse FileResponseItem;
				FileResponseItem.Name = FileJsonItem->GetStringField("Name");
				FileResponseItem.Checksum = FileJsonItem->GetStringField("Checksum");
				FileResponseItem.Size = FileJsonItem->GetIntegerField("Size");

				PackageResponseItem.Files.Add(FileResponseItem);
			}
		}

		Response.Packages.Add(PackageResponseItem);
	}

	if (!OnGetDlcUsingTagsSuccess.IsBound()) {
		UE_LOG(LogChilliConnect, Log, TEXT("No OnGetDlcUsingTagsSuccess Handler Bound"));
	}
	else {
		OnGetDlcUsingTagsSuccess.ExecuteIfBound(Response);
	}
}

TSharedPtr<FJsonObject> 
UChilliConnectSDK::DeserialiseHttpResponse(FHttpResponsePtr HttpResponse)
{
	TSharedPtr<FJsonObject> JsonObject;
	TSharedRef<TJsonReader<>> Reader = TJsonReaderFactory<>::Create(HttpResponse->GetContentAsString());
	if (FJsonSerializer::Deserialize(Reader, JsonObject))
	{
		return JsonObject;
	}

	return nullptr;
}

FRedeemIapResponse 
UChilliConnectSDK::GetIapResponse(TSharedPtr<FJsonObject> Json)
{
	FRedeemIapResponse Response;
	Response.Redeemed = Json->GetBoolField("Redeemed");
	Response.Status = Json->GetStringField("Status");

	if (Json->HasField("Rewards")) {
		TSharedPtr < FJsonObject > RewardsJson = Json->GetObjectField("Rewards");

		if (RewardsJson->HasField("Currencies")) {
			TArray < TSharedPtr < FJsonValue > > CurrencyRewardsJson = RewardsJson->GetArrayField("Currencies");
			for (auto& CurrencyRewardJson : CurrencyRewardsJson) {
				TSharedPtr < FJsonObject > JsonObjectValue = CurrencyRewardJson->AsObject();

				FRedeemIapResponseCurrencyReward CurrencyReward;
				CurrencyReward.Key = JsonObjectValue->GetStringField("Key");
				CurrencyReward.Name = JsonObjectValue->GetStringField("Name");
				CurrencyReward.Amount = JsonObjectValue->GetNumberField("Amount");

				Response.Rewards.Currencies.Add(CurrencyReward);
			}
		}

		if (RewardsJson->HasField("Items")) {
			TArray < TSharedPtr < FJsonValue > > InventoryRewardsJson = RewardsJson->GetArrayField("Items");
			for (auto& InventoryRewardJson : InventoryRewardsJson) {
				TSharedPtr < FJsonObject > JsonObjectValue = InventoryRewardJson->AsObject();

				FRedeemIapResponseInventoryItemReward InventoryReward;
				InventoryReward.Key = JsonObjectValue->GetStringField("Key");
				InventoryReward.Name = JsonObjectValue->GetStringField("Name");
				InventoryReward.Amount = JsonObjectValue->GetNumberField("Amount");

				TArray < TSharedPtr < FJsonValue > > ItemIDsJson = RewardsJson->GetArrayField("ItemIDs");
				for (auto& ItemIDJson : ItemIDsJson) {
					InventoryReward.ItemIDs.Add(ItemIDJson->AsString());
				}

				Response.Rewards.Items.Add(InventoryReward);
			}
		}
	}

	return Response;
}
