using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Infrastructure;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BeersController : Controller
    {

        private readonly IBeersService _beersSvc;
        public BeersController(IBeersService svc) => _beersSvc = svc;

        // GET api/beers
        [HttpGet]
        public IEnumerable<Beer> Get() => _beersSvc.GetAllBeers();
    }
}
