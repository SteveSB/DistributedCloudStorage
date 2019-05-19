using System.Threading.Tasks;
using FileServerManager.Helpers;
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

        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllFilesTask([FromBody] string userName)
        {
            var result = await _serverService.GetAllFiles(userName);

            if (result == null)
                return BadRequest(new { message = "Error downloading file!" });

            return Ok(result);
        }

        [HttpPost("getServerPort")]
        public async Task<IActionResult> ServerPortTask([FromBody] ServerPortRequestDto serverPortRequest)
        {
            var result = await _serverService.ChooseServerPort(serverPortRequest.FileName, serverPortRequest.FileSize, serverPortRequest.UserName);

            if (result == null)
                return BadRequest(new { message = "Error downloading file!" });

            return Ok(result);
        }

        [HttpPost("download")]
        public async Task<IActionResult> DownloadTask([FromBody] int fileId)
        {
            var result = await _serverService.GetFile(fileId);

            if (result == null)
                return BadRequest(new { message = "Error downloading file!" });

            return Ok(result);
        }
    }
}
