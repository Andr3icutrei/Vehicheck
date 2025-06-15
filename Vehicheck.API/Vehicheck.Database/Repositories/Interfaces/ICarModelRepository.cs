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
    public interface ICarModelRepository : IBaseRepository<CarModel>
    {
        Task<CarModel?> GetCarModelAsync(int modelId);
        Task<PagedResult<CarModelResult>> GetCarModelQueryiedAsync(CarModelQueryingFilter payload);
        Task<List<CarModel>> GetAllCarModelsAsync();
        Task<CarModel> AddCarModelAsync(CarModel carModel);
        Task<CarModel> UpdateCarModelAsync(CarModel carModel);
        Task<bool> DeleteCarModelAsync(int id);
    }
}
