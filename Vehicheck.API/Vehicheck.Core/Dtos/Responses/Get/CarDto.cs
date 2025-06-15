using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class CarDto
    {
        public int Id { get; set; }
        public int YearOfManufacture { get; set; }
        public int CarMileage { get; set; }
        public string CarModel { get; set; }
        public string CarManufacturer { get; set; } 
        public List<ComponentDto> Components { get; set; } = new List<ComponentDto>();

        public static CarDto ToDto(CarResult result)
        {
            return new CarDto
            {
                Id = result.Id,
                YearOfManufacture = result.YearOfManufacture,
                CarMileage = result.CarMileage,
                CarManufacturer = result.CarManufacturer,
                CarModel = result.CarModel
            };
        }
    }
}
