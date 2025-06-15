using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class CarManufacturerQueryRequestDto
    {
        public string? Name { get; set; }
        public QueryParametersDto? Params { get; set; } = new QueryParametersDto();

        public CarManufacturerQueryingFilter ToQueryingFilter()
        {
            return new CarManufacturerQueryingFilter
            {
                Name = Name,
                Params = Params.ToQueryParameters() 
            };
        }
    }
}
