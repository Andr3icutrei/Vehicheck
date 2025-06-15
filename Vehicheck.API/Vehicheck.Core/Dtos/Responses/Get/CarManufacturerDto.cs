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
    public class CarManufacturerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static CarManufacturerDto ToDto(CarManufacturerResult result)
        {
            return new CarManufacturerDto
            {
                Id = result.Id,
                Name = result.Name
            };
        }
    }
}
