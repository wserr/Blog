using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host;
            if(args.Length>0)
            {
                host = BuildWebHostWithCustomURL(args);
            }
            else
            {
                host = BuildWebHost(args);
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
            
        public static IWebHost BuildWebHostWithCustomURL(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(args[0])
                .UseStartup<Startup>()
                .Build();
    }
}
