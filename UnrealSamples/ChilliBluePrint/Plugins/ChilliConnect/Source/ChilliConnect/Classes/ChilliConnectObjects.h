#pragma once

#include "ChilliConnectJson.h"
#include "ChilliConnectObjects.generated.h"

USTRUCT(BlueprintType)
struct FChilliConnectErrorResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	uint8 ErrorCode;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	uint8 HttpCode;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString ErrorMessage;
};

USTRUCT(BlueprintType)
struct FCreatePlayerRequest
{
	GENERATED_USTRUCT_BODY()

};

USTRUCT(BlueprintType)
struct FCreatePlayerResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString ChilliConnectID;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString ChilliConnectSecret;
};

USTRUCT(BlueprintType)
struct FLoginUsingChilliConnectRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString ChilliConnectID;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString ChilliConnectSecret;
};

USTRUCT(BlueprintType)
struct FLoginUsingChilliConnectResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString ConnectAccessToken;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString MetricsAccessToken;
};

USTRUCT(BlueprintType)
struct FSetPlayerDataRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		UChilliConnectJson * Value;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString WriteLock;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Attachment;
};

USTRUCT(BlueprintType)
struct FSetPlayerDataResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString WriteLock;
};

USTRUCT(BlueprintType)
struct FGetPlayerDataRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	TArray<FString> Keys;

};

USTRUCT(BlueprintType)
struct FGetPlayerDataResponseValue
{
	GENERATED_USTRUCT_BODY()

		UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		UChilliConnectJson * Value;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		bool HasAttachment;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString WriteLock;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString DateCreated;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString DateModified;
};

USTRUCT(BlueprintType)
struct FGetPlayerDataResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FGetPlayerDataResponseValue> Values;
};