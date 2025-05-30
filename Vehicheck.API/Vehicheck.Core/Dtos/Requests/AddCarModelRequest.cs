using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Core.Dtos.Requests
{
    public class AddCarModelRequest
    {
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public int CarManufacturerId { get; set; }
    }
}
