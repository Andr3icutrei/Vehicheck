using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Repositories.Interfaces;

namespace Vehicheck.Core.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _repository;

        public ComponentService(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Component> AddComponentAsync(AddComponentRequest payload)
        {
            return await _repository.AddComponentAsync(payload.ToEntity());
        }

        public async Task<GetComponentDto?> GetComponentAsync(int id)
        {
            var result = await _repository.GetComponentAsync(id);
            return result.ToDto();
        }

        public async Task<List<GetComponentDto>> GetComponentsAsync()
        {
            var result = await _repository.GetAllComponentsAsync();
            return result.Select(c => c.ToDto()).ToList();
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            return await _repository.DeleteComponentAsync(id);
        }

    }
}
