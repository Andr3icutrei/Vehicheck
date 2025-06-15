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
    public class ComponentManufacturerRepository : BaseRepository<ComponentManufacturer>, IComponentManufacturerRepository
    {
        private readonly VehicheckDbContext _context;

        public ComponentManufacturerRepository(VehicheckDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ComponentManufacturer> AddComponentAsync(ComponentManufacturer componentManufacturer)
        {
            Insert(componentManufacturer); 
            await _context.SaveChangesAsync();
            return componentManufacturer;
        }

        public async Task<List<ComponentManufacturer>> GetAllComponentsAsync()
        {
            return await _context.ComponentManufacturers.
                Where(cm => cm.DeletedAt == null).
                Include(cm => cm.Components.Where(c => c.DeletedAt == null)).
                OrderBy(cm => cm.Id).
                ToListAsync();
        }

        public async Task<ComponentManufacturer?> GetComponentManufacturerAsync(int id)
        {
            return await _context.ComponentManufacturers.
                Where(cm => cm.DeletedAt == null).
                Include(cm => cm.Components.Where(c => c.DeletedAt == null)).
                FirstOrDefaultAsync(cm => cm.Id == id);
        }

        public async Task<bool> DeleteComponentManufacturerAsync(int id)
        {
            var componentManufacturer = await GetFirstOrDefaultAsync(id);

            if (componentManufacturer == null)
                return false;

            SoftDelete(componentManufacturer);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<ComponentManufacturerResult>> GetComponentmanufacturersQueriedAsync(ComponentManufacturerQueryingFilter payload)
        {
            // Sorting + filtering
            IQueryable<ComponentManufacturer> query = GetRecords();

            if (!string.IsNullOrEmpty(payload.Name))
                query = query.Where(cm => cm.Name.Contains(payload.Name));

            if (payload.YearOfFounding.HasValue)
                query = query.Where(cm => cm.YearOfFounding == payload.YearOfFounding);

            if (payload.Params.SortBy.IsNullOrEmpty())
                query = query.OrderBy(cm => cm.Id);

            query.ApplySorting<ComponentManufacturer>(payload.Params.SortBy, payload.Params.SortDescending ?? false);

            // Paging
            int totalCount = await query.CountAsync();

            List<ComponentManufacturer>? componentManufacturers;
            if (!(payload.Params.PageSize != null && payload.Params.PageSize != null))
            {
                componentManufacturers = await query.ToListAsync();
            }
            else
            {
                componentManufacturers = await query.
                    Skip((int)payload.Params.PageSize * ((int)payload.Params.Page - 1)).
                    Take((int)payload.Params.PageSize).
                    ToListAsync();
            }

            return new PagedResult<ComponentManufacturerResult>
            {
                Data = componentManufacturers.Select(cm => ComponentManufacturerResult.ToResult(cm)),
                PageSize = (int)payload.Params.PageSize,
                Page = (int)payload.Params.Page,
                TotalPages = totalCount
            };
        }
    }
}
