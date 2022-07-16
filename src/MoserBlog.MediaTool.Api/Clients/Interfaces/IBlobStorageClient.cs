using MoserBlog.MediaTool.Api.Clients.Dtos;

namespace MoserBlog.MediaTool.Api.Clients.Interfaces;

public interface IBlobStorageClient
{
    Task<MediaResultDto> GetMediaResultDtoAsync(string mediaName);
}
