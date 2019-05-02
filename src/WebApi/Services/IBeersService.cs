using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IBeersService
    {
        IEnumerable<Beer> GetAllBeers();
    }
}
