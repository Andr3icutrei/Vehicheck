using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Entities
{
    public class CarModel : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public int ReleaseYear { get; set; }

        //navigation properties 
        public int? CarManufacturerId { get; set; }
        public CarManufacturer? Manufacturer { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();   
    }
}
