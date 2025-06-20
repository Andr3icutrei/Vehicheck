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
    public class CarRepository(VehicheckDbContext databaseContext) : BaseRepository<Car>(databaseContext), ICarRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;
        public async Task<Car?> GetCarAsync(int carId)
        {
            return await _context.Cars
                .Include(c => c.User)
                .Include(c => c.CarModel)
                .Include(c => c.CarManufacturer)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == carId);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars
                .Include(c => c.User)
                .Include(c => c.CarManufacturer)
                .Include(c => c.CarModel)
                    .ThenInclude(cm => cm.Components) // CarModel.Components (collection of CarModelComponent)
                        .ThenInclude(cmc => cmc.Component) // CarModelComponent.Component (navigation)
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            Insert(car);
            await SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            Update(car);
            await SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await GetFirstOrDefaultAsync(id);
            if (car == null)
                return false;

            SoftDelete(car);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<CarResult>> GetCarsQueriedAsync(CarQueryingFilter payload)
        {
            // Sorting + filtering
            IQueryable<Car> query = GetRecords();

            if(payload.YearOfManufacture.HasValue)
                query = query.Where(c => c.YearOfManufacture == payload.YearOfManufacture);

            if (payload.CarMileage.HasValue)
                query = query.Where(c => c.CarMileage == payload.CarMileage);

            if (!string.IsNullOrEmpty(payload.CarModel))
            {
                query = query.Where(c => c.CarModel.Name.Contains(payload.CarModel)); 
            }
            query = query.Include(c => c.CarModel);

            if (!string.IsNullOrEmpty(payload.CarManufacturer))
            {
                query = query.Where(c => c.CarManufacturer.Name.Contains(payload.CarManufacturer));
            }
            query = query.Include(c => c.CarManufacturer);

            if (payload.Params.SortBy.IsNullOrEmpty())
                query = query.OrderBy(cm => cm.Id);

            query.ApplySorting<Car>(payload.Params.SortBy, payload.Params.SortDescending ?? false);

            // Paging
            int totalCount = await query.CountAsync();

            List<Car>? cars;
            if (!(payload.Params.PageSize != null && payload.Params.PageSize != null))
            {
                cars = await query.ToListAsync();
            }
            else
            {
                cars = await query.
                    Skip((int)payload.Params.PageSize * ((int)payload.Params.Page - 1)).
                    Take((int)payload.Params.PageSize).
                    ToListAsync();
            }

            return new PagedResult<CarResult>
            {
                Data = cars.Select(c => CarResult.ToResult(c)),
                PageSize = (int)payload.Params.PageSize,
                Page = (int)payload.Params.Page,
                TotalPages = payload.Params.PageSize != null ?
                    (int)Math.Ceiling((double)totalCount / (int)payload.Params.PageSize) : 1
            };
        }
    }
}
