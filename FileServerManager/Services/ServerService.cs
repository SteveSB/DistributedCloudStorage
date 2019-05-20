using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileServerManager.Helpers;
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

        public async Task<List<File>> GetAllFiles(string userName)
        {
            return await Task.Run(() => _context.Files.Where(f => f.Owner == userName).ToList());
        }

        public async Task<ServerPortResponse> ChooseServerPort(string fileName, string size, string userName)
        {
            var fileSize = int.Parse(size);
            var file = await UpdateDatabase(fileSize, fileName, userName);

            return new ServerPortResponse
            {
                ServerPort = ((file.ServerId == 1) ? StaticRef.Server1Port : StaticRef.Server2Port),
                BackupServerPort = ((file.BackupServer == 1) ? StaticRef.Server1Port : StaticRef.Server2Port)
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

        private async Task<File> UpdateDatabase(int fileSize, string fileName, string userName)
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
                Owner = userName
            };

            _context.Files.Add(file);

            await _context.SaveChangesAsync();

            return file;
        }
    }
}
