using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Context;
using Vehicheck.Database.Repositories.Interfaces;
using Vehicheck.Database.Models.Querying.Filters;
using Microsoft.IdentityModel.Tokens;
using Vehicheck.Database.Extensions;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Database.Repositories
{
    public class ComponentRepository(VehicheckDbContext databaseContext) : BaseRepository<Component>(databaseContext), IComponentRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;

        public async Task<Component?> GetComponentAsync(int componentId)
        {
            return await _context.Components
                .Include(c => c.Manufacturer)
                .Include(c => c.Cars.Where(c => c.DeletedAt == null))
                    .ThenInclude(cc => cc.Car)
                .Include(c => c.Fixes.Where(c => c.DeletedAt == null))
                    .ThenInclude(cf => cf.Fix)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == componentId);
        }

        public async Task<List<Component>> GetAllComponentsAsync()
        {
            return await _context.Components
                .Include(c => c.Manufacturer)
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Component> AddComponentAsync(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            Insert(component);
            await SaveChangesAsync();
            return component;
        }

        public async Task<Component> UpdateComponentAsync(Component component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            Update(component);
            await SaveChangesAsync();
            return component;
        }
        public async Task<bool> DeleteComponentAsync(int id)
        {
            var component = await GetFirstOrDefaultAsync(id);

            if (component == null)
                return false;

            SoftDelete(component);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<ComponentResult>> GetAllComponentsQueriedAsync(ComponentQueryingFilter payload)
        {
            // Sorting + filtering
            IQueryable<Component> query = GetRecords();

            if (!string.IsNullOrEmpty(payload.Name))
                query = query.Where(cm => cm.Name.Contains(payload.Name));

            if (payload.Price.HasValue)
                query = query.Where(cm => cm.Price == payload.Price);

            if (payload.Params.SortBy.IsNullOrEmpty())
                query = query.OrderBy(cm => cm.Id);

            query.ApplySorting<Component>(payload.Params.SortBy, payload.Params.SortDescending ?? false);

            // Paging
            int totalCount = await query.CountAsync();

            List<Component>? components;
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

            return new PagedResult<ComponentResult>
            {
                Data = components.Select(c => ComponentResult.ToResult(c)),
                PageSize = (int)payload.Params.PageSize,
                Page = (int)payload.Params.Page,
                TotalPages = payload.Params.PageSize != null ?
                    (int)Math.Ceiling((double)totalCount / (int)payload.Params.PageSize) : 1
            };
        }
    }
}
