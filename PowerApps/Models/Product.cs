using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerApps.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [NotMapped]
        public string PictureUrl { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }
        
        [JsonIgnore]
        public List<ProductOrder> ProductOrders { get; set; }
    }
}