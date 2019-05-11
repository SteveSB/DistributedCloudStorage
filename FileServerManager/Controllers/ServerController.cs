using System.Threading.Tasks;
using FileServerManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileServerManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IServerService _serverService;

        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadTask()
        {
            var form = Request.Form;
            var result = await _serverService.UploadFile(form.Files);

            if (result == null)
                BadRequest(new { message = "Error, Make sure you've uploaded a valid file!" });

            return Ok(result);
        }

        [HttpPost("download")]
        public async Task<IActionResult> DownloadTask([FromBody] int fileId)
        {
            var file = await _serverService.GetFilePath(fileId);
            return Ok(file.Path);
        }
    }
}
