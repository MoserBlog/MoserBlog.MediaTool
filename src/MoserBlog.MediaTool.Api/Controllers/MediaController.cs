using Microsoft.AspNetCore.Mvc;

namespace MoserBlog.MediaTool.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok();
    }
}
