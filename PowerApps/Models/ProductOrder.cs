using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerApps.Models
{
    public class ProductOrder
    {
        [Key]
        public int Id { get; set; }

        public bool IsPackaged { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}
