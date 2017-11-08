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

TArray<FString>
UChilliConnectJson::GetStringArray(FString Field)
{
	TArray<FString> Values;
	if (JsonObj->HasField(Field)) {
		TArray < TSharedPtr < FJsonValue > > Strings = JsonObj->GetArrayField(Field);
		for (auto& StringValue : Strings) {
			Values.Add(StringValue->AsString());
		}
	}

	return Values;
}


TArray<float>
UChilliConnectJson::GetNumberArray(FString Field)
{
	TArray<float> Values;
	if (JsonObj->HasField(Field)) {
		TArray < TSharedPtr < FJsonValue > > Numbers = JsonObj->GetArrayField(Field);
		for (auto& NumberValue : Numbers) {
			Values.Add(NumberValue->AsNumber());
		}
	}

	return Values;
}

void 
UChilliConnectJson::SetString(FString Field, FString Value)
{
	JsonObj->SetStringField(Field, Value);
}

void
UChilliConnectJson::SetStringArray(FString Field, TArray<FString> Values)
{
	TArray <TSharedPtr<FJsonValue>> JsonValues;
	for (auto& Value : Values) {
		JsonValues.Add(MakeShareable(new FJsonValueString(Value)));
	}

	JsonObj->SetArrayField(Field, JsonValues);
}

void
UChilliConnectJson::SetObjectArray(FString Field, TArray<UChilliConnectJson *> Values)
{
	TArray <TSharedPtr<FJsonValue>> JsonValues;
	for (auto& Value : Values) {
		JsonValues.Add(MakeShareable( new FJsonValueObject(Value->GetJsonObject())));
	}

	JsonObj->SetArrayField(Field, JsonValues);
}

TArray<UChilliConnectJson *>
UChilliConnectJson::GetObjectArray(FString Field)
{
	TArray<UChilliConnectJson *> Values;
	if (JsonObj->HasField(Field)) {
		TArray < TSharedPtr < FJsonValue > > Objects = JsonObj->GetArrayField(Field);
		for (auto& ObjectValue : Objects) {
			UChilliConnectJson * json = NewObject<UChilliConnectJson>(this);
			json->SetJsonObject(ObjectValue->AsObject());
			Values.Add(json);
		}
	}

	return Values;
}

void
UChilliConnectJson::SetNumberArray(FString Field, TArray<float> Values)
{
	TArray <TSharedPtr<FJsonValue>> JsonValues;
	for (auto& Value : Values) {
		JsonValues.Add(MakeShareable(new FJsonValueNumber(Value)));
	}

	JsonObj->SetArrayField(Field, JsonValues);
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

void 
UChilliConnectJson::SetJson(FString Field, UChilliConnectJson * JsonValue)
{
	JsonObj->SetObjectField(Field, JsonValue->GetJsonObject());
}

UChilliConnectJson *
UChilliConnectJson::GetJson(FString Field)
{
	UChilliConnectJson * json = NewObject<UChilliConnectJson>(this);
	json->SetJsonObject(JsonObj->GetObjectField(Field));
	return json;
}

