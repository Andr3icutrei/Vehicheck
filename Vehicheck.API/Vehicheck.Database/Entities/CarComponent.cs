using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Vehicheck.Database.Entities
{
    [PrimaryKey(nameof(CarId), nameof(ComponentId))]
    public class CarComponent
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // navigation properties
        public int CarId { get; set; }
        public Car Car { get; set; }

        public int ComponentId { get; set; }
        public Component Component { get; set; }
    }
}
