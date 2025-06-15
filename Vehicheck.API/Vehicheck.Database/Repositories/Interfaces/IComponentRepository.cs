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
    public interface IComponentRepository : IBaseRepository<Component>
    {
        Task<Component?> GetComponentAsync(int componentId);
        Task<List<Component>> GetAllComponentsAsync();
        Task<PagedResult<ComponentResult>> GetAllComponentsQueriedAsync(ComponentQueryingFilter payload);
        Task<Component> AddComponentAsync(Component component);
        Task<Component> UpdateComponentAsync(Component component);
        Task<bool> DeleteComponentAsync(int id);
    }
}
