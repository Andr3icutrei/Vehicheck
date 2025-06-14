using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Post
{
    public class AddComponentRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ComponentManufacturerId { get; set; }

        public Component ToEntity()
        {
            return new Component
            {
                Name = Name,
                Price = Price,
                ComponentManufacturerId = ComponentManufacturerId
            };
        }
    }
}
