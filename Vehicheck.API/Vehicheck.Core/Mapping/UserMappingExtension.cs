using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Mapping
{
    public static class UserMappingExtension
    {
        public static UserDto ToDto(this User self)
        {
            return new UserDto
            {
                Id = self.Id,
                FirstName = self.FirstName,
                LastName = self.LastName,
                Email = self.Email,
                Phone = self.Phone,
                Cars = self.Cars.ToList().Select(c => c.ToDto()).ToList()
            };
        }
    }
}
