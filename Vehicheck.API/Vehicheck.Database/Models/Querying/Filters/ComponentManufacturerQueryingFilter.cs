using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Models.Querying.Filters
{
    public class ComponentManufacturerQueryingFilter
    {
        public string? Name { get; set; }
        public int? YearOfFounding { get; set; }
        public QueryParameters? Params { get; set; }
    }
}
