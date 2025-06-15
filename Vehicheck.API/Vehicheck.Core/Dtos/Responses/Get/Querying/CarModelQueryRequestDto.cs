using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class CarModelQueryRequestDto
    {
        public string? Name { get; set; }
        public int? ReleaseYear { get; set; }
        public QueryParametersDto? Params { get; set; } = new QueryParametersDto();

        public CarModelQueryingFilter ToQueryingFilter()
        {
            return new CarModelQueryingFilter
            {
                Name = Name,
                ReleaseYear = ReleaseYear,
                Params = Params.ToQueryParameters()
            };
        }
    }
}
