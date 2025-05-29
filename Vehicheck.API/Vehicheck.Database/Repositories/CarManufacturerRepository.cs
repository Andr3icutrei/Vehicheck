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
    public class CarManufacturerRepository(VehicheckDbContext databaseContext) : BaseRepository<CarManufacturer>(databaseContext), ICarManufacturerRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;

        public async Task<CarManufacturer?> GetCarManufacturerId(int manufacturerId)
        {
            return await _context.CarManufacturers
                .Include(cm => cm.Cars)
                .Include(cm => cm.Models)
                .Where(cm => cm.DeletedAt == null)
                .FirstOrDefaultAsync(cm => cm.Id == manufacturerId);
        }

        public async Task<List<CarManufacturer>> GetAllCarManufacturer()
        {
            return await _context.CarManufacturers
                .Include(cm => cm.Cars)
                .Include(cm => cm.Models)
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
    }
}
