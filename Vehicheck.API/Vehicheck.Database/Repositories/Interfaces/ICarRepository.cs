using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<Car?> GetCarAsync(int carId);
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(Car car);
    }
}
