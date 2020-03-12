using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MvcCookieSample.Data;

namespace MvcCookieSample
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //CreateWebHost(args).Run();
            //CreateWebHost(args).MigrateDbContext<ApplicationDbContext>((context, services) =>
            //    { new ApplicationDbContextSeed().SeedAsync(context, services).Wait(); })
            //    .Run();
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}
