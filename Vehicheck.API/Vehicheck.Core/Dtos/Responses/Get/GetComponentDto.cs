using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class GetComponentDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public GetComponentManufacturerDto Manufacturer { get; set; }
    }
}
