using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class ComponentManufacturerQueryRequestDto
    {
        public string? Name { get; set; }
        public int? YearOfFounding { get; set; }
        public QueryParametersDto? Params { get; set; } = new QueryParametersDto();

        public ComponentManufacturerQueryingFilter ToQueryingFilter()
        {
            return new ComponentManufacturerQueryingFilter()
            {
                Name = Name,
                YearOfFounding = YearOfFounding,
                Params = Params.ToQueryParameters()
            };  
        }
    }
}
