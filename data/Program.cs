using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PowerApps.Extension;
using PowerApps.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace data
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                 .AddJsonFile("appsettings.json", optional: true);
            var configuration = builder.Build();
            var data = new DataContext(configuration);


            data.ProductOrders.RemoveRange(data.ProductOrders);
            data.SaveChanges();
            data.Products.RemoveRange(data.Products);
            data.SaveChanges();
            data.Orders.RemoveRange(data.Orders);
            data.SaveChanges();
            data.Customers.RemoveRange(data.Customers);
            data.SaveChanges();


            var random = new Random();
            var firstNames = new List<string>() { "Bob", "Mark", "Mike", "Ryan", "James", "Jamie", "Sally", "Jennifer", "Ann", "Jo", "Laura", "Anne", "Frank", "Bill", "Ryan", "Rachel", "Kala", "Steven" };
            var lastNames = new List<string>() { "Jones", "Jackson", "Smith", "Kennedy", "Gates", "Buckler", "Stites", "Reeves", "Harrod", "Fincher", "Tucker", "Crouch", "Walker", "Ballard", "Morrison" };
            var cities = new List<string>() { "Arlington", "Hurst", "Kansas City", "Wataga", "Springfield", "Franklin", "Lebanon", "Greenville", "Bristol", "Fairview", "Salem", "Madison", "Georgetown", "Ashland", "Dover" };

            for (var i = 0; i < 99; i++)
            {
                var customer = new Customer()
                {
                    Name = $"{firstNames.GetRandomItem()} {lastNames.GetRandomItem()}",
                    Address = $"{ListExtensions.Random.Next(0, 99999).ToString()} ",
                    City = cities.GetRandomItem(),
                    State = StateArray.Abbreviations().ToList().GetRandomItem(),
                    Zip = ListExtensions.Random.Next(20000, 99999).ToString()
                };
                data.Add(customer);
                data.SaveChanges();
            }

            var limitLatitude = .00015f;
            var limitLongitude = .0003f;
            foreach (var file in Directory.GetFiles(@"C:\Users\MarkD\OneDrive\Documents\products"))
            {
                var product = new Product()
                {
                    Latitude = (1f - (float)random.NextDouble() * 2f) * limitLatitude,
                    Longitude = (1f - (float)random.NextDouble() * 2f) * limitLongitude,
                    Name = System.IO.Path.GetFileNameWithoutExtension(file),
                    Picture = File.ReadAllBytes(file)
                };
                data.Add(product);
                data.SaveChanges();
            }


        }
    }
}
