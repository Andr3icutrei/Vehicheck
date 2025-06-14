using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface IComponentManufacturerService
    {
        Task<GetComponentManufacturerDto?> GetComponentManufacturerAsync(int id);
        Task<List<GetComponentManufacturerDto>> GetComponentManufacturesAsync();
        Task<ComponentManufacturer> AddComponentManufactureAsync(AddComponentManufacturerRequest payload);
        Task<bool> DeleteComponentManufacturerAsync(int id);
        Task<GetComponentManufacturerDto> PatchComponentManufacturerAsync(PatchComponentManufacturerRequest payload);
    }
}
