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
        private readonly ICarModelRepository _repository;

        public CarModelService(ICarModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<CarModel> AddCarModelAsync(AddCarModelRequest payload)
        {
            var result = await _repository.AddCarModelAsync(payload.ToEntity());
            return result;
        }

        public async Task<CarModelDto?> GetCarModelAsync(int id)
        {
            var result =  await _repository.GetCarModelAsync(id);

            if (result == null)
            {
                throw new EntityNotFoundException("CarModel", id);
            }

            return result?.ToDto();
        }

        public async Task<List<CarModelDto>> GetCarModelsAsync()
        {
            var result = await _repository.GetAllCarModelsAsync();
            return result.Select(cm => cm.ToDto()).ToList();
        }

        public async Task<bool> DeleteCarModelAsync(int id)
        {
            var carModel = await _repository.GetFirstOrDefaultAsync(id);
            if (carModel == null)
            {
                throw new EntityNotFoundException("CarModel", id);
            }
            return await _repository.DeleteCarModelAsync(id);
        }

        public async Task<CarModelDto> PatchCarModelAsync(PatchCarModelRequest payload)
        {
            CarModel? carModel = await _repository.GetCarModelAsync(payload.Id);

            if (carModel == null)
            {
                throw new EntityNotFoundException("CarModel", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchCarModelRequest, CarModel>(carModel, payload);

            carModel.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return carModel.ToDto();
        }

        public async Task<PagedResponse<CarModelDto>> GetCarModelsQueryiedAsync(CarModelQueryRequestDto payload)
        {
            PagedResult<CarModelResult> result = await _repository.GetCarModelQueryiedAsync(payload.ToQueryingFilter());
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
