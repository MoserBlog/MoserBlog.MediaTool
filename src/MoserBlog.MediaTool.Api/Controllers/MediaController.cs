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
        var mediaResult = await _blobStorageClient.GetMediaResultDtoAsync(mediaName);

        if (mediaResult is null)
        {
            return NotFound();
        }

        return File(mediaResult.FileArray, mediaResult.ContentType);
    }
}
