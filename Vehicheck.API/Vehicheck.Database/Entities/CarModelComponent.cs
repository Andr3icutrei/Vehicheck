using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Vehicheck.Database.Entities
{
    [PrimaryKey(nameof(CarModelId), nameof(ComponentId))]
    public class CarModelComponent
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // navigation properties
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }

        public int ComponentId { get; set; }
        public Component Component { get; set; }
    }
}
