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
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _componentRepository;
        private readonly IComponentManufacturerRepository _manufacturerRepository;
        private readonly ICarModelRepository _carModelRepository;

        public ComponentService(IComponentRepository componentRepository,
                                IComponentManufacturerRepository componentManufacturerRepository,
                                ICarModelRepository carModelRepository)
        {
            _componentRepository = componentRepository;
            _manufacturerRepository = componentManufacturerRepository;
            _carModelRepository = carModelRepository;
        }

        public async Task<ComponentDto> AddComponentAsync(AddComponentRequest payload)
        {
            var componentManufacturer = await _manufacturerRepository.GetComponentManufacturerAsync(payload.ComponentManufacturerId);
            if(componentManufacturer == null)
            {
                throw new EntityNotFoundException(nameof(payload.ComponentManufacturerId), payload.ComponentManufacturerId);
            }

            var carModels = await _carModelRepository.GetAllAsync();
            foreach (int id in payload.CarModelIds)
            {
                if (carModels.Any(c => c.Id == id) == false)
                    throw new EntityNotFoundException(nameof(payload.CarModelIds), id);
            }

            return (await _componentRepository.AddComponentAsync(payload.ToEntity(), payload.CarModelIds)).ToDto();
        }

        public async Task<ComponentDto?> GetComponentAsync(int id)
        {
            var result = await _componentRepository.GetComponentAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("Component", id);
            }

            return result.ToDto();
        }

        public async Task<List<ComponentDto>> GetComponentsAsync()
        {
            var result = await _componentRepository.GetAllComponentsAsync();
            return result.Select(c => c.ToDto()).ToList();
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            var component = await _componentRepository.GetFirstOrDefaultAsync(id);
            if (component == null)
            {
                throw new EntityNotFoundException("Component", id);
            }
            return await _componentRepository.DeleteComponentAsync(id);
        }

        public async Task<ComponentDto> PatchComponentAsync(PatchComponentRequest payload)
        {
            Component? component = await _componentRepository.GetComponentAsync(payload.Id);
            if (component == null)
            {
                throw new EntityNotFoundException("Component", payload.Id);
            }

            if (payload.ComponentManufacturerId != null)
            {
                var componentManufacturer = await _manufacturerRepository.GetComponentManufacturerAsync((int)payload.ComponentManufacturerId);
                if (componentManufacturer == null)
                {
                    throw new EntityNotFoundException("ComponentManufacturer", payload.ComponentManufacturerId);
                }
            }

            if (payload.CarModelIds != null)
            {
                var allCarModels = await _carModelRepository.GetAllAsync();

                foreach (var existingModel in component.Models.Where(m => m.DeletedAt == null))
                {
                    existingModel.DeletedAt = DateTime.UtcNow;
                    existingModel.ModifiedAt = DateTime.UtcNow;
                }

                foreach (int carModelId in payload.CarModelIds)
                {
                    if (!allCarModels.Any(c => c.Id == carModelId))
                        throw new EntityNotFoundException("CarModel", carModelId);

                    var existingRelation = component.Models
                        .FirstOrDefault(m => m.CarModelId == carModelId);

                    if (existingRelation != null)
                    {
                        existingRelation.DeletedAt = null;
                        existingRelation.ModifiedAt = null;
                    }
                    else
                    {   
                        component.Models.Add(new CarModelComponent
                        {
                            ComponentId = component.Id,
                            CarModelId = carModelId,
                            DeletedAt = null
                        });
                    }
                }
            }

            PatchRequestToEntity.PatchFrom<PatchComponentRequest, Component>(component, payload);

            component.ModifiedAt = DateTime.UtcNow;
            await _componentRepository.SaveChangesAsync();
            return component.ToDto();
        }

        public async Task<PagedResponse<ComponentDto>> GetComponentsQueriedAsync(ComponentQueryRequestDto payload)
        {
            PagedResult<ComponentResult> result = await _componentRepository.GetAllComponentsQueriedAsync(payload.ToQueryingFilter());
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
