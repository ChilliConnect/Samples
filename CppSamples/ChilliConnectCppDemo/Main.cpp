#include <iostream>
#include <time.h> 
#include "ChilliConnect.h"

using namespace std;

int main()
{
	//Replace these with the relevant values from your own dashboard
	const string gameToken = "<GAME-TOKEN>";
	const string leaderboardKey = "MY_LEADERBOARD";

	//Create a new instance of the ChilliConnect client
	ChilliConnect * chilliConnect = new ChilliConnect(gameToken);

	//Create a new player account - usually, in a real game you would save the ChilliConnectID
	//and ChilliConnectSecret and attempt to load on start up.
	CreatePlayerResponse createPlayer = chilliConnect->CreatePlayer();
	if (!createPlayer.wasOk) {
		cout << "Error Creating ChilliConnect Player: " << createPlayer.rawBody;
		cin.ignore();
		exit(1);
	}

	cout << "Created New Player With ChilliConnectID: " << createPlayer.chilliConnectId << endl;

	//Login in the player. Login is used to return an AccessToken that is used to authenticate
	//subsequent requests
	string chilliConnectId = createPlayer.chilliConnectId;
	string chilliConnectSecret = createPlayer.chilliConnectSecret;
	LoginResponse login = chilliConnect->Login(chilliConnectId, chilliConnectSecret);
	if (!login.wasOk) {
		cout << "Error Logging In Player:" << login.rawBody;
		cin.ignore();
		exit(1);
	}

	cout << "Player Logged In" << endl;

	string accessToken = login.accessToken;

	//Add a random leaderboard score for the player
	srand(time(NULL));
	int score = rand() % 1000;
	AddScoreResponse addScore = chilliConnect->AddScore(accessToken, leaderboardKey, score);
	if (!addScore.wasOk) {
		cout << "Error Adding Score:" << addScore.rawBody;
		cin.ignore();
		exit(1);
	}

	cout << "Added Score. Player Is Ranked " << addScore.globalRank << " Of " << addScore.totalScores << endl;
	
	//Print out the top scores on the leaderboard
	GetScoresResponse getScores = chilliConnect->GetScores(accessToken, leaderboardKey);
	if (!getScores.wasOk) {
		cout << "Error Getting Scores:" << addScore.rawBody;
		cin.ignore();
		exit(1);
	}

	cout << "Top Scores:" << endl;
	for (const auto& score : getScores.scores) {
		cout << get<0>(score) << ":" << get<1>(score) << "( " << get<2>(score) << " )" << endl;
	}

	cin.ignore();
	return 0;
}