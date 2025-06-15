using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class QueryParametersDto
    {
        public string? SortBy { get; set; } = "Id";
        public bool? SortDescending { get; set; } = false;

        public string? SearchTerm { get; set; }

        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 1;

        public QueryParameters ToQueryParameters()
        {
            return new QueryParameters
            {
                SortBy = SortBy,
                SortDescending = SortDescending,
                PageSize = PageSize,
                Page = Page
            };
        }
    }
}
