using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    internal interface ICarModelRepository : IBaseRepository<CarModel>
    {
        Task<CarModel?> GetCarModelAsync(int modelId);
        Task<List<CarModel>> GetAllCarModelsAsync();
        Task<CarModel> AddCarModelAsync(CarModel carModel);
        Task<CarModel> UpdateCarModelAsync(CarModel carModel);
    }
}
