using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Infrastructure;
using WebApi.Models;

namespace WebApi.Services
{
    public class BeersService : IBeersService
    {
        private readonly IOptionsSnapshot<Settings> settings;
        private readonly BeersContext ctx;
        public BeersService(BeersContext ctx, IOptionsSnapshot<Settings> settings)
        {
            this.settings = settings;
            this.ctx = ctx;
        }
        public IEnumerable<Beer> GetAllBeers()
        {
            var url = settings.Value.UseBlob ? settings.Value.BlobStorageUrl : "images/";
            return ctx.Beers.AsQueryable().Select(s =>
            new Beer()
            {
                Id = s.Id,
                Code = s.Code,
                Country = s.Country,
                Name = s.Name,
                ImageUrl = $"{url}{s.Code}.jpg"
            }).ToList();
        }
    }
}
