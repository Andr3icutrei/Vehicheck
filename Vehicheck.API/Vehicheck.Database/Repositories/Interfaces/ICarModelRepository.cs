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
        Task<CarModel?> GetCarModelId(int modelId);
        Task<List<CarModel>> GetAllCarModel();
        Task<CarModel> AddCarModelAsync(CarModel carModel);
        Task<CarModel> UpdateCarModelAsync(CarModel carModel);
    }
}
