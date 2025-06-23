using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Dtos.Responses.Get.Querying;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface ICarModelService
    {
        Task<CarModelDto?> GetCarModelAsync(int id);
        Task<PagedResponse<CarModelDto>> GetCarModelsQueryiedAsync(CarModelQueryRequestDto payload);
        Task<List<CarModelDto>> GetCarModelsAsync();
        Task<CarModelDto> AddCarModelAsync(AddCarModelRequest payload);
        Task<bool> DeleteCarModelAsync(int id);
        Task<CarModelDto> PatchCarModelAsync(PatchCarModelRequest payload);
    }
}
