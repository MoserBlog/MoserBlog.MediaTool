using Microsoft.AspNetCore.Mvc;
using MoserBlog.MediaTool.Api.Clients.Interfaces;

namespace MoserBlog.MediaTool.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    private readonly IBlobStorageClient _blobStorageClient;

    public MediaController(IBlobStorageClient blobStorageClient)
    {
        _blobStorageClient = blobStorageClient;
    }

    [HttpGet("{mediaName}")]
    public async Task<IActionResult> Get(string mediaName)
    {
        var mediaResultDto = await _blobStorageClient.GetMediaResultDtoAsync(mediaName);

        return File(mediaResultDto.FileArray, mediaResultDto.ContentType);
    }
}
