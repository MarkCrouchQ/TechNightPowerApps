using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerApps.Models;

namespace PowerApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerWithDataContext
    {
        public ProductController(DataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Randomizes product locations
        /// </summary>
        // GET: api/Image/5
        [HttpGet(Name = "RandomizeProductLocations")]
        public async Task<ActionResult> Get(string action)
        {
            var limitLatitude = .00015f;
            var limitLongitude = .0003f;
            var random = new Random();

            foreach (var product in await _data.Products.ToListAsync())
            {
                product.Latitude = (1f - (float)random.NextDouble() * 2f) * limitLatitude;
                product.Longitude = (1f - (float)random.NextDouble() * 2f) * limitLongitude;
            }
            await _data.SaveChangesAsync();

            return Ok();
        }
        
    }
}
