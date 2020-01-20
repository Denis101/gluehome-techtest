using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.ClearProviders();
                });
    }
}
