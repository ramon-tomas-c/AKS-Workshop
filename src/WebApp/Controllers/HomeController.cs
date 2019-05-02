using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<Settings> settings;

        public HomeController(IOptionsSnapshot<Settings> settings)
        {
            this.settings = settings;
        }

        public async Task<IActionResult> Index()
        {
            var result = new List<BeerModel>();
            var retry = Policy.Handle<HttpRequestException>()
                         .WaitAndRetryAsync(new TimeSpan[]
                         {
                             TimeSpan.FromSeconds(5),
                             TimeSpan.FromSeconds(8)
                         });

            using (var api = new HttpClient())
            {
                await retry.ExecuteAsync(async () =>
                {
                    var response = await api.GetAsync($"{settings.Value.ApiUrl}/api/beers");
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<BeerModel>>(data);
                });                
            }
                        
            return View(result);
        }       

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
