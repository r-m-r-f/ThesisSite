using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThesisSite.Data;
using ThesisSite.Domain.Helpers;

namespace ThesisSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                CreateRoles(services).Wait();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private async static Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var role in ApplicationRoles.Roles)
            {
                var exists = await roleManager.RoleExistsAsync(role);

                if (!exists)
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }
        }

    }
}
