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

USTRUCT(BlueprintType)
struct FGetCurrencyBalanceRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	TArray<FString> Keys;
};

USTRUCT(BlueprintType)
struct FGetCurrencyBalanceResponseValue
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Balance;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString WriteLock;
};

USTRUCT(BlueprintType)
struct FGetCurrencyBalanceResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FGetCurrencyBalanceResponseValue> Values;
};

USTRUCT(BlueprintType)
struct FSetCurrencyBalanceRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Amount;
};

USTRUCT(BlueprintType)
struct FSetCurrencyBalanceResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Balance;
};


USTRUCT(BlueprintType)
struct FGetInventoryResponseItem
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString ItemID;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString WriteLock;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	UChilliConnectJson * InstanceData;
};


USTRUCT(BlueprintType)
struct FGetInventoryResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	TArray<FGetInventoryResponseItem> Items;
};

USTRUCT(BlueprintType)
struct FAddInventoryItemRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	UChilliConnectJson * InstanceData;
};

USTRUCT(BlueprintType)
struct FAddInventoryItemResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString ItemID;
};

USTRUCT(BlueprintType)
struct FRemoveInventoryItemRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString ItemID;
};

USTRUCT(BlueprintType)
struct FRedeemGoogleIapRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString PurchaseData;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString PurchaseDataSignature;
};

USTRUCT(BlueprintType)
struct FRedeemAppleIapRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Receipt;

};


USTRUCT(BlueprintType)
struct FRedeemIapResponseCurrencyReward
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Amount;

};

USTRUCT(BlueprintType)
struct FRedeemIapResponseInventoryItemReward
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Amount;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> ItemIDs;

};

USTRUCT(BlueprintType)
struct FRedeemIapResponseRewards
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FRedeemIapResponseCurrencyReward> Currencies;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FRedeemIapResponseInventoryItemReward> Items;

};

USTRUCT(BlueprintType)
struct FRedeemIapResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		bool Redeemed;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Status;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FRedeemIapResponseRewards Rewards;
};

USTRUCT(BlueprintType)
struct FGetMetadataDefinitionsRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	TArray<FString> Tags;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	int Page = 1;
};

USTRUCT(BlueprintType)
struct FGetMetadataDefinitionsResponseItem
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> Tags;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		UChilliConnectJson * CustomData;
};

USTRUCT(BlueprintType)
struct FGetMetadataDefinitionsResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString EconomyVersion;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FGetMetadataDefinitionsResponseItem> Items;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Total;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Page;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int PageSize;
};


USTRUCT(BlueprintType)
struct FRegisterPushTokenRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Service;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString DeviceToken;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		bool Overwrite = false;
};

USTRUCT(BlueprintType)
struct FGetDlcUsingTagsRequest
{
	GENERATED_USTRUCT_BODY()
	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> Tags;
};

USTRUCT(BlueprintType)
struct FGetDlcUsingTagsFileResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Location;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Size;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Checksum;
};

USTRUCT(BlueprintType)
struct FGetDlcUsingTagsPackageResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Type;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Checksum;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString DateUploaded;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Url;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	int Size;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	TArray<FGetDlcUsingTagsFileResponse> Files;
};

USTRUCT(BlueprintType)
struct FGetDlcUsingTagsResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FGetDlcUsingTagsPackageResponse> Packages;
};


USTRUCT(BlueprintType)
struct FGetVirtualPurchaseDefinitionsRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> Tags;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Page = 1;
};

USTRUCT(BlueprintType)
struct FCurrencyDefinition
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> Tags;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		UChilliConnectJson * CustomData;
	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Initial;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Max;
};

USTRUCT(BlueprintType)
struct FInventoryItemDefinition
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> Tags;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		UChilliConnectJson * CustomData;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int InitialAllocation;
};

USTRUCT(BlueprintType)
struct FGetVirtualPurchaseDefinitionsResponseCurrencyExchange
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FCurrencyDefinition Item;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Amount;

};

USTRUCT(BlueprintType)
struct FGetVirtualPurchaseDefinitionsResponseItemExchange
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FInventoryItemDefinition Item;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Amount;
};


USTRUCT(BlueprintType)
struct FGetVirtualPurchaseDefinitionExchange
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FGetVirtualPurchaseDefinitionsResponseCurrencyExchange> Currencies;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FGetVirtualPurchaseDefinitionsResponseItemExchange> Items;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		UChilliConnectJson * CustomData;
};


USTRUCT(BlueprintType)
struct FGetVirtualPurchaseDefinitionsResponseItem
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> Tags;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FGetVirtualPurchaseDefinitionExchange Costs;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FGetVirtualPurchaseDefinitionExchange Rewards;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		UChilliConnectJson * CustomData;
};

USTRUCT(BlueprintType)
struct FGetVirtualPurchaseDefinitionsResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString EconomyVersion;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FGetVirtualPurchaseDefinitionsResponseItem> Items;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Total;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Page;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int PageSize;
};

USTRUCT(BlueprintType)
struct FMakeVirtualPurchaseRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> ItemIDs;
};

USTRUCT(BlueprintType)
struct FMakeVirtualPurchaseCurrencyExchange
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Amount;
};

USTRUCT(BlueprintType)
struct FMakeVirtualPurchaseItemExchange
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Name;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		int Amount;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
		TArray<FString> ItemIDs;
};

USTRUCT(BlueprintType)
struct FMakeVirtualPurchaseExchange
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	TArray<FMakeVirtualPurchaseCurrencyExchange> Currencies;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	TArray<FMakeVirtualPurchaseItemExchange> Items;
};

USTRUCT(BlueprintType)
struct FMakeVirtualPurchaseResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FMakeVirtualPurchaseExchange Costs;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FMakeVirtualPurchaseExchange Rewards;
};

USTRUCT(BlueprintType)
struct FAddCollectionObjectRequest
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString Key;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	UChilliConnectJson * Value;
};

USTRUCT(BlueprintType)
struct FAddCollectionObjectResponse
{
	GENERATED_USTRUCT_BODY()

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "ChilliConnect")
	FString ObjectID;
};