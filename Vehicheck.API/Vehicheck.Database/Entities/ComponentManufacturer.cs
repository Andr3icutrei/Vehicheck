using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Entities
{
    public class ComponentManufacturer : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public int YearOfFounding { get; set; }

        //navigation properties
        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}
