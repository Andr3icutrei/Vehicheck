using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Repositories.Interfaces;
using Vehicheck.Core.Mapping;
using Vehicheck.Database.Entities;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Database.PatchHelpers;
using Vehicheck.Infrastructure.Exceptions;
using Vehicheck.Core.Dtos.Responses.Get.Querying;
using Vehicheck.Database.Models.Querying.Results;
using Vehicheck.Database.Models.Querying.Filters;

namespace Vehicheck.Core.Services
{
    public class CarManufacturerService : ICarManufacturerService
    {
        private readonly ICarManufacturerRepository _repository;

        public CarManufacturerService(ICarManufacturerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CarManufacturerDto> AddCarManufacturerAsync(AddCarManufacturerRequest payload)
        {
            var result = await _repository.AddCarManufacturerAsync(payload.ToEntity());
            return result.ToDto();
        }

        public async Task<CarManufacturerDto?> GetCarManufacturerAsync(int id)
        {
            var carManufacturer = await _repository.GetCarManufacturerAsync(id);

            if(carManufacturer == null)
            {
                throw new EntityNotFoundException("CarManufacturer", id);
            }

            return carManufacturer?.ToDto();
        }

        public async Task<List<CarManufacturerDto>> GetAllCarManufacturersAsync()
        {
            List<CarManufacturerDto> toReturn = new List<CarManufacturerDto>();
            foreach(var carManufacturer in await _repository.GetCarManufacturersAsync())
            {
                toReturn.Add(carManufacturer.ToDto());  
            }

            return toReturn;
        }

        public async Task<bool> DeleteCarManufacturerAsync(int id)
        {
            var carManufacturer = await _repository.GetCarManufacturerAsync(id);
            if (carManufacturer == null)
            {
                throw new EntityNotFoundException("CarManufacturer", id);
            }

            return await _repository.DeleteCarManufacturerAsync(id);
        }

        public async Task<CarManufacturerDto> PatchCarManufacturerAsync(PatchCarManufacturerRequest payload)
        {
            CarManufacturer? carManufacturer = await _repository.GetCarManufacturerAsync(payload.Id);

            if (carManufacturer == null)
            {
                throw new EntityNotFoundException("CarManufacturer", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchCarManufacturerRequest, CarManufacturer>(carManufacturer, payload);

            carManufacturer.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return carManufacturer.ToDto();
        }

        public async Task<PagedResponse<CarManufacturerDto>> GetCarManufacturersQueriedAsync(CarManufacturerQueryRequestDto payload)
        {
            PagedResult<CarManufacturerResult> result = await _repository.GetCarManufacturersQueriedAsync(payload.ToQueryingFilter());
            return new PagedResponse<CarManufacturerDto>
            {
                Data = result.Data.Select(cm => CarManufacturerDto.ToDto(cm)),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
            };
        }
    }
}
