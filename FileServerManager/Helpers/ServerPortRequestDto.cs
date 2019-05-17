using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServerManager.Helpers
{
    public class ServerPortRequestDto
    {
        public string UserName { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
}
