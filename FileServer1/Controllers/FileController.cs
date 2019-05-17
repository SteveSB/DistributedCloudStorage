using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                using (var stream = new FileStream("Files\\" /*+ userName + "\\"*/ + file.FileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok();
        }
    }
}
