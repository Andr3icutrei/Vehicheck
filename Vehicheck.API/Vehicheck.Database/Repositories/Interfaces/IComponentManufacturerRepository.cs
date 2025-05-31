using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface IComponentManufacturerRepository : IBaseRepository<ComponentManufacturer>
    {
        Task<ComponentManufacturer> AddComponentAsync(ComponentManufacturer componentManufacturer);
        Task<ComponentManufacturer?> GetComponentManufacturerAsync(int id);
        Task<List<ComponentManufacturer>> GetAllComponentsAsync();
        Task<bool> DeleteComponentManufacturerAsync(int id);
    }
}
