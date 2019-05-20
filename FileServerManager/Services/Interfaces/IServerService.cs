using System.Collections.Generic;
using System.Threading.Tasks;
using FileServerManager.Helpers;
using FileServerManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileServerManager.Services.Interfaces
{
    public interface IServerService
    {
        Task<List<File>> GetAllFiles(string userName);
        Task<ServerPortResponse> GetFile(int id);
        Task<ServerPortResponse> ChooseServerPort(string fileName, string fileSize, string userName);
    }
}
