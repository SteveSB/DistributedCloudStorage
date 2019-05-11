namespace FileServerManager.Helpers
{
    public class StaticRef
    {
        public static int Server1Load { get; set; } = 0;
        public static string Server1FilesSavePath { get; } =
            "P:\\Projects\\ASP.NET Core\\DistributedCloudStorage\\FileServer\\Files\\Uploads1\\";

        public static int Server2Load { get; set; } = 0;
        public static string Server2FilesSavePath { get; } =
            "P:\\Projects\\ASP.NET Core\\DistributedCloudStorage\\FileServer\\Files\\Uploads2\\";

    }
}
