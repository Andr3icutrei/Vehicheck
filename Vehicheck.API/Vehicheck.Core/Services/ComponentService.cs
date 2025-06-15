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

        public async Task<ComponentDto?> GetComponentAsync(int id)
        {
            var result = await _repository.GetComponentAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("Component", id);
            }

            return result.ToDto();
        }

        public async Task<List<ComponentDto>> GetComponentsAsync()
        {
            var result = await _repository.GetAllComponentsAsync();
            return result.Select(c => c.ToDto()).ToList();
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            var component = await _repository.GetFirstOrDefaultAsync(id);
            if (component == null)
            {
                throw new EntityNotFoundException("Component", id);
            }
            return await _repository.DeleteComponentAsync(id);
        }

        public async Task<ComponentDto> PatchComponentAsync(PatchComponentRequest payload)
        {
            Component? component = await _repository.GetComponentAsync(payload.Id);

            if (component == null)
            {
                throw new EntityNotFoundException("Component", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchComponentRequest, Component>(component, payload);
            
            component.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();
            return component.ToDto();
        }

        public async Task<PagedResponse<ComponentDto>> GetComponentsQueriedAsync(ComponentQueryRequestDto payload)
        {
            PagedResult<ComponentResult> result = await _repository.GetAllComponentsQueriedAsync(payload.ToQueryingFilter());
            return new PagedResponse<ComponentDto>
            {
                Data = result.Data.Select(c => ComponentDto.ToDto(c)),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
            };
        }
    }
}
