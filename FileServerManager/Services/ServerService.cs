using System.Linq;
using System.Threading.Tasks;
using FileServerManager.Helpers;
using FileServerManager.Helpers.Response;
using FileServerManager.Models;
using FileServerManager.Services.Interfaces;

namespace FileServerManager.Services
{
    public class ServerService : IServerService
    {
        private readonly DataContext _context;

        public ServerService(DataContext context)
        {
            _context = context;
        }

        public async Task<FileFolderResponse> GetAllFiles(int folderId, string userName)
        {
            //var files = await Task.Run(() => 
            //    _context.Files
            //        .Join(_context.Folders, 
            //            file => file.Folder.Id, 
            //            folder => folder.Id, 
            //            (file, folder) => new FileFolderResponse
            //            {
            //                Files = file, Folders = folder
            //            })
            //        .Where(x => x.Files.Owner == userName && 
            //                    x.Folders.Owner == userName && 
            //                    x.Files.Folder.Id == folderId && 
            //                    x.Folders.folder.Id == folderId).ToList());
            //return files;
            var files = await Task.Run(() =>
                _context.Files.Where(f => f.Owner == userName && (folderId == 0) ? f.FolderId == null : f.FolderId == folderId).ToList());

            var folders = await Task.Run(() =>
                _context.Folders.Where(f => f.Owner == userName && (folderId == 0) ? f.folderId == null : f.folderId == folderId).ToList());

            return new FileFolderResponse
            {
                Files = files,
                Folders = folders
            };
        }

        public async Task<ServerPortResponse> ChooseServerPort(string fileName, string size, int folderId, string userName)
        {
            var fileSize = int.Parse(size);
            var file = await UpdateDatabase(fileSize, folderId, fileName, userName);

            return new ServerPortResponse
            {
                ServerPort = ((file.ServerId == 1) ? StaticRef.Server1Port : StaticRef.Server2Port),
                BackupServerPort = ((file.BackupServer == 1) ? StaticRef.Server1Port : StaticRef.Server2Port)
            };
        }

        public async Task<CreateFolderResponse> CreateFolder(int? folderId, string folderName, string userName)
        {
            var folder = new Folder
            {
                Name = folderName,
                Owner = userName,
                folderId = (folderId == 0) ? null : folderId
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            return new CreateFolderResponse
            {
                FolderId = folder.Id
            };
        }

        public async Task<ServerPortResponse> GetFile(int id)
        {
            var file = await _context.Files.FindAsync(id);
            return new ServerPortResponse
            {
                ServerPort = ((file.ServerId == 1) ? StaticRef.Server1Port : StaticRef.Server2Port),
                BackupServerPort = ((file.BackupServer == 1) ? StaticRef.Server1Port : StaticRef.Server2Port)
            };
        }

        private async Task<File> UpdateDatabase(int fileSize, int? folderId, string fileName, string userName)
        {
            if (_context.Files.SingleOrDefault(f => f.Name.Contains(fileName)) != null)
                return null;

            var server = _context.Servers.OrderBy(s => s.Size).First();
            server.Size += fileSize;

            var basePath = (server.Id == 1) ? StaticRef.Server1FilesSavePath : StaticRef.Server2FilesSavePath;
            var backupPath = (server.Id == 2) ? StaticRef.Server1FilesSavePath : StaticRef.Server2FilesSavePath;

            var file = new File
            {
                Name = fileName,
                Size = fileSize,
                Path = basePath + /*userName + "\\" + */fileName,
                ServerId = server.Id,
                BackupServer = (server.Id % 2) + 1,
                BackupPath = backupPath + /*userName + "\\" + */fileName,
                Owner = userName,
                FolderId = (folderId == 0) ? null : folderId
            };

            _context.Files.Add(file);

            await _context.SaveChangesAsync();

            return file;
        }
    }
}
