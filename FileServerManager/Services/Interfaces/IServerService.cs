using System.Collections.Generic;
using System.Threading.Tasks;
using FileServerManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileServerManager.Services.Interfaces
{
    public interface IServerService
    {
        Task<List<File>> GetAllFiles(string userName);
        Task<File> GetFile(int id);
        Task<int> ChooseServerPort(string fileName, string fileSize, string userName);
    }
}
