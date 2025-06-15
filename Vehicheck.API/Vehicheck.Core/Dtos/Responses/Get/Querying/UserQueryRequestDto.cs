using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Dtos.Responses.Get.Querying
{
    public class UserQueryRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsAdmin { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public QueryParametersDto? Params { get; set; } = new QueryParametersDto();

        public UserQueryingFilter ToQueryingFilter()
        {
            return new UserQueryingFilter()
            {
                FirstName = FirstName,
                LastName = LastName,
                IsAdmin = IsAdmin,
                Email = Email,
                Phone = Phone,
                Params = Params.ToQueryParameters()
            };
        }
    }
}
