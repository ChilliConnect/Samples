// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Components/SceneComponent.h"
#include "ChilliConnect.h"
#include "SceneController.generated.h"

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class CHILLICONNECTEXAMPLE_API USceneController : public USceneComponent
{
	GENERATED_BODY()

public:	
	USceneController();

private:
	FChilliConnect * ChilliConnect;

	FString ChilliConnectID;
	FString ChilliConnectSecret;
	FString AccessToken;

	void OnPlayerCreated(FCreatePlayerResponse Response);
	void OnPlayerLoggedIn(FLoginResponse Response);
	void OnLeaderboardScoreAdded(FAddLeaderboardScoreResponse Response);

protected:
	virtual void BeginPlay() override;

public:	
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;		
	
};
