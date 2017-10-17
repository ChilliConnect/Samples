// Copyright (c) 2016 Tag Games Ltd. All rights reserved

#pragma once

#include "Http.h"
#include "Json.h"
#include <queue>

/**
* Represents a generic response from a ChilliConnect API Request
*/
struct FChilliConnectResponse
{
	/**
	 * Did the request complete and return a valid 200 HTTP response
	 */
	bool bWasOk;

	/**
	 * Any ChilliConnect Error Code returned by the request
	 */
	uint8 ErrorCode;

	/**
	 * The HTTP resposne code of the API re
	 */
	uint8 HttpCode;

	/**
	 * Any ChilliConnect Error Message returned by the request
	 */
	FString ErrorMessage;

	/**
	 * The JSON body of the response from the ChilliConnectRequest
	 */
	TSharedPtr<FJsonObject> Body;
};

/**
 * Represents the response from a CreatedPlayer API Request
 */
struct FCreatePlayerResponse : FChilliConnectResponse
{
	/** 
	 * The ChilliConnectID of the newly created plauer
	 */
	FString ChilliConnectID;

	/**
	 * The ChilliConnectID of the newly created player
	 */
	FString ChilliConnectSecret;
};

/**
 * Represents the response from a LoginPlayer API Request
 */
struct FLoginResponse : FChilliConnectResponse
{
	/**
	 * The AccessToken returned from a succesfull login. Is used to 
	 * authenticate subsequent requests
	 */
	FString AccessToken;
};

/**
 * Represents the response from a AddLeaderboardScore API Request
 */
struct FAddLeaderboardScoreResponse : FChilliConnectResponse
{
	/*
	 * The players position within the leaderboard
	 */
	uint8 GlobalRank;

	/*
	* The total number of scores posted to the leaderboard
	*/
	uint8 GlobalTotal;
};

class CHILLICONNECTEXAMPLE_API FChilliConnect
{

public:

	FChilliConnect();

	/**
	 * Event broadcast when PlayerCreated API request returns
	*/
	DECLARE_EVENT_OneParam(FChilliConnect, FPlayerCreatedEvent, FCreatePlayerResponse);
	
	/**
	* Event broadcast when LoginPlayer API request returns
	*/
	DECLARE_EVENT_OneParam(FChilliConnect, FPlayerLoggedInEvent, FLoginResponse);

	/**
	* Event broadcast when AddLeaderboardScore API request returns
	*/
	DECLARE_EVENT_OneParam(FChilliConnect, FLeaderboardScoreAddedEvent, FAddLeaderboardScoreResponse);
	
	/**
	 * Create a new Player account in ChilliConnect. Response can by handled by subscribing to the 
	 * FPlayerCreatedEvent.
	 */
	void CreatePlayer();

	/**
	 * Login a player account to ChilliConnect. Response can be handled by subscribing to the 
	 * FPlayerLoggedInEvent
	 */
	void LoginPlayer(FString ChilliConnectID, FString ChilliConnectSecret);

	/**
	* Add a leaderboard score. Response can be handled by subscribing to the FAddLeaderboardScoreResponse
	*/
	void AddLeaderboardScore(FString AccessToken, FString LeaderboardKey, uint8 Score);
	
	FPlayerCreatedEvent& OnPlayerCreated() { return PlayerCreatedEvent; };
	FPlayerLoggedInEvent& OnPlayerLoggedIn() { return PlayerLoggedInEvent; };
	FLeaderboardScoreAddedEvent& OnLeaderboardScoreAdded() { return LeaderboardScoreAddedEvent; };

	void Update();

private:

	enum ERequestType
	{
		CreatePlayerRequestType,
		LoginPlayerRequestType,
		AddLeaderboardScoreRequestType
	};

	struct FPendingRequest
	{
		FPendingRequest(FHttpRequestPtr&& request, ERequestType type)
			: Request(std::move(request)), Type(type) {}

		FHttpRequestPtr Request;
		ERequestType Type;
	};

	FString GameToken;
	
	FHttpRequestPtr ProcessingRequest;
	std::queue<FPendingRequest> PendingRequests;
	
	TSharedPtr<FJsonObject> DeserialiseHttpResponse(FHttpResponsePtr HttpResponse);
	
	void QueueRequest(FHttpRequestPtr&& HttpRequest, ERequestType RequestType);
	void OnRequestFinished(ERequestType RequestType, const FHttpResponsePtr& HttpResponse);
	bool PopulateResponse(const FHttpResponsePtr& HttpResponse, FChilliConnectResponse& ChilliConnectResponse);

	void OnCreatePlayerFinished(const FHttpResponsePtr& HttpResponse);
	void OnLoginFinished(const FHttpResponsePtr& HttpResponse);
	void OnAddLeaderboardScoreFinished(const FHttpResponsePtr& HttpResponse);

	void SetJsonRequestBody(const FHttpRequestPtr& HttpRequest, TSharedPtr<FJsonObject> Json);

	FPlayerCreatedEvent PlayerCreatedEvent;
	FPlayerLoggedInEvent PlayerLoggedInEvent;
	FLeaderboardScoreAddedEvent LeaderboardScoreAddedEvent;
	
};
