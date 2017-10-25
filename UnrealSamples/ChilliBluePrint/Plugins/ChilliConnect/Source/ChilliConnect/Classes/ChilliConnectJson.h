#pragma once

#include "ChilliConnectJson.generated.h"

UCLASS(Blueprintable, BlueprintType)
class UChilliConnectJson : public UObject
{
	GENERATED_UCLASS_BODY()

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		FString GetString(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetString(FString Field, FString Value);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		float GetNumber(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetNumber(FString Field, float Float);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		bool GetBool(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		FString GetJsonString();

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetBool(FString Field, bool BoolValue);

	TSharedPtr<FJsonObject> GetJsonObject();
	void SetJsonObject(TSharedPtr<FJsonObject> JsonObject);

private:

	TSharedPtr<FJsonObject> JsonObj;
};
