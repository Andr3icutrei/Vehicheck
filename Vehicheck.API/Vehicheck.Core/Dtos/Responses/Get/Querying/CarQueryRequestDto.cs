using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class CarQueryRequestDto
    {
        public int? YearOfManufacture { get; set; }
        public int? CarMileage { get; set; }
        public string? CarModel { get; set; }
        public string? CarManufacturer { get; set; } 
        public QueryParametersDto? Params { get; set; } = new QueryParametersDto();

        public CarQueryingFilter ToQueryingFilter()
        {
            return new CarQueryingFilter()
            {
                YearOfManufacture = YearOfManufacture,
                CarMileage = CarMileage,
                CarModel = CarModel,
                CarManufacturer = CarManufacturer,
                Params = Params.ToQueryParameters()
            };
        }
    }
}
