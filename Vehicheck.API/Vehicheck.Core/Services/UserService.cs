using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> AddUserAsync(AddUserRequest payload)
        {
            var user = payload.ToEntity();
            user.SetPassword(payload.Password);
            return await _repository.AddUserAsync(user);
        }

        public async Task<GetUserDto?> GetUserAsync(int id)
        {
            var result = await _repository.GetUserAsync(id);
            return result.ToDto();
        }

        public async Task<List<GetUserDto>> GetUsersAsync()
        {
            var result = await _repository.GetAllUsersAsync();
            return result.Select(u => u.ToDto()).ToList();   
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _repository.DeleteUserAsync(id);
        }
    }
}
