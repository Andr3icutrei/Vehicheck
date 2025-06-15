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
    public class FixRepository : BaseRepository<Fix>, IFixRepository
    {
        private readonly VehicheckDbContext _context;
        public FixRepository(VehicheckDbContext databaseContext) : base(databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<Fix> AddFixAsync(Fix fix)
        {
            Insert(fix);
            await _context.SaveChangesAsync();
            return fix;
        }

        public async Task<List<Fix>> GetAllFixesAsync()
        {
            return await _context.Fixes
                .Where(f => f.DeletedAt == null)
                .Include(f => f.Components.Where(cf => cf.DeletedAt == null))
                .ThenInclude(cf => cf.Component)
                .OrderBy(f => f.Id)
                .ToListAsync();  
        }

        public async Task<Fix?> GetFixAsync(int id)
        {
            return await _context.Fixes
                .Where(f => f.DeletedAt == null)
                .Include(f => f.Components.Where(cf => cf.DeletedAt == null))
                .ThenInclude(cf => cf.Component)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> DeleteFixAsync(int id)
        {
            var fix = await GetFirstOrDefaultAsync(id);
            if (fix == null)
                return false;

            SoftDelete(fix);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<FixResult>> GetFixesQueryiedAsync(FixQueryingFilter payload)
        {
            // Sorting + filtering
            IQueryable<Fix> query = GetRecords();

            if (!string.IsNullOrEmpty(payload.Description))
                query = query.Where(f => f.Description.Contains(payload.Description));

            if (payload.Price.HasValue)
                query = query.Where(f => f.Price == payload.Price);

            if (payload.Params.SortBy.IsNullOrEmpty())
                query = query.OrderBy(f => f.Id);

            query.ApplySorting<Fix>(payload.Params.SortBy, payload.Params.SortDescending ?? false);

            // Paging
            int totalCount = await query.CountAsync();

            List<Fix>? components;
            if (!(payload.Params.PageSize != null && payload.Params.PageSize != null))
            {
                components = await query.ToListAsync();
            }
            else
            {
                components = await query.
                    Skip((int)payload.Params.PageSize * ((int)payload.Params.Page - 1)).
                    Take((int)payload.Params.PageSize).
                    ToListAsync();
            }

            return new PagedResult<FixResult>
            {
                Data = components.Select(f => FixResult.ToResult(f)),
                PageSize = (int)payload.Params.PageSize,
                Page = (int)payload.Params.Page,
                TotalPages = payload.Params.PageSize != null ? (int)Math.Ceiling((double)totalCount / (int)payload.Params.PageSize) : 1
            };
        }
    }
}
