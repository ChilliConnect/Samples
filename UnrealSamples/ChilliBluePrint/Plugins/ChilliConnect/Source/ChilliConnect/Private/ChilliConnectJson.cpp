#include "ChilliConnectPrivatePCH.h"
#include "ChilliConnectJson.h"

UChilliConnectJson::UChilliConnectJson(const class FObjectInitializer& PCIP)
	: Super(PCIP)
{
	JsonObj = MakeShareable(new FJsonObject());
}

FString 
UChilliConnectJson::GetString(FString Field)
{
	return JsonObj->GetStringField(Field);
}

void 
UChilliConnectJson::SetString(FString Field, FString Value)
{
	JsonObj->SetStringField(Field, Value);
}

float 
UChilliConnectJson::GetNumber(FString Field)
{
	return JsonObj->GetNumberField (Field);
}

void 
UChilliConnectJson::SetNumber(FString Field, float Float)
{
	JsonObj->SetNumberField(Field, Float);
}

bool 
UChilliConnectJson::GetBool(FString Field)
{
	return JsonObj->GetBoolField(Field);
}

void 
UChilliConnectJson::SetBool(FString Field, bool BoolValue)
{
	JsonObj->SetBoolField(Field, BoolValue);
}

TSharedPtr<FJsonObject> 
UChilliConnectJson::GetJsonObject()
{
	return JsonObj;
}

FString 
UChilliConnectJson::GetJsonString()
{
	FString JsonString;
	TSharedRef<TJsonWriter<>> writer = TJsonWriterFactory<>::Create(&JsonString);
	FJsonSerializer::Serialize(JsonObj.ToSharedRef(), writer);

	return JsonString;
}

void 
UChilliConnectJson::SetJsonObject(TSharedPtr<FJsonObject> JsonObject)
{
	JsonObj = JsonObject;
}