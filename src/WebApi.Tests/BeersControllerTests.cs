using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

using Microsoft.Extensions.Configuration;

using TestStartup = WebApi.Startup;
using WebApi.Infrastructure;

namespace WebApi.Tests
{
    public class BeersController_
    {
        [Fact]
        public async Task Should_Return_A_List_Of_Beers()
        {


            var webHostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration(cb => cb.AddEnvironmentVariables())
                .UseStartup<TestStartup>();

            var server = new TestServer(webHostBuilder);
            DbMigrator.MigrateDb(server.Host);
            var response = await server.CreateRequest("api/beers").GetAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
