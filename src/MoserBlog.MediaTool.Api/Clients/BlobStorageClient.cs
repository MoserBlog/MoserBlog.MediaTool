using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using MoserBlog.MediaTool.Api.Clients.Dtos;
using MoserBlog.MediaTool.Api.Clients.Interfaces;
using MoserBlog.MediaTool.Api.Configurations;

namespace MoserBlog.MediaTool.Api.Clients;

public class BlobStorageClient : IBlobStorageClient
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobStorageClient(
        IConfiguration configuration,
        IOptions<BlobStorageConfig> blobStorageConfig)
    {
        var blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("BlobConnection"));
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageConfig.Value.ContainerName);
    }


    public async Task<MediaResultDto> GetMediaResultDtoAsync(string mediaName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(mediaName);

        using MemoryStream memoryStream = new();

        try
        {
            await blobClient.DownloadToAsync(memoryStream);

            return new MediaResultDto(memoryStream.ToArray(), "image/png");
        }
        catch (Exception)
        {
            return new MediaResultDto();
        }
    }
}
