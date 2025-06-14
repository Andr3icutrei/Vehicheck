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
    public class CarModelRepository(VehicheckDbContext databaseContext) : BaseRepository<CarModel>(databaseContext), ICarModelRepository
    {
        private readonly VehicheckDbContext _context = databaseContext;

        public async Task<CarModel?> GetCarModelAsync(int modelId)
        {
            return await _context.CarModels
                .Include(cm => cm.Manufacturer)
                .Include(cm => cm.Cars)
                .Where(cm => cm.DeletedAt == null)
                .FirstOrDefaultAsync(cm => cm.Id == modelId);
        }

        public async Task<List<CarModel>> GetAllCarModelsAsync()
        {
            return await _context.CarModels
                .Include(cm => cm.Manufacturer)
                .Include(cm => cm.Cars)
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
    }
}
