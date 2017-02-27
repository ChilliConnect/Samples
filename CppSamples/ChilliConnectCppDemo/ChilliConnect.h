#pragma once

#include "HttpSystem.h"
#include "json/json.h"

#include <string>
#include <memory>

struct ChilliConnectResponse
{
	bool wasOk;
	int errorCode;
	std::string errorMessage;
	std::string rawBody;
};

struct CreatePlayerResponse : ChilliConnectResponse
{
	std::string chilliConnectId;
	std::string chilliConnectSecret;
};

struct LoginResponse : ChilliConnectResponse
{
	std::string accessToken;
};

struct AddScoreResponse : ChilliConnectResponse
{
	int globalRank;
	int totalScores;
};

struct Score {
	std::string chilliConnectId;
	int score;
	std::string dateSet;
};

struct GetScoresResponse : ChilliConnectResponse
{
	std::vector<Score> scores;
};

class ChilliConnect
{

private:
	std::string gameToken;
	std::unique_ptr<HttpSystem> httpSystem;
	bool parse(const HttpResult &httpResult, ChilliConnectResponse & response, Json::Value & json);

public:
	ChilliConnect(const std::string & gameToken);
	
	CreatePlayerResponse CreatePlayer();
	LoginResponse Login(const std::string & chilliConnectId, const std::string & chilliConnectSecret);
	AddScoreResponse AddScore(const std::string & accessToken, const std::string & leaderboardKey, const int score);
	GetScoresResponse GetScores(const std::string & accessToken, const std::string & leaderboardKey);
};

