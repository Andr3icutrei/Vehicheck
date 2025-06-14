using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Core.Dtos.Requests.Patch
{
    public class PatchUserRequest
    {
        public int Id { get; set; }

        [MinLength(5), MaxLength(30)]
        public string? FirstName { get; set; }

        [MinLength(5), MaxLength(30)]
        public string? LastName { get; set; }

        public bool? IsAdmin { get; set; }

        [MinLength(5), MaxLength(40)]
        public string? Email { get; set; }

        [MinLength(5), MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Password { get; set; }
    }
}
