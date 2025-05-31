using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserAsync(int id);   
        Task<List<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
