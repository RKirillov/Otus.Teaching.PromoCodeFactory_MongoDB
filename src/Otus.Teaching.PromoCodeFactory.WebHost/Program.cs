using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; } = null!;
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), true);
            Configuration = builder.Build();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}