using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    internal interface IComponentRepository : IBaseRepository<Component>
    {
        Task<Component?> GetComponentId(int componentId);
        Task<List<Component>> GetAllComponent();
        Task<Component> AddComponentAsync(Component component);
        Task<Component> UpdateComponentAsync(Component component);

    }
}
