using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        private readonly Settings _settings;
        public ConfigController(IOptionsSnapshot<Settings> config) => _settings = config.Value;

        [HttpGet()]
        public IActionResult GetConfig() => Ok(_settings);
    }
}
