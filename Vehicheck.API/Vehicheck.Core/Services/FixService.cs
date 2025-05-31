using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Core.Services
{
    public class FixService : IFixService
    {
        private readonly IFixRepository _repository;

        public FixService(IFixRepository repository)
        {
            _repository = repository;
        }

        public async Task<Fix> AddFixAsync(AddFixRequest payload)
        {
            return await _repository.AddFixAsync(payload.ToEntity());
        }

        public async Task<GetFixDto?> GetFixAsync(int id)
        {
            var result = await _repository.GetFixAsync(id);
            return result.ToDto();
        }

        public async Task<List<GetFixDto>> GetFixesAsync()
        {
            var result = await _repository.GetAllFixesAsync();
            return result.Select(f => f.ToDto()).ToList();
        }
    }
}
