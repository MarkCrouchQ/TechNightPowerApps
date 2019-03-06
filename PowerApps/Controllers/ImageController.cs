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
    public class ImageController : ControllerWithDataContext
    {
        public ImageController(DataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Returns an Agent or Product image.
        /// </summary>
        // GET: api/Image/5
        [HttpGet("{type}/{id}", Name = "GetImage")]
        public async Task<ActionResult> Get(string type, int id)
        {
            byte[] data = null;
            switch (type.ToLowerInvariant())
            {
                case "agent":
                    var agent = await _data.Agents
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();
                    if (agent != null)
                        data = agent.Picure;
                    break;
                case "product":
                    var product = await _data.Products
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();
                    if (product != null)
                        data = product.Picture;
                    break;
                default:
                    return NotFound();
            }

            if (data == null)
                return NotFound();

            //return $"data:image/png;base64,{Convert.ToBase64String(data)}";
            return File(data, "image/png");
        }
        
    }
}
