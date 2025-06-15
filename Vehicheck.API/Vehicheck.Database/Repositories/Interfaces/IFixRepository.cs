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
    public interface IFixRepository : IBaseRepository<Fix>
    {
        Task<Fix> AddFixAsync(Fix fix);
        Task<PagedResult<FixResult>> GetFixesQueryiedAsync(FixQueryingFilter payload);
        Task<Fix?> GetFixAsync(int id);
        Task<List<Fix>> GetAllFixesAsync();
        Task<bool> DeleteFixAsync(int id);
    }
}
