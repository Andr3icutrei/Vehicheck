using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface ICarModelService
    {
        Task<GetCarModelDto?> GetCarModelAsync(int id);
        Task<List<GetCarModelDto>> GetCarModelsAsync();
        Task<CarModel> AddCarModelAsync(AddCarModelRequest payload);
        Task<bool> DeleteCarModelAsync(int id);
    }
}
