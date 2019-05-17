using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServerManager.Helpers
{
    public class ApiHelper
    {
        public static string UploadServer1 { get; } = "http://localhost:53737/file/upload";
        public static string UploadServer2 { get; } = "http://localhost:53738/file/upload";
    }
}
