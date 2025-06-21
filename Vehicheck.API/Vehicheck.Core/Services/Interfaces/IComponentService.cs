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
    public interface IComponentService
    {
        Task<ComponentDto?> GetComponentAsync(int id);
        Task<PagedResponse<ComponentDto>> GetComponentsQueriedAsync(ComponentQueryRequestDto payload);
        Task<List<ComponentDto>> GetComponentsAsync();
        Task<ComponentDto> AddComponentAsync(AddComponentRequest payload);
        Task<bool> DeleteComponentAsync(int id);
        Task<ComponentDto> PatchComponentAsync(PatchComponentRequest payload); 
    }
}
