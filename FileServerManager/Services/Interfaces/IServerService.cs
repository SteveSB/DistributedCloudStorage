using System.Threading.Tasks;
using FileServerManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileServerManager.Services.Interfaces
{
    public interface IServerService
    {
        Task<File> GetFilePath(int id);
        Task<IActionResult> UploadFile(IFormFileCollection form);
    }
}
