#include "ChilliConnect.h"

using std::string;
using std::map;
using std::vector;
using std::unique_ptr;

ChilliConnect::ChilliConnect(const string & gameToken)
{
	this->gameToken = gameToken;
	this->httpSystem = unique_ptr<HttpSystem>(new HttpSystem());
}

bool
ChilliConnect::parse(const HttpResult &httpResult, ChilliConnectResponse & response, Json::Value & json)
{
	response.wasOk = (httpResult.code == 200);
	response.rawBody = httpResult.body;

	bool parsingSuccessful = false;
	if (!httpResult.body.empty()) {
		Json::Reader reader;
		parsingSuccessful = reader.parse(httpResult.body, json);
		if (!response.wasOk) {
			response.errorCode = json["Code"].asInt();
			response.errorMessage = json["Error"].asString();
		}
	}

	return parsingSuccessful;
}

CreatePlayerResponse
ChilliConnect::CreatePlayer() 
{
	//https://docs.chilliconnect.com/api/?system=http#api-PlayerAccounts-CreatePlayer

	map<string, string> headers;
	headers["Game-Token"] = "Vv7VANzImRtEUeiYaoz4lWKqB6t349iy";

	HttpResult httpResult = this->httpSystem->MakePostRequest("connect.chilliconnect.com", "1.0/player/create", headers, "");

	CreatePlayerResponse response;
	
	Json::Value json;
	if (parse(httpResult, response, json) && response.wasOk) {
		response.chilliConnectId = json["ChilliConnectID"].asString();
		response.chilliConnectSecret = json["ChilliConnectSecret"].asString();
	}

	return response;	
}

LoginResponse
ChilliConnect::Login(const string & chilliConnectId, const string & chilliConnectSecret)
{
	//https://docs.chilliconnect.com/api/?system=http#api-PlayerAccounts-LogInUsingChilliConnect

	map<string, string> headers;
	headers["Game-Token"] = this->gameToken;

	Json::Value requestBody;
	requestBody["ChilliConnectID"] = chilliConnectId;
	requestBody["ChilliConnectSecret"] = chilliConnectSecret;

	HttpResult httpResult = this->httpSystem->MakePostRequest("connect.chilliconnect.com", "1.0/player/login/chilli", headers, requestBody.toStyledString());

	LoginResponse response;
	Json::Value json;
	if (parse(httpResult, response, json) && response.wasOk) {
		response.accessToken = json["ConnectAccessToken"].asString();
	}

	return response;
}

AddScoreResponse 
ChilliConnect::AddScore(const string & accessToken, const string & leaderboardKey, const int score)
{
	//https://docs.chilliconnect.com/api/?system=http#api-Leaderboards-AddScore

	map<string, string> headers;
	headers["Connect-Access-Token"] = accessToken;

	Json::Value requestBody;
	requestBody["Key"] = leaderboardKey;
	requestBody["Score"] = score;
	
	HttpResult httpResult = this->httpSystem->MakePostRequest("connect.chilliconnect.com", "1.0/leaderboard/scores/add", headers, requestBody.toStyledString());

	AddScoreResponse response;
	Json::Value json;
	if (parse(httpResult, response, json) && response.wasOk) {
		response.globalRank = json["GlobalRank"].asInt();
		response.totalScores = json["GlobalTotal"].asInt();
	}

	return response;
}

GetScoresResponse
ChilliConnect::GetScores(const string & accessToken, const string & leaderboardKey)
{
	//https://docs.chilliconnect.com/api/?system=http#api-Leaderboards-GetScores

	map<string, string> headers;
	headers["Connect-Access-Token"] = accessToken;

	Json::Value requestBody;
	requestBody["Key"] = leaderboardKey;
	
	HttpResult httpResult = this->httpSystem->MakePostRequest("connect.chilliconnect.com", "1.0/leaderboard/scores/get", headers, requestBody.toStyledString());

	GetScoresResponse response;
	Json::Value json;
	if (parse(httpResult, response, json) && response.wasOk) {
		for (const auto& jsonScore : json["Scores"]) {
			Score score;
			score.chilliConnectId = jsonScore["ChilliConnectID"].asString();
			score.score = jsonScore["Score"].asInt();
			score.dateSet = jsonScore["Date"].asString();

			response.scores.push_back(score);
		}
	}

	return response;
}