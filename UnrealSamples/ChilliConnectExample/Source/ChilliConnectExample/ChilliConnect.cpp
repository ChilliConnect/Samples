// Fill out your copyright notice in the Description page of Project Settings.

#include "ChilliConnect.h"
#include "Blueprint/UserWidget.h"
#include "Json.h"

FChilliConnect::FChilliConnect()
{
	// Replace with your own Game Token
	GameToken = "";
}

void FChilliConnect::CreatePlayer()
{
	//https://docs.chilliconnect.com/api/?system=http#api-PlayerAccounts-CreatePlayer
	
	TSharedRef<IHttpRequest> CreateRequest = FHttpModule::Get().CreateRequest();
	CreateRequest->SetVerb(TEXT("POST"));
	CreateRequest->SetURL(TEXT("https://connect.chilliconnect.com/1.0/player/create"));
	CreateRequest->SetHeader(TEXT("Content-Type"), TEXT("application/json"));
	CreateRequest->SetHeader(TEXT("Game-Token"), GameToken);

	QueueRequest(CreateRequest, ERequestType::CreatePlayerRequestType);
}

void FChilliConnect::LoginPlayer(FString ChilliConnectID, FString ChilliConnectSecret)
{
	//https://docs.chilliconnect.com/api/?system=http#api-PlayerAccounts-CreatePlayer

	TSharedRef<IHttpRequest> LoginRequest = FHttpModule::Get().CreateRequest();
	LoginRequest->SetVerb(TEXT("POST"));
	LoginRequest->SetURL(TEXT("https://connect.chilliconnect.com/1.0/player/login/chilli"));
	LoginRequest->SetHeader(TEXT("Content-Type"), TEXT("application/json"));
	LoginRequest->SetHeader(TEXT("Game-Token"), GameToken);

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("ChilliConnectID", *ChilliConnectID);
	Json->SetStringField("ChilliConnectSecret", *ChilliConnectSecret);

	SetJsonRequestBody(LoginRequest, Json);

	QueueRequest(LoginRequest, ERequestType::LoginPlayerRequestType);
}

void FChilliConnect::AddLeaderboardScore(FString AccessToken, FString LeaderboardKey, uint8 Score)
{
	//https://docs.chilliconnect.com/api/?system=http#api-Leaderboards-AddScore

	TSharedRef<IHttpRequest> AddScoreRequest = FHttpModule::Get().CreateRequest();
	AddScoreRequest->SetVerb(TEXT("POST"));
	AddScoreRequest->SetURL(TEXT("https://connect.chilliconnect.com/1.0/leaderboard/scores/add"));
	AddScoreRequest->SetHeader(TEXT("Content-Type"), TEXT("application/json"));
	AddScoreRequest->SetHeader(TEXT("Connect-Access-Token"), AccessToken);

	TSharedPtr<FJsonObject> Json = MakeShareable(new FJsonObject);
	Json->SetStringField("Key", *LeaderboardKey);
	Json->SetNumberField("Score", Score);

	SetJsonRequestBody(AddScoreRequest, Json);

	QueueRequest(AddScoreRequest, ERequestType::AddLeaderboardScoreRequestType);
}

void FChilliConnect::SetJsonRequestBody(const FHttpRequestPtr& HttpRequest, TSharedPtr<FJsonObject> Json)
{
	FString jsonString;
	TSharedRef<TJsonWriter<>> writer = TJsonWriterFactory<>::Create(&jsonString);
	FJsonSerializer::Serialize(Json.ToSharedRef(), writer);

	HttpRequest->SetContentAsString(TCHAR_TO_UTF8(*jsonString));
}

void FChilliConnect::QueueRequest(FHttpRequestPtr&& HttpRequest, ERequestType RequestType)
{
	PendingRequests.push(FPendingRequest(std::move(HttpRequest), RequestType));
}

