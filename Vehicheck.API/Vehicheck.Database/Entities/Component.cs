using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Vehicheck.Database.Entities
{
    public class Component : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }

        [Precision(10,2)]
        public decimal Price {  get; set; }

        // navigation properties
        public int? ComponentManufacturerId { get; set; }
        public ComponentManufacturer? Manufacturer { get; set; } 

        public ICollection<CarComponent> Cars { get; set; } = new List<CarComponent>();
        public ICollection<ComponentFix> Fixes { get; set; } = new List<ComponentFix>();
    }
}
