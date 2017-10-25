#include "ChilliConnectPrivatePCH.h"

class FChilliConnect : public IChilliConnect
{
    virtual void StartupModule() override
    {
        
    }

    virtual void ShutdownModule() override
    {

    }

};

IMPLEMENT_MODULE(FChilliConnect, ChilliConnect)

DEFINE_LOG_CATEGORY(LogChilliConnect);
