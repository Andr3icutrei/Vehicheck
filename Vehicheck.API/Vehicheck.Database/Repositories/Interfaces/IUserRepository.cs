using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Filters;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> AddUserAsync(User user);
        Task<PagedResult<UserResult>> GetUsersQueriedAsync(UserQueryingFilter payload);
        Task<User?> GetUserAsync(int id);   
        Task<List<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
