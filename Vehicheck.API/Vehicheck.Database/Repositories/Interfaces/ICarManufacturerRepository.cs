using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface ICarManufacturerRepository : IBaseRepository<CarManufacturer>
    {
        Task<CarManufacturer?> GetCarManufacturerId(int manufacturerId);
        Task<List<CarManufacturer>> GetAllCarManufacturer();
        Task<CarManufacturer> AddCarManufacturerAsync(CarManufacturer carManufacturer);
        Task<CarManufacturer> UpdateCarManufacturerAsync(CarManufacturer carManufacturer);
    }
}
