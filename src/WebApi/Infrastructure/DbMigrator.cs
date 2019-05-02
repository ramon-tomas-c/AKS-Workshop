using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Infrastructure
{
    public static class DbMigrator
    {
        public static IWebHost MigrateDb(this IWebHost host)
        {
            host.MigrateDbContext<BeersContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<BeersContextSeed>>();

                new BeersContextSeed()
                    .SeedAsync(context, logger)
                    .Wait();
            });
            return host;
        }
    }
}
