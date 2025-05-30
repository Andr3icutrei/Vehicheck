using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface IFixService
    {
        Task<GetFixDto?> GetFixAsync(int id);
        Task<List<GetFixDto>> GetFixesAsync();
        Task AddFixAsync(AddFixRequest payload);
    }
}
