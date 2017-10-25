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

		UChilliConnectJson * ChilliConnectJson = NewObject<UChilliConnectJson>();
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
