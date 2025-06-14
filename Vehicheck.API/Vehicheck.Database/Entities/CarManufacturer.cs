using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Entities
{
    public class CarManufacturer : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }

        // navigation properties 
        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public ICollection<CarModel> Models { get; set; } = new List<CarModel>();
    }
}
