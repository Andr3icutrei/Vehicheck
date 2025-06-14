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
    public class ComponentManufacturerService : IComponentManufacturerService
    {
        private readonly IComponentManufacturerRepository _repository;

        public ComponentManufacturerService(IComponentManufacturerRepository repository)
        {
            _repository = repository;
        }

        public async Task<ComponentManufacturer> AddComponentManufactureAsync(AddComponentManufacturerRequest payload)
        {
            return await _repository.AddComponentAsync(payload.ToEntity());
        }

        public async Task<GetComponentManufacturerDto?> GetComponentManufacturerAsync(int id)
        {
            var result = await _repository.GetComponentManufacturerAsync(id);
            return result.ToDto();
        }

        public async Task<List<GetComponentManufacturerDto>> GetComponentManufacturesAsync()
        {
            var result = await _repository.GetAllComponentsAsync();
            return result.Select(cm => cm.ToDto()).ToList();
        }

        public async Task<bool> DeleteComponentManufacturerAsync(int id)
        {
            return await _repository.DeleteComponentManufacturerAsync(id);
        }
    }
}
