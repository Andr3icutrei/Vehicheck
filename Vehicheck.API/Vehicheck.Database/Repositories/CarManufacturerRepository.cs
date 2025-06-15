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
    public class CarManufacturerRepository(VehicheckDbContext databaseContext) : BaseRepository<CarManufacturer>(databaseContext), ICarManufacturerRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;

        public async Task<CarManufacturer?> GetCarManufacturerAsync(int manufacturerId)
        {
            return await _context.CarManufacturers
                .Include(cm => cm.Cars.Where(c => c.DeletedAt == null))
                .Include(cm => cm.Models.Where(m => m.DeletedAt == null))
                .Where(cm => cm.DeletedAt == null)
                .FirstOrDefaultAsync(cm => cm.Id == manufacturerId);
        }

        public async Task<List<CarManufacturer>> GetCarManufacturersAsync()
        {
            return await _context.CarManufacturers
                .Include(cm => cm.Cars.Where(c => c.DeletedAt == null))
                .Include(cm => cm.Models.Where(m => m.DeletedAt == null))
                .Where(cm => cm.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<CarManufacturer> AddCarManufacturerAsync(CarManufacturer carManufacturer)
        {
            if (carManufacturer == null)
                throw new ArgumentNullException(nameof(carManufacturer));

            Insert(carManufacturer);
            await SaveChangesAsync();
            return carManufacturer;
        }

        public async Task<CarManufacturer> UpdateCarManufacturerAsync(CarManufacturer carManufacturer)
        {
            if (carManufacturer == null)
                throw new ArgumentNullException(nameof(carManufacturer));

            Update(carManufacturer);
            await SaveChangesAsync();
            return carManufacturer;
        }

        public async Task<bool> DeleteCarManufacturerAsync(int id)
        {
            var carManufacturer = await GetFirstOrDefaultAsync(id);
            if (carManufacturer == null)
                return false;

            SoftDelete(carManufacturer);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<CarManufacturerResult>> GetCarManufacturersQueriedAsync(CarManufacturerQueryingFilter payload)
        {
            IQueryable<CarManufacturer> query = GetRecords();

            // Filtering
            if (!string.IsNullOrEmpty(payload.Name))
                query = query.Where(cm => cm.Name.Contains(payload.Name));

            // Sorting
            if (string.IsNullOrEmpty(payload.Params.SortBy))
                query = query.OrderBy(cm => cm.Id);
            else
                query = query.ApplySorting<CarManufacturer>(payload.Params.SortBy, payload.Params.SortDescending ?? false);

            // Get total count before paging
            int totalCount = await query.CountAsync();

            // Paging
            List<CarManufacturer> carManufacturers;
            if (payload.Params.PageSize == null || payload.Params.Page == null)
            {
                carManufacturers = await query.ToListAsync();
            }
            else
            {
                carManufacturers = await query
                    .Skip((int)payload.Params.PageSize * ((int)payload.Params.Page - 1))
                    .Take((int)payload.Params.PageSize)
                    .ToListAsync();
            }

            return new PagedResult<CarManufacturerResult>
            {
                Data = carManufacturers.Select(cm => CarManufacturerResult.ToResult(cm)),
                PageSize = payload.Params.PageSize ?? totalCount,
                Page = payload.Params.Page ?? 1,
                TotalPages = payload.Params.PageSize != null ?
                    (int)Math.Ceiling((double)totalCount / (int)payload.Params.PageSize) : 1
            };
        }
    }
}
