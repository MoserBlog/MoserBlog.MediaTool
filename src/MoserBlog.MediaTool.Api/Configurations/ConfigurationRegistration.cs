namespace MoserBlog.MediaTool.Api.Configurations;

public static class ConfigurationRegistration
{
    public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        services.Configure<BlobStorageConfig>(configuration.GetSection(nameof(BlobStorageConfig)));

        return services;
    }
}
