using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface IFixService
    {
        Task<GetFixDto?> GetFixAsync(int id);
        Task<List<GetFixDto>> GetFixesAsync();
        Task<Fix> AddFixAsync(AddFixRequest payload);
        Task<bool> DeleteFixAsync(int id);
    }
}
