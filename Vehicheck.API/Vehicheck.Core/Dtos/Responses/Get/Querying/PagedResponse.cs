using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;
    }
}
