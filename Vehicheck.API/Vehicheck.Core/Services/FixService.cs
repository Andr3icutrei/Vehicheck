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
        private readonly IFixRepository _repository;

        public FixService(IFixRepository repository)
        {
            _repository = repository;
        }

        public async Task<Fix> AddFixAsync(AddFixRequest payload)
        {
            return await _repository.AddFixAsync(payload.ToEntity());
        }

        public async Task<FixDto?> GetFixAsync(int id)
        {
            var result = await _repository.GetFixAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("Fix", id);
            }

            return result.ToDto();
        }

        public async Task<List<FixDto>> GetFixesAsync()
        {
            var result = await _repository.GetAllFixesAsync();
            return result.Select(f => f.ToDto()).ToList();
        }
        public async Task<bool> DeleteFixAsync(int id)
        {
            var fix = await _repository.GetFirstOrDefaultAsync(id);
            if (fix == null)
            {
                throw new EntityNotFoundException("Fix", id);
            }
            return await _repository.DeleteFixAsync(id);
        }

        public async Task<FixDto> PatchFixAsync(PatchFixRequest payload)
        {
            Fix? fix = await _repository.GetFixAsync(payload.Id);

            if (fix == null)
            {
                throw new EntityNotFoundException("Fix", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchFixRequest, Fix>(fix, payload);

            fix.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return fix.ToDto();
        }

        public async Task<PagedResponse<FixDto>> GetFixesQueryiedAsync(FixQueryRequestDto payload)
        {
            PagedResult<FixResult> result = await _repository.GetFixesQueryiedAsync(payload.ToQueryingFilter());
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
