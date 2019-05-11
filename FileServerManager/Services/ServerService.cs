using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FileServerManager.Helpers;
using FileServerManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using File = FileServerManager.Models.File;

namespace FileServerManager.Services
{
    public class ServerService : IServerService
    {
        private readonly DataContext _context;

        public ServerService(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> UploadFile(IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
                return null;

            foreach (var file in files)
            {
                using (var client = new HttpClient())
                {
                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                        data = br.ReadBytes((int)file.OpenReadStream().Length);

                    var bytes = new ByteArrayContent(data);

                    var multiContent = new MultipartFormDataContent { { bytes, "file", file.FileName } };

                    var result = await client.PostAsync(ApiHelper.Upload, multiContent);

                    if (result == null)
                        return null;
                }
            }
            return new JsonResult(new { message = "File created successfully!" });
        }

        public async Task<File> GetFilePath(int id)
        {
            return await _context.Files.FindAsync(id);
        }
    }
}
