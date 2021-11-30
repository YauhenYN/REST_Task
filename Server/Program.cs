using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            try
            {
                host.Run();
            }
            catch(Exception e)
            {
                foreach(var service in host.Services.GetServices<Services.IFileBridge<Models.IEntity>>()) service.Dispose();
                Console.WriteLine(e.Message);
                Console.Read();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
            return host;
        }
    }
}
