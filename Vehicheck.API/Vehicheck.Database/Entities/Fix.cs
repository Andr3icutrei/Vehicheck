using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Vehicheck.Database.Entities
{
    public class Fix : BaseEntity
    {
        [MaxLength(100)]
        public string Description { get; set; }

        [Precision(10,2)]
        public decimal Price { get; set; }

        // navigation properties
        public ICollection<ComponentFix> Components { get; set; } = new List<ComponentFix>();
    }
}
