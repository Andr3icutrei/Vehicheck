using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface IComponentRepository : IBaseRepository<Component>
    {
        Task<Component?> GetComponentAsync(int componentId);
        Task<List<Component>> GetAllComponentsAsync();
        Task<Component> AddComponentAsync(Component component);
        Task<Component> UpdateComponentAsync(Component component);
    }
}
