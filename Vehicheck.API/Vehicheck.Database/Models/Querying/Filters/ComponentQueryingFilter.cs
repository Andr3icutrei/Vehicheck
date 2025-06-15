using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Models.Querying.Filters
{
    public class ComponentQueryingFilter
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public QueryParameters? Params { get; set; }
    }
}
