using MoserBlog.MediaTool.Api.Clients;
using MoserBlog.MediaTool.Api.Clients.Interfaces;

namespace MoserBlog.MediaTool.Api;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBlobStorageClient, BlobStorageClient>();

        return services;
    }
}
