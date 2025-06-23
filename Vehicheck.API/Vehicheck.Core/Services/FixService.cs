using Microsoft.EntityFrameworkCore;
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
    public class FixService : IFixService
    {
        private readonly IFixRepository _fixRepository;
        private readonly IComponentRepository _componentRepository;

        public FixService(IFixRepository fixRepository, IComponentRepository componentRepository)
        {
            _fixRepository = fixRepository;
            _componentRepository = componentRepository;
        }

        public async Task<FixDto> AddFixAsync(AddFixRequest payload)
        {
            var components = await _componentRepository.GetAllComponentsAsync();
            foreach (var component in payload.PossibleComponentsToFixIds)
            {
                if (components.Any(c => c.Id == component) == false)
                    throw new EntityNotFoundException("Fix Id:", component);
            }

            return (await _fixRepository.AddFixAsync(payload.ToEntity())).ToDto();
        }

        public async Task<FixDto?> GetFixAsync(int id)
        {
            var result = await _fixRepository.GetFixAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("Fix", id);
            }

            return result.ToDto();
        }

        public async Task<List<FixDto>> GetFixesAsync()
        {
            var result = await _fixRepository.GetAllFixesAsync();
            return result.Select(f => f.ToDto()).ToList();
        }

        public async Task<bool> DeleteFixAsync(int id)
        {
            var fix = await _fixRepository.GetFirstOrDefaultAsync(id);
            if (fix == null)
            {
                throw new EntityNotFoundException("Fix", id);
            }
            return await _fixRepository.DeleteFixAsync(id);
        }

        public async Task<FixDto> PatchFixAsync(PatchFixRequest payload)
        {
            Fix? fix = await _fixRepository.GetFixAsync(payload.Id);
            if (fix == null)
            {
                throw new EntityNotFoundException("Fix", payload.Id);
            }

            if (payload.PossibleComponentsToFixIds != null)
            {
                var allComponents = await _componentRepository.GetAllComponentsAsync();

                foreach (var existingComponentFix in fix.Components.Where(cf => cf.DeletedAt == null))
                {
                    existingComponentFix.DeletedAt = DateTime.UtcNow;
                    existingComponentFix.ModifiedAt = DateTime.UtcNow;
                }

                foreach (var componentId in payload.PossibleComponentsToFixIds)
                {
                    if (!allComponents.Any(c => c.Id == componentId))
                        throw new EntityNotFoundException("Component Id:", componentId);

                    var existingRelation = fix.Components
                        .FirstOrDefault(cf => cf.ComponentId == componentId);

                    if (existingRelation != null)
                    {
                        existingRelation.DeletedAt = null;
                        existingRelation.ModifiedAt = null;
                    }
                    else
                    {
                        fix.Components.Add(new ComponentFix
                        {
                            ComponentId = componentId,
                            FixId = fix.Id
                        });
                    }
                }
            }

            PatchRequestToEntity.PatchFrom<PatchFixRequest, Fix>(fix, payload);
            fix.ModifiedAt = DateTime.UtcNow;
            await _fixRepository.SaveChangesAsync();
            return fix.ToDto();
        }

        public async Task<PagedResponse<FixDto>> GetFixesQueryiedAsync(FixQueryRequestDto payload)
        {
            PagedResult<FixResult> result = await _fixRepository.GetFixesQueryiedAsync(payload.ToQueryingFilter());
            return new PagedResponse<FixDto>
            {
                Data = result.Data.Select(f => FixDto.ToDto(f)),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
            };
        }
    }
}
