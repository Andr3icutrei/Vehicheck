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
    public interface ICarService
    {
        Task<CarDto?> GetCarAsync(int id);
        Task<PagedResponse<CarDto>> GetCarsQueriedAsync(CarQueryRequestDto payload);
        Task<List<CarDto>> GetCarsAsync();
        Task<CarDto> AddCarAsync(AddCarRequest payload);
        Task<bool> DeleteCarAsync(int id);
        Task<CarDto> PatchCarAsync(PatchCarRequest payload); 
    }
}
