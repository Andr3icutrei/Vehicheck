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
    public interface IComponentManufacturerService
    {
        Task<ComponentManufacturerDto?> GetComponentManufacturerAsync(int id);
        Task<List<ComponentManufacturerDto>> GetComponentManufacturesAsync();
        Task<ComponentManufacturerDto> AddComponentManufactureAsync(AddComponentManufacturerRequest payload);
        Task<PagedResponse<ComponentManufacturerDto>> GetComponentManufacturerQueryiedAsync(ComponentManufacturerQueryRequestDto payload);
        Task<bool> DeleteComponentManufacturerAsync(int id);
        Task<ComponentManufacturerDto> PatchComponentManufacturerAsync(PatchComponentManufacturerRequest payload);
    }
}
