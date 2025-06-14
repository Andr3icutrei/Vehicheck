using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserDto?> GetUserAsync(int id);
        Task<List<GetUserDto>> GetUsersAsync();
        Task<User> AddUserAsync(AddUserRequest payload);
        Task<bool> DeleteUserAsync(int id);
    }
}
