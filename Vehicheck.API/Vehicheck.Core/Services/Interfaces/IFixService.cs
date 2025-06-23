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
    public interface IFixService
    {
        Task<FixDto?> GetFixAsync(int id);
        Task<PagedResponse<FixDto>> GetFixesQueryiedAsync(FixQueryRequestDto payload);
        Task<List<FixDto>> GetFixesAsync();
        Task<FixDto> AddFixAsync(AddFixRequest payload);
        Task<bool> DeleteFixAsync(int id);
        Task<FixDto> PatchFixAsync(PatchFixRequest payload);
    }
}
