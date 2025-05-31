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
    public class CarRepository(VehicheckDbContext databaseContext) : BaseRepository<Car>(databaseContext), ICarRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;
        public async Task<Car?> GetCarAsync(int carId)
        {
            return await _context.Cars
                .Include(c => c.User)
                .Include(c => c.CarModel)
                .Include(c => c.CarManufacturer)
                .Include(c => c.Components)
                    .ThenInclude(cc => cc.Component)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == carId);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars
                .Include(c => c.User)
                .Include(c => c.CarModel)
                .Include(c => c.CarManufacturer)
                .Include(c => c.Components)
                    .ThenInclude(cc => cc.Component)
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
    }
}
