using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using WebApi.Extensions;
using WebApi.Infrastructure;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDb()
                .Run();
        }


        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseBeatPulse(options =>
                {
                    options.EnableDetailedOutput();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();
                    bool.TryParse(builtConfig["UseKeyVault"], out bool useKv);
                    if (useKv)
                    {
                        config.AddAzureKeyVault(
                            $"https://{builtConfig["Vault:Name"]}.vault.azure.net/",
                            builtConfig["Vault:ClientId"],
                            builtConfig["Vault:ClientSecret"]);
                    }
                })
            .UseStartup<Startup>()
            .Build();
        }
    }
}
