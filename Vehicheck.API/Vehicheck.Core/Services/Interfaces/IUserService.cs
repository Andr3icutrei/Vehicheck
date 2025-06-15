using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Dtos.Responses.Get.Querying;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserAsync(int id);
        Task<PagedResponse<UserDto>> GetUsersQueryiedAsync(UserQueryRequestDto payload);
        Task<List<UserDto>> GetUsersAsync();
        Task<User> AddUserAsync(AddUserRequest payload);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDto> PatchUserAsync(PatchUserRequest payload);
    }
}
