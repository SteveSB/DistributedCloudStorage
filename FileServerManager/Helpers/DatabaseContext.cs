using FileServerManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FileServerManager.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<File> Files { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}
