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
    public interface ICarManufacturerService
    {
        Task<CarManufacturer> AddCarManufacturerAsync(AddCarManufacturerRequest payload);
        Task<CarManufacturerDto?> GetCarManufacturerAsync(int id);
        Task<List<CarManufacturerDto>> GetAllCarManufacturersAsync();
        Task<PagedResponse<CarManufacturerDto>> GetCarManufacturersQueriedAsync(CarManufacturerQueryRequestDto payload);
        Task<bool> DeleteCarManufacturerAsync(int id);
        Task<CarManufacturerDto> PatchCarManufacturerAsync(PatchCarManufacturerRequest payload);
    }
}