void FChilliConnect::Update()
{
	if (PendingRequests.empty())
	{
		return;
	}
	
	if (ProcessingRequest.IsValid())
	{
		bool bRequestFinished = ProcessingRequest->GetStatus() == EHttpRequestStatus::Failed || ProcessingRequest->GetStatus() == EHttpRequestStatus::Failed_ConnectionError || ProcessingRequest->GetStatus() == EHttpRequestStatus::Succeeded;
		if (bRequestFinished)
		{
			FHttpResponsePtr HttpResponse = ProcessingRequest->GetResponse();
			auto CurrentRequest = PendingRequests.front();

			if (HttpResponse.IsValid() == false)
			{
				UE_LOG(LogTemp, Error, TEXT("ChilliConnect - HTTP request no valid response from Url: %s."), *ProcessingRequest->GetURL());

				ProcessingRequest = nullptr;
				PendingRequests.pop();

				OnRequestFinished(CurrentRequest.Type, HttpResponse);
			}
			else if (EHttpResponseCodes::IsOk(HttpResponse->GetResponseCode()))
			{
				UE_LOG(LogTemp, Display, TEXT("ChilliConnect - HTTP request successful to Url: %s"), *ProcessingRequest->GetURL());

				ProcessingRequest = nullptr;
				PendingRequests.pop();

				OnRequestFinished(CurrentRequest.Type, HttpResponse);
			}
			else if (HttpResponse->GetResponseCode() == EHttpResponseCodes::ServiceUnavail || ProcessingRequest->GetStatus() == EHttpRequestStatus::Failed_ConnectionError)
			{
				// If service is temporarily unavailble we try this request again so don't pop it
				UE_LOG(LogTemp, Warning, TEXT("ChilliConnect - HTTP request service unavailable to Url: %s. Trying again"), *ProcessingRequest->GetURL());
				ProcessingRequest = nullptr;
			}
			else
			{
				UE_LOG(LogTemp, Error, TEXT("ChilliConnect - HTTP request error. Code: %d. Content: %s"), HttpResponse->GetResponseCode(), *HttpResponse->GetContentAsString());

				ProcessingRequest = nullptr;
				PendingRequests.pop();

				OnRequestFinished(CurrentRequest.Type, HttpResponse);
			}
		}
	}
	else if (PendingRequests.size() > 0)
	{
		ProcessingRequest = PendingRequests.front().Request;

		UE_LOG(LogTemp, Display, TEXT("ChilliConnect - HTTP request starting to Url: %s."), *ProcessingRequest->GetURL());
		if (ProcessingRequest->ProcessRequest() == false)
		{
			UE_LOG(LogTemp, Error, TEXT("ChilliConnect - HTTP request error to Url: %s."), *ProcessingRequest->GetURL());
		}
	}
}

void FChilliConnect::OnRequestFinished(ERequestType requestType, const FHttpResponsePtr& response)
{
	switch (requestType)
	{
	case CreatePlayerRequestType:
		OnCreatePlayerFinished(response);
		break;
	case LoginPlayerRequestType:
		OnLoginFinished(response);
		break;
	case AddLeaderboardScoreRequestType:
		OnAddLeaderboardScoreFinished(response);
		break;
	}
}

void FChilliConnect::OnLoginFinished(const FHttpResponsePtr& HttpResponse)
{
	FLoginResponse Response;
	if (PopulateResponse(HttpResponse, Response)) {
		Response.AccessToken = Response.Body->GetStringField("ConnectAccessToken");
		
		UE_LOG(LogTemp, Display, TEXT("ChilliConnect - Logged In Player"))
	}

	PlayerLoggedInEvent.Broadcast(Response);
}

void FChilliConnect::OnCreatePlayerFinished(const FHttpResponsePtr& HttpResponse)
{
	FCreatePlayerResponse Response;
	if (PopulateResponse(HttpResponse, Response)) {
		Response.ChilliConnectID = Response.Body->GetStringField("ChilliConnectID");
		Response.ChilliConnectSecret = Response.Body->GetStringField("ChilliConnectSecret");

		UE_LOG(LogTemp, Display, TEXT("ChilliConnect - Created Player: %s."), *Response.ChilliConnectID);
	}
	
	PlayerCreatedEvent.Broadcast(Response);
}

void FChilliConnect::OnAddLeaderboardScoreFinished(const FHttpResponsePtr& HttpResponse)
{
	FAddLeaderboardScoreResponse Response;
	if (PopulateResponse(HttpResponse, Response)) {
		Response.GlobalRank = Response.Body->GetIntegerField("GlobalRank");
		Response.GlobalTotal = Response.Body->GetIntegerField("GlobalTotal");

		UE_LOG(LogTemp, Display, TEXT("ChilliConnect - Added Leaderboard Score. Position: %d of %d."), Response.GlobalRank, Response.GlobalTotal);
	}

	LeaderboardScoreAddedEvent.Broadcast(Response);
}

bool FChilliConnect::PopulateResponse(const FHttpResponsePtr& HttpResponse, FChilliConnectResponse& Response)
{
	Response.HttpCode = HttpResponse->GetResponseCode();
	Response.bWasOk = HttpResponse.IsValid() && EHttpResponseCodes::IsOk(HttpResponse->GetResponseCode());

	TSharedPtr<FJsonObject> JsonObject = DeserialiseHttpResponse(HttpResponse);
	if (!JsonObject.IsValid()) {
		return false;
	}

	Response.Body = JsonObject;
	if (!Response.bWasOk) {
		Response.ErrorCode = JsonObject->GetIntegerField("Code");
		Response.ErrorMessage = JsonObject->GetStringField("Error");

		UE_LOG(LogTemp, Display, TEXT("ChilliConnect - Error Making Request %s"), *Response.ErrorMessage);

		return false;
	}

	return true;
}

TSharedPtr<FJsonObject> FChilliConnect::DeserialiseHttpResponse(FHttpResponsePtr HttpResponse)
{
	TSharedPtr<FJsonObject> JsonObject;
	TSharedRef<TJsonReader<>> Reader = TJsonReaderFactory<>::Create(HttpResponse->GetContentAsString());
	if (FJsonSerializer::Deserialize(Reader, JsonObject))
	{
		return JsonObject;
	}

	return nullptr;
}