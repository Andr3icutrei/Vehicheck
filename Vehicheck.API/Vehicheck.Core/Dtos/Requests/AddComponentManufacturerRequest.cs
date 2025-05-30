using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Core.Dtos.Requests
{
    public class AddComponentManufacturerRequest
    {
        public string Name { get; set; }
        public int YearOfFounding { get; set; }
    }
}
