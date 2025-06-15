using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Results
{
    public class UserResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public static UserResult ToResult(User self)
        {
            return new UserResult
            {
                Id = self.Id,
                FirstName = self.FirstName,
                LastName = self.LastName,
                Email = self.Email,
                Phone = self.Phone,
            };
        }
    }
}
