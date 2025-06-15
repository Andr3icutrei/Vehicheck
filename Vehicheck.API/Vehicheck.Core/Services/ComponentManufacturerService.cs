using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Dtos.Responses.Get.Querying;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Filters;
using Vehicheck.Database.Models.Querying.Results;
using Vehicheck.Database.PatchHelpers;
using Vehicheck.Database.Repositories.Interfaces;
using Vehicheck.Infrastructure.Exceptions;

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

        public async Task<ComponentManufacturerDto?> GetComponentManufacturerAsync(int id)
        {
            var result = await _repository.GetComponentManufacturerAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("ComponentManufacturer", id);
            }

            return result.ToDto();
        }

        public async Task<List<ComponentManufacturerDto>> GetComponentManufacturesAsync()
        {
            var result = await _repository.GetAllComponentsAsync();
            return result.Select(cm => cm.ToDto()).ToList();
        }

        public async Task<bool> DeleteComponentManufacturerAsync(int id)
        {
            var componentManufacturer = await _repository.GetFirstOrDefaultAsync(id);
            if (componentManufacturer == null)
            {
                throw new EntityNotFoundException("ComponentManufacturer", id);
            }
            return await _repository.DeleteComponentManufacturerAsync(id);
        }

        public async Task<ComponentManufacturerDto> PatchComponentManufacturerAsync(PatchComponentManufacturerRequest payload)
        {
            ComponentManufacturer? componentManufacturer = await _repository.GetComponentManufacturerAsync(payload.Id);

            if (componentManufacturer == null)
            {
                throw new EntityNotFoundException("ComponentManufacturer", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchComponentManufacturerRequest, ComponentManufacturer>(componentManufacturer, payload);
            
            componentManufacturer.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return componentManufacturer.ToDto();
        }

        public async Task<PagedResponse<ComponentManufacturerDto>> GetComponentManufacturerQueryiedAsync(ComponentManufacturerQueryRequestDto payload)
        {
            PagedResult<ComponentManufacturerResult> result = await _repository.GetComponentmanufacturersQueriedAsync(payload.ToQueryingFilter());
            return new PagedResponse<ComponentManufacturerDto>
            {
                Data = result.Data.Select(cm => ComponentManufacturerDto.ToDto(cm)),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
            };
        }
    }
}
