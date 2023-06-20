using NuGet.Protocol.Plugins;
using ServicesLibrary.LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.ServicesLocator;

//This is a testing feature that i have not implemented yet so i will leave it here for now
public static class ServiceLocator
{
    private static ILogService logService;


    public static void RegisteredLogingService(ILogService logService)
    {
        logService = logService;
    }

    public static ILogService GetLoggingService()
    {
        if (logService == null)
        {
            throw new InvalidOperationException("Logging service has not been registered.");
        }

        return logService;
    }
}
