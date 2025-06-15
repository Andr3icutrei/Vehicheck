using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Models.Querying.Filters
{
    public class CarQueryingFilter
    {
        public int? YearOfManufacture { get; set; }
        public int? CarMileage { get; set; }
        public string? CarModel { get; set; }
        public string? CarManufacturer { get; set; }
        public QueryParameters? Params { get; set; }
    }
}
