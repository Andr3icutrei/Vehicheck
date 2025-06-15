using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Models.Querying.Filters
{
    public class UserQueryingFilter
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsAdmin { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public QueryParameters? Params { get; set; }
    }
}
