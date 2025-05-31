using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Repositories.Interfaces
{
    public interface IFixRepository : IBaseRepository<Fix>
    {
        Task<Fix> AddFixAsync(Fix fix);
        Task<Fix?> GetFixAsync(int id);
        Task<List<Fix>> GetAllFixesAsync();
        Task<bool> DeleteFixAsync(int id);
    }
}
