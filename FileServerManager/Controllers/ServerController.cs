using Microsoft.AspNetCore.Mvc;

namespace FileServerManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
