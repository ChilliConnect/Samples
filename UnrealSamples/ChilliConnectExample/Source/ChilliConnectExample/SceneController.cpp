#include "SceneController.h"
#include "ChilliConnect.h"

USceneController::USceneController()
{
	PrimaryComponentTick.bCanEverTick = true;
	ChilliConnect = new FChilliConnect();
}

// Called when the game starts
void USceneController::BeginPlay()
{
	Super::BeginPlay();

	//Add event listeners when ChilliConnect API calls complete
	ChilliConnect->OnPlayerCreated().AddUObject(this, &USceneController::OnPlayerCreated);
	ChilliConnect->OnPlayerLoggedIn().AddUObject(this, &USceneController::OnPlayerLoggedIn);
	ChilliConnect->OnLeaderboardScoreAdded().AddUObject(this, &USceneController::OnLeaderboardScoreAdded);

	//Create a new ChilliConnect Player. In a real game, we would usually save the
	//ChilliConnectID and ChilliConnect Secret of the last logged in player to the device
	//and log them in automatically. To keep the sample project simple, we just create
	//a new player account every time.
	ChilliConnect->CreatePlayer();
}

void USceneController::OnPlayerCreated(FCreatePlayerResponse Response)
{
	UE_LOG(LogTemp, Display, TEXT("Created Player"));

	ChilliConnectID = Response.ChilliConnectID;
	ChilliConnectSecret = Response.ChilliConnectSecret;

	//Login the newly created ChilliConnect player account
	ChilliConnect->LoginPlayer(ChilliConnectID, ChilliConnectSecret);
}

void USceneController::OnPlayerLoggedIn(FLoginResponse Response)
{
	UE_LOG(LogTemp, Display, TEXT("Logged In Player"));

	AccessToken = Response.AccessToken;

	//Login returns an access token that is valid for 24 hours. The access token is 
	//provided on subsequent requests. Add a random leaderboard score for the 
	//HIGH_SCORES leaderboard.

	uint8 Score = rand() % 1000;

	ChilliConnect->AddLeaderboardScore(AccessToken, "HIGH_SCORES", Score);
}

void USceneController::OnLeaderboardScoreAdded(FAddLeaderboardScoreResponse Response)
{
	UE_LOG(LogTemp, Display, TEXT("Added Leaderboard Score"));
}

void USceneController::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);
	ChilliConnect->Update();
}

