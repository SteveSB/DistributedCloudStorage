using System.Collections.Generic;
using FileServerManager.Models;

namespace FileServerManager.Helpers.Response
{
    public class FileFolderResponse
    {
        public List<File> Files { get; set; }
        public List<Folder> Folders { get; set; }
    }
}
