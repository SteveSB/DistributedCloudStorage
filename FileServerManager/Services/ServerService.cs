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

        public async Task<int> ChooseServerPort(string fileName, string size, string userName)
        {
            var fileSize = int.Parse(size);
            var server = await UpdateDatabase(fileSize, fileName, userName);

            if (server == null)
                return 0;

            return server.Id == 1 ? StaticRef.Server1Port : StaticRef.Server2Port;
        }

        public async Task<File> GetFile(int id)
        {
            return await _context.Files.FindAsync(id);
        }

        private async Task<Server> UpdateDatabase(int fileSize, string fileName, string userName)
        {
            if (_context.Files.SingleOrDefault(f => f.Name.Contains(fileName)) != null)
                return null;

            var server = _context.Servers.OrderBy(s => s.Size).First();
            server.Size += fileSize;

            var basePath = server.Id == 1 ? StaticRef.Server1FilesSavePath : StaticRef.Server2FilesSavePath;

            _context.Files.Add(new File
            {
                Name = fileName,
                Size = fileSize,
                Path = basePath + userName + "\\" + fileName,
                ServerId = server.Id,
                HasBackup = false,
                Owner = userName
            });

            await _context.SaveChangesAsync();

            return server;
        }
    }
}
