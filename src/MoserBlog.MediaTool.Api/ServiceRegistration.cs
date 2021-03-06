using Microsoft.Extensions.Diagnostics.HealthChecks;
using MoserBlog.MediaTool.Api.Clients;
using MoserBlog.MediaTool.Api.Clients.Interfaces;
using MoserBlog.MediaTool.Api.Handler;
using MoserBlog.MediaTool.Api.Handler.Interfaces;

namespace MoserBlog.MediaTool.Api;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBlobStorageClient, BlobStorageClient>();
        services.AddScoped<ICacheHandler, CacheHandler>();

        services.AddHealthChecks()
            .AddCheck<HealthCheck>(
                "mediatool_health_check",
                failureStatus: HealthStatus.Degraded
            );

        return services;
    }
}
