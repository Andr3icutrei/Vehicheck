using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Context;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Extensions;
using Vehicheck.Database.Models.Querying.Filters;
using Vehicheck.Database.Models.Querying.Results;
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                .ThenInclude(c => c.CarManufacturer)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                    .ThenInclude(c => c.CarModel)
                        .ThenInclude(carModel => carModel.Components)
                            .ThenInclude(components => components.Component)
                                .ThenInclude(component => component.Manufacturer)
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                    .ThenInclude(c => c.CarManufacturer)
                .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                    .ThenInclude(c => c.CarModel)
                        .ThenInclude(carModel => carModel.Components)
                            .ThenInclude(components => components.Component)
                                .ThenInclude(component => component.Manufacturer)
                .OrderBy(u => u.Id)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetFirstOrDefaultAsync(id);
            if (user == null)
                return false;

            SoftDelete(user);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<UserResult>> GetUsersQueriedAsync(UserQueryingFilter payload)
        {
            // Sorting + filtering
            IQueryable<User> query = GetRecords()
                                        .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                                            .ThenInclude(c => c.CarManufacturer)
                                        .Include(u => u.Cars.Where(c => c.DeletedAt == null))
                                            .ThenInclude(c => c.CarModel)
                                                .ThenInclude(carModel => carModel.Components)
                                                    .ThenInclude(components => components.Component)
                                                        .ThenInclude(component => component.Manufacturer);

            if (!string.IsNullOrEmpty(payload.FirstName))
                query = query.Where(u => u.FirstName.Contains(payload.FirstName));

            if (!string.IsNullOrEmpty(payload.LastName))
                query = query.Where(u => u.LastName.Contains(payload.LastName));

            if(!string.IsNullOrEmpty(payload.Email))
                query = query.Where(u => u.Email.Contains(payload.Email));

            if(!string.IsNullOrEmpty(payload.Phone))
                query = query.Where(u => u.Phone.Contains(payload.Phone));

            if (payload.Params.SortBy.IsNullOrEmpty())
                query = query.OrderBy(u => u.Id);

            query.ApplySorting<User>(payload.Params.SortBy, payload.Params.SortDescending ?? false);

            // Paging
            int totalCount = await query.CountAsync();

            List<User>? users;
            if (!(payload.Params.PageSize != null && payload.Params.PageSize != null))
            {
                users = await query.ToListAsync();
            }
            else
            {
                users = await query.
                    Skip((int)payload.Params.PageSize * ((int)payload.Params.Page - 1)).
                    Take((int)payload.Params.PageSize).
                    ToListAsync();
            }

            return new PagedResult<UserResult>
            {
                Data = users.Select(u => UserResult.ToResult(u)),
                PageSize = (int)payload.Params.PageSize,
                Page = (int)payload.Params.Page,
                TotalPages = payload.Params.PageSize != null ? (int)Math.Ceiling((double)totalCount / (int)payload.Params.PageSize) : 1
            };
        }
    }
}
