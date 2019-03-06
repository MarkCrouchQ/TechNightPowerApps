using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PowerApps.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public List<ProductOrder> ProductOrders { get; set; }

        public Customer Customer { get; set; }

        public int? AgentId { get; set; }
        public Agent Agent { get; set; }

        public bool HasShipped { get; set; }
    }
}
