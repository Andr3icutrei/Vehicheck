using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class FixQueryRequestDto
    {
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public QueryParametersDto? Params { get; set; } = new QueryParametersDto();

        public FixQueryingFilter ToQueryingFilter()
        {
            return new FixQueryingFilter
            {
                Description = Description,
                Price = Price,
                Params = Params.ToQueryParameters()
            };
        }
    }
}
