using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Context;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Database.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly VehicheckDbContext _context;
        public UserRepository(VehicheckDbContext databaseContext) : base(databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            Insert(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return _context.Users
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        public Task<User?> GetUserAsync(int id)
        {
            return _context.Users
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
