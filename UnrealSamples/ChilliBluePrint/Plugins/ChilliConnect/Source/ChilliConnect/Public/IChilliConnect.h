#pragma once

#include "ModuleManager.h"

class IChilliConnect : public IModuleInterface
{
public:
    
    static inline IChilliConnect& Get()
    {
        return FModuleManager::LoadModuleChecked< IChilliConnect >("ChilliConnect");
    }

    static inline bool IsAvailable()
    {
        return FModuleManager::Get().IsModuleLoaded("ChilliConnect");
    }

    inline bool IsConnectLoggedIn()
    {
        return ConnectAccessToken.Len() > 0;
    }
    
    inline FString GetConnectAccessToken()
    {
        return ConnectAccessToken;
    }
    
    inline void SetConnectAccessToken(FString NewConnectAccessToken)
    {
        ConnectAccessToken = NewConnectAccessToken;
    }

    inline bool IsMetricsLoggedIn()
    {
        return MetricsAccessToken.Len() > 0;
    }
    
    inline FString GetMetricsAccessToken()
    {
        return MetricsAccessToken;
    }
    
    inline void SetMetricsAccessToken(FString NewMetricsAccessToken)
    {
        MetricsAccessToken = NewMetricsAccessToken;
    }

    inline FString GetGameToken()
    {
        return GameToken;
    }
    
    inline void SetGameToken(FString NewGameToken)
    {
        GameToken = NewGameToken;
    }

private:
    
    FString ConnectAccessToken;
    FString MetricsAccessToken;
    FString GameToken;
   
};
