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
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<Car?> GetCarAsync(int carId);
        Task<List<Car>> GetAllCarsAsync();
        Task<PagedResult<CarResult>> GetCarsQueriedAsync(CarQueryingFilter payload);
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(Car car);
        Task<bool> DeleteCarAsync(int id);
    }
}
