using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Entities
{
    public class Car : BaseEntity
    {
        public int YearOfManufacture {  get; set; }
        public int CarMileage { get; set; }
        
        // navigation properties 

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? CarModelId { get; set; }
        public CarModel? CarModel { get; set; }

        public int? CarManufacturerId { get; set; }
        public CarManufacturer? CarManufacturer { get; set; }
        
        public ICollection<CarComponent> Components { get; set; } = new List<CarComponent>();
    }
}
