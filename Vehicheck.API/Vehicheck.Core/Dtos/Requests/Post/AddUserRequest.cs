using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Post
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public User ToEntity()
        {
            User u = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                IsAdmin = IsAdmin,
            };
            u.SetPassword(Password);
            return u;
        }
    }
}
