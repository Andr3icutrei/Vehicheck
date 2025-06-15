using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Filters
{
    public class CarManufacturerQueryingFilter
    {
        public string Name { get; set; }
        public QueryParameters? Params { get; set; }
    }
}
