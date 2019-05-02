using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Infrastructure
{
    public class BeersContextSeed
    {
        public async Task SeedAsync(BeersContext context, ILogger<BeersContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(BeersContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Beers.Any())
                {
                    await context.Beers.AddRangeAsync(GetDefaultBeers());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<Beer> GetDefaultBeers()
        {
            return new[]
            {
                new Beer()
                {
                    Name = "Guinness",
                    Country = "Ireland",
                    Code = "guinness"
                },
                new Beer()
                {
                    Name = "Blue Moon",
                    Country = "Belgium",
                    Code = "blue_moon"
                },
                new Beer()
                {
                    Name = "Dos Equis",
                    Country = "Mexico",
                    Code = "dos_equis"
                },
                new Beer()
                {
                    Name = "Fat Tire",
                    Country = "Belgium",
                    Code = "fat_tire"
                },
                new Beer()
                {
                    Name = "Heady Topper",
                    Country = "US",
                    Code = "heady_topper"
                },
                new Beer()
                {
                    Name = "Dogfish Head",
                    Country = "US",
                    Code = "dogfish_head"
                },
                new Beer()
                {
                    Name = "Goose Island",
                    Country = "US",
                    Code = "goose_island"
                },
                new Beer()
                {
                    Name = "Mikkeller",
                    Country = "Denmark",
                    Code = "mikkeller"
                },
                new Beer()
                {
                    Name = "Foster's Lager",
                    Country = "Australia",
                    Code = "foster's_lager"
                },
                new Beer()
                {
                    Name = "Impaled",
                    Country = "Spain",
                    Code = "impaled"
                },
                new Beer()
                {
                    Name = "Devil's IPA",
                    Country = "Spain",
                    Code = "devils"
                },
                new Beer()
                {
                    Name = "Ibuprofano",
                    Country = "Spain",
                    Code = "ibuprofano",
                },
                new Beer()
                {
                    Name = "Malavida",
                    Country = "Spain",
                    Code = "malavida"
                }
            };
        }       

        private Policy CreatePolicy(ILogger<BeersContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }    
    }
}
