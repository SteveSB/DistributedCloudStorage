using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileServer.Helpers;
using FileServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileServer.Controllers
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

            var filesList = new List<string>();

            var form = Request.Form;
            foreach (var file in form.Files)
            {
                if (file.Length <= 0) continue;
                using (var stream = new FileStream(StaticRef.FilesSavePath + file.FileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    filesList.Add(StaticRef.FilesSavePath + file.FileName);
                }
            }
            return Ok(filesList);
        }
    }
}
