using System;

namespace Embique.DocumentManagementApi.Extension;

public static class LoggingExtension
{
    public static void GetInfoLoggingGoing<T>(this ILogger<T> logger,string message, params object?[] args){
        logger.LogInformation($"[{typeof(T)}] : {message} ", args);
    }
    public static void GetExceptionLoggingGoing<T>(this ILogger<T> logger,string message, params object?[] args){
        logger.LogError($"[{typeof(T)}] : {message} ", args);
    }
}
