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
using Vehicheck.Infrastructure.Exceptions;

namespace Vehicheck.Database.Repositories
{
    public class CarModelRepository(VehicheckDbContext databaseContext) : BaseRepository<CarModel>(databaseContext), ICarModelRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;

        public async Task<CarModel?> GetCarModelAsync(int modelId)
        {
            return await _context.CarModels
                .Include(cm => cm.Manufacturer)
                .Include(cm => cm.Cars.Where(c => c.DeletedAt == null))
                .Where(cm => cm.DeletedAt == null)
                .FirstOrDefaultAsync(cm => cm.Id == modelId);
        }

        public async Task<List<CarModel>> GetAllCarModelsAsync()
        {
            return await _context.CarModels
                .Include(cm => cm.Manufacturer)
                .Include(cm => cm.Cars.Where(c => c.DeletedAt == null))
                .Where(cm => cm.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<CarModel> AddCarModelAsync(CarModel carModel)
        {
            if (carModel == null)
                throw new ArgumentNullException(nameof(carModel));

            Insert(carModel);
            await SaveChangesAsync();
            return carModel;
        }

        public async Task<CarModel> UpdateCarModelAsync(CarModel carModel)
        {
            if (carModel == null)
                throw new ArgumentNullException(nameof(carModel));

            Update(carModel);
            await SaveChangesAsync();
            return carModel;
        }

        public async Task<bool> DeleteCarModelAsync(int id)
        {
            var carModel = await GetFirstOrDefaultAsync(id);
            if (carModel == null)
                return false;

            SoftDelete(carModel);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<CarModelResult>> GetCarModelQueryiedAsync(CarModelQueryingFilter payload)
        {
            // Sorting + filtering
            IQueryable<CarModel> query = GetRecords().Include(cm => cm.Manufacturer).Where(cm => cm.Manufacturer.DeletedAt == null);

            if (!string.IsNullOrEmpty(payload.Name))
                query = query.Where(cm => cm.Name.Contains(payload.Name));

            if(payload.ReleaseYear.HasValue)
                query = query.Where(cm => cm.ReleaseYear ==  payload.ReleaseYear.Value);

            if (payload.Params.SortBy.IsNullOrEmpty())
                query = query.OrderBy(cm => cm.Id);

            query.ApplySorting<CarModel>(payload.Params.SortBy, payload.Params.SortDescending ?? false);

            // Paging
            int totalCount = await query.CountAsync();

            List<CarModel>? carModels;
            if (!(payload.Params.PageSize != null && payload.Params.PageSize != null))
            {
                carModels = await query.ToListAsync();
            }
            else
            {
                carModels = await query.
                    Skip((int)payload.Params.PageSize * ((int)payload.Params.Page - 1)).
                    Take((int)payload.Params.PageSize).
                    ToListAsync();
            }

            return new PagedResult<CarModelResult>
            {
                Data = carModels.Select(cm => CarModelResult.ToResult(cm)),
                PageSize = (int)payload.Params.PageSize,
                Page = (int)payload.Params.Page,
                TotalPages = payload.Params.PageSize != null ?
                    (int)Math.Ceiling((double)totalCount / (int)payload.Params.PageSize) : 1
            };
        }
    }
}
