using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Entities
{
    public class User : BaseEntity
    {
        [MinLength(5),MaxLength(30)]
        public string FirstName { get; set; }

        [MinLength(5),MaxLength(30)]
        public string LastName { get; set; }
    
        public bool IsAdmin { get; set; }

        [MinLength(5),MaxLength(40)]   
        public string Email { get; set; }

        [MinLength(5), MaxLength(20)]
        public string Phone {  get; set; }

        [MinLength(5), MaxLength(30)]
        public string Password { get; set; }

        // navigation properties
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
