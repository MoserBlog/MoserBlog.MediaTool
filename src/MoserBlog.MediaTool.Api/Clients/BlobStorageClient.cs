using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using MoserBlog.MediaTool.Api.Clients.Dtos;
using MoserBlog.MediaTool.Api.Clients.Interfaces;
using MoserBlog.MediaTool.Api.Configurations;
using MoserBlog.MediaTool.Api.Handler.Interfaces;

namespace MoserBlog.MediaTool.Api.Clients;

public class BlobStorageClient : IBlobStorageClient
{
    private readonly BlobContainerClient _blobContainerClient;
    private readonly ICacheHandler _cacheHandler;

    public BlobStorageClient(
        IConfiguration configuration,
        IOptions<BlobStorageConfig> blobStorageConfig,
        ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;

        var blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("BlobConnection"));
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageConfig.Value.ContainerName);
    }


    public async Task<MediaResultDto> GetMediaResultDtoAsync(string mediaName)
    {
        var blobClient = _cacheHandler.TryGetCacheEntry(
            () => _blobContainerClient.GetBlobClient(mediaName),
            cacheDurationInMinutes: 720,
            mediaName);

        using MemoryStream memoryStream = new();

        try
        {
            await blobClient.DownloadToAsync(memoryStream);

            return new MediaResultDto(memoryStream.ToArray(), "image/png");
        }
        catch (Exception)
        {
            return new();
        }
    }
}
