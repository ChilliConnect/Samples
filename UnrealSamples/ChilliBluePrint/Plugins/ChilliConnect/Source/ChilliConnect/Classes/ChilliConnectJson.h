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
		TArray<FString> GetStringArray(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetStringArray(FString Field, TArray<FString> Values);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		float GetNumber(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetNumber(FString Field, float Float);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		TArray<float> GetNumberArray(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetNumberArray(FString Field, TArray<float> Values);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		TArray<UChilliConnectJson *> GetObjectArray(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetObjectArray(FString Field, TArray<UChilliConnectJson *> Values);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		bool GetBool(FString Field);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		FString GetJsonString();

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetBool(FString Field, bool BoolValue);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		void SetJson(FString Field, UChilliConnectJson * JsonValue);

	UFUNCTION(BlueprintCallable, Category = "ChilliConnect")
		UChilliConnectJson * GetJson(FString Field);

	TSharedPtr<FJsonObject> GetJsonObject();
	void SetJsonObject(TSharedPtr<FJsonObject> JsonObject);

private:

	TSharedPtr<FJsonObject> JsonObj;
};
