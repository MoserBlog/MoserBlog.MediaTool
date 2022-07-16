using Microsoft.AspNetCore.Mvc;
using MoserBlog.MediaTool.Api.Clients.Interfaces;

namespace MoserBlog.MediaTool.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Get-Test succeeded");
    }
}
