using System.IO;
using System.Net;
using System.Threading.Tasks;
using FileServer2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileServer2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync([FromForm] string userName)
        {
            if (!Request.HasFormContentType) return BadRequest();

            var form = Request.Form;
            foreach (var file in form.Files)
            {
                if (file.Length <= 0) continue;
                using (var stream = new FileStream("Files\\" + /*userName + "\\" + */file.FileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok();
        }

        [HttpGet("download")]
        public IActionResult DownloadFileAsync(string userName, string fileName)
        {
            var net = new WebClient();
            var data = net.DownloadData("Files\\" + /*userName + "\\" + */fileName);
            var content = new MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            return File(content, contentType, fileName);
        }
    }
}
