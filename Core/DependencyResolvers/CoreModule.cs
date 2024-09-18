using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Loggers;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers;

public class CoreModule : ICoreModule
{
    public void Load(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheManager, RedisCacheManager>();
        services.AddDistributedMemoryCache();
        services.AddSingleton<LoggerServiceBase, MsSqlLogger>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<Stopwatch>();
    }
}
