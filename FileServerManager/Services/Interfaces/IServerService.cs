using System.Collections.Generic;
using System.Threading.Tasks;
using FileServerManager.Helpers;
using FileServerManager.Helpers.Response;
using FileServerManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileServerManager.Services.Interfaces
{
    public interface IServerService
    {
        Task<FileFolderResponse> GetAllFiles(int folderId, string userName);
        Task<ServerPortResponse> GetFile(int id);
        Task<ServerPortResponse> ChooseServerPort(string fileName, string fileSize, int folderId, string userName);
        Task<CreateFolderResponse> CreateFolder(int? folderId, string folderName, string userName);
    }
}
