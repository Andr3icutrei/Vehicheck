using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface IComponentService
    {
        Task<GetComponentDto?> GetComponentAsync(int id);
        Task<List<GetComponentDto>> GetComponentsAsync();
        Task<Component> AddComponentAsync(AddComponentRequest payload);
        Task<bool> DeleteComponentAsync(int id);
    }
}
