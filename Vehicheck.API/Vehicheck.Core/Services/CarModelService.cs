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
    public class CarModelService : ICarModelService
    {
        private readonly ICarModelRepository _carModelRepository;
        private readonly ICarManufacturerRepository _manufacturerRepository;

        public CarModelService(ICarModelRepository carModelRepository, ICarManufacturerRepository carManufacturerRepository)
        {
            _carModelRepository = carModelRepository;
            _manufacturerRepository = carManufacturerRepository;
        }

        public async Task<CarModelDto> AddCarModelAsync(AddCarModelRequest payload)
        {
            var carManufacturer = await _manufacturerRepository.GetCarManufacturerAsync(payload.CarManufacturerId);
            if (carManufacturer == null)
            {
                throw new EntityNotFoundException(nameof(payload.CarManufacturerId), payload.CarManufacturerId);    
            }

            var result = await _carModelRepository.AddCarModelAsync(payload.ToEntity());
            return result.ToDto();
        }

        public async Task<CarModelDto?> GetCarModelAsync(int id)
        {
            var result =  await _carModelRepository.GetCarModelAsync(id);

            if (result == null)
            {
                throw new EntityNotFoundException("CarModel", id);
            }

            return result?.ToDto();
        }

        public async Task<List<CarModelDto>> GetCarModelsAsync()
        {
            var result = await _carModelRepository.GetAllCarModelsAsync();
            return result.Select(cm => cm.ToDto()).ToList();
        }

        public async Task<bool> DeleteCarModelAsync(int id)
        {
            var carModel = await _carModelRepository.GetFirstOrDefaultAsync(id);
            if (carModel == null)
            {
                throw new EntityNotFoundException("CarModel", id);
            }
            return await _carModelRepository.DeleteCarModelAsync(id);
        }

        public async Task<CarModelDto> PatchCarModelAsync(PatchCarModelRequest payload)
        {
            CarModel? carModel = await _carModelRepository.GetCarModelAsync(payload.Id);

            if (carModel == null)
            {
                throw new EntityNotFoundException("CarModel", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchCarModelRequest, CarModel>(carModel, payload);

            carModel.ModifiedAt = DateTime.UtcNow;

            await _carModelRepository.SaveChangesAsync();

            return carModel.ToDto();
        }

        public async Task<PagedResponse<CarModelDto>> GetCarModelsQueryiedAsync(CarModelQueryRequestDto payload)
        {
            PagedResult<CarModelResult> result = await _carModelRepository.GetCarModelQueryiedAsync(payload.ToQueryingFilter());
            return new PagedResponse<CarModelDto>
            {
                Data = result.Data.Select(cm => CarModelDto.ToDto(cm)),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
            };
        }
    }
}
