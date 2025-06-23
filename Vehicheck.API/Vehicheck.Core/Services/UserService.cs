using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Dtos.Responses.Get.Querying;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Filters;
using Vehicheck.Database.Models.Querying.Results;
using Vehicheck.Database.PatchHelpers;
using Vehicheck.Database.Repositories.Interfaces;
using Vehicheck.Infrastructure.Exceptions;

namespace Vehicheck.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> AddUserAsync(AddUserRequest payload)
        {
            var user = payload.ToEntity();
            user.SetPassword(payload.Password);
            return (await _repository.AddUserAsync(user)).ToDto();
        }

        public async Task<UserDto?> GetUserAsync(int id)
        {
            var result = await _repository.GetUserAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("User", id);
            }

            return result.ToDto();
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var result = await _repository.GetAllUsersAsync();
            return result.Select(u => u.ToDto()).ToList();   
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repository.GetFirstOrDefaultAsync(id);
            if (user == null)
            {
                throw new EntityNotFoundException("User", id);
            }
            return await _repository.DeleteUserAsync(id);
        }

        public async Task<UserDto> PatchUserAsync(PatchUserRequest payload)
        {
            User? user = await _repository.GetUserAsync(payload.Id);
            if (user == null)
            {
                throw new EntityNotFoundException("User", payload.Id);
            }
            PatchRequestToEntity.PatchFrom<PatchUserRequest, User>(user, payload);

            user.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return user.ToDto();
        }

        public async Task<PagedResponse<UserDto>> GetUsersQueryiedAsync(UserQueryRequestDto payload)
        {
            PagedResult<UserResult> result = await _repository.GetUsersQueriedAsync(payload.ToQueryingFilter());
            return new PagedResponse<UserDto>
            {
                Data = result.Data.Select(u => UserDto.ToDto(u)),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
            };
        }
    }
}
