using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PowerApps.Extension;
using PowerApps.Models;

namespace PowerApps.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerWithDataContext
    {
        public OrdersController(DataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Returns all open orders  with joined children included.
        /// </summary>

        [HttpGet(Name = "GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await Get(string.Empty);
        }

        /// <summary>
        /// Returns all orders for a user with joined children included.
        /// </summary>
        [HttpGet("{email}", Name = "GetOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> Get(string email)
        {
            IQueryable<Order> ordersQuery;
            if (string.IsNullOrEmpty(email))
                ordersQuery = _data.Orders;
            else
                ordersQuery = _data.Orders
                    .Where(x => x.Agent.Email.ToLowerInvariant() == email.ToLowerInvariant());

            var orders = await ordersQuery
                .Where(x=>!x.HasShipped)
                .Include(x => x.Agent)
                .Include(x => x.Customer)
                .Include(x => x.ProductOrders)
                .ThenInclude(x => x.Product)
                .ToListAsync();

            foreach (var order in orders)
            {
                if (order.Agent != null)
                    order.Agent.PictureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/image/agent/{order.Agent.Id}";
                foreach (var productOrder in order.ProductOrders)
                {
                    productOrder.Product.PictureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/image/product/{productOrder.Product.Id}";
                }
            }

            return orders;
        }

        /// <summary>
        /// Assigns agents a new randomized order.
        /// </summary>
        [HttpGet("{type}/{id}", Name = "CreateOrders")]
        public async Task<ActionResult<IEnumerable<string>>> Get(string type, int id)
        {
            var agents = await _data.Agents.ToListAsync();
            var customers = await _data.Customers.ToListAsync();
            var products = await _data.Products.ToListAsync();
            
            foreach (var agent in agents)
            {
                var order = new Order()
                {
                    Agent = agent,
                    Customer = customers.GetRandomItem(),
                    HasShipped = false
                };
                await _data.AddAsync(order);
                await _data.SaveChangesAsync();
                foreach (var i in ListExtensions.RandomLoop(10))
                {
                    var productOrder = new ProductOrder()
                    {
                        IsPackaged = false,
                        Order = order,
                        Product = products.GetRandomItem(),
                        Quantity = ListExtensions.Random.Next(1, 99)
                    };
                    await _data.AddAsync(productOrder);
                }
                await _data.SaveChangesAsync();
            }

            return agents.Select(x => x.Email).ToList();
        }
    }
}