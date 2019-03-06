using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PowerApps.Models
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        [Column(TypeName = "image")]
        public byte[] Picure { get; set; }

        [NotMapped]
        public string PictureUrl { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; }
    }
}
