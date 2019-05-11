using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FileServerManager.Helpers;
using FileServerManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> UploadFile(IFormFileCollection files, ICollection<string> formKeys)
        {
            if (files == null || files.Count == 0)
                return null;

            foreach (var file in files)
            {
                using (var client = new HttpClient())
                {
                    int fileSize = 0;
                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                    {
                        fileSize = (int) file.OpenReadStream().Length;
                        data = br.ReadBytes(fileSize);
                    }

                    var bytes = new ByteArrayContent(data);

                    var multiContent = new MultipartFormDataContent { { bytes, "file", file.FileName } };

                    var result = await client.PostAsync(ApiHelper.Upload, multiContent);

                    if (result == null)
                        return null;

                    UpdateDatabase(fileSize, file.FileName, formKeys.Single(k => k == "UserName"));
                }
            }
            return new JsonResult(new { message = "File created successfully!" });
        }

        private void UpdateDatabase(int fileSize, string fileName, string userName)
        {
            var server = _context.Servers.OrderBy(s => s.Size).First();
            server.Size += fileSize;

            _context.Files.Add(new File
            {
                Name = fileName,
                Size = fileSize,
                Path = StaticRef.Server1FilesSavePath,
                ServerId = server.Id,
                HasBackup = false,
                Owner = userName
            });

            _context.SaveChanges();
        }

        public async Task<File> GetFilePath(int id)
        {
            return await _context.Files.FindAsync(id);
        }
    }
}
