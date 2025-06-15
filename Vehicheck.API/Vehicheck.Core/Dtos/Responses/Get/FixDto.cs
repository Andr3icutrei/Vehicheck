using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class FixDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ComponentDto> Components { get; set; } = new List<ComponentDto>();

        public static FixDto ToDto(FixResult result)
        {
            return new FixDto
            {
                Id = (int)result.Id,
                Description = result.Description,
                Price = (int)result.Price
            };
        }
    }
}
