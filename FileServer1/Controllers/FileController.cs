using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FileServer1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileServer1.Controllers
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
        public async Task<IActionResult> UploadFileAsync()
        {
            if (!Request.HasFormContentType) return BadRequest();

            var form = Request.Form;
            foreach (var file in form.Files)
            {
                if (file.Length <= 0) continue;
                using (var stream = new FileStream("Files\\" + file.FileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok();
        }

        [HttpGet("download")]
        public IActionResult DownloadFileAsync(string fileName)
        {
            try
            {
                var net = new WebClient();
                var data = net.DownloadData("Files\\" + fileName);
                var content = new MemoryStream(data);
                var contentType = "APPLICATION/octet-stream";
                return File(content, contentType, fileName);
            }
            catch (Exception)
            {
                return new JsonResult(new { message = "File Not Found!" });
            }
        }
    }
}
