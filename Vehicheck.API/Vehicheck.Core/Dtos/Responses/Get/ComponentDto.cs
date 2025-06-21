using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Mapping;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class ComponentDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }

        public static ComponentDto ToDto(ComponentResult result)
        {
            return new ComponentDto
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Manufacturer = result.ComponentManufacturer.Name,
            };
        }
    }
}
