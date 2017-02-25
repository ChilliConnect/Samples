#pragma once

#include "HttpSystem.h"
#include "json/json.h"

#include <string>
#include <memory>

using std::string;
using std::unique_ptr;
using std::tuple;
using std::vector;

struct ChilliConnectResponse
{
	bool wasOk;
	int errorCode;
	string errorMessage;
	string rawBody;
};

struct CreatePlayerResponse : ChilliConnectResponse
{
	string chilliConnectId;
	string chilliConnectSecret;
};

struct LoginResponse : ChilliConnectResponse
{
	string accessToken;
};

struct AddScoreResponse : ChilliConnectResponse
{
	int globalRank;
	int totalScores;
};

struct GetScoresResponse : ChilliConnectResponse
{
	vector<tuple<string, int, string>> scores;
};

class ChilliConnect
{

private:
	string gameToken;
	unique_ptr<HttpSystem> httpSystem;
	bool parse(const HttpResult &httpResult, ChilliConnectResponse & response, Json::Value & json);

public:
	ChilliConnect(const string gameToken);
	~ChilliConnect();

	CreatePlayerResponse CreatePlayer();
	LoginResponse Login(const string chilliConnectId, const string chilliConnectSecret);
	AddScoreResponse AddScore(const string accessToken, const string leaderboardKey, const int score);
	GetScoresResponse GetScores(const string accessToken, const string leaderboardKey);
};

