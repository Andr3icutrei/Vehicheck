using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Filters;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface ICarManufacturerRepository : IBaseRepository<CarManufacturer>
    {
        Task<CarManufacturer?> GetCarManufacturerAsync(int manufacturerId);
        Task<PagedResult<CarManufacturerResult>> GetCarManufacturersQueriedAsync(CarManufacturerQueryingFilter payload);
        Task<List<CarManufacturer>> GetCarManufacturersAsync();
        Task<CarManufacturer> AddCarManufacturerAsync(CarManufacturer carManufacturer);
        Task<CarManufacturer> UpdateCarManufacturerAsync(CarManufacturer carManufacturer);
        Task<bool> DeleteCarManufacturerAsync(int id);
    }
}
