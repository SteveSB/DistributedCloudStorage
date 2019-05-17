namespace FileServerManager.Helpers
{
    public class StaticRef
    {
        public static int Server1Port { get; } = 53737;
        public static string Server1FilesSavePath { get; } =
            "P:\\Projects\\ASP.NET Core\\DistributedCloudStorage\\FileServer1\\Files\\";

        public static int Server2Port { get; } = 53738;
        public static string Server2FilesSavePath { get; } =
            "P:\\Projects\\ASP.NET Core\\DistributedCloudStorage\\FileServer2\\Files\\";

    }
}
