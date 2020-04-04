using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {


            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@"file.log", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
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
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
            
        public static IWebHost BuildWebHostWithCustomURL(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(args[0])
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
