using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public List<CarDto> Cars { get; set; } = new List<CarDto>();

        public static UserDto ToDto(UserResult result)
        {
            return new UserDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                IsAdmin = result.IsAdmin,
                Email = result.Email,
                Phone = result.Phone,
                Password = result.Password,
            };
        }
    }
}
