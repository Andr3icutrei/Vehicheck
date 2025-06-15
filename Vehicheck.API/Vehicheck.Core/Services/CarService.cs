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
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;

        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<Car> AddCarAsync(AddCarRequest payload)
        {
            return await _repository.AddCarAsync(payload.ToEntity());
        }

        public async Task<CarDto?> GetCarAsync(int id)
        {
            var result = await _repository.GetCarAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("Car", id);
            }

            return result.ToDto();
        }

        public async Task<List<CarDto>> GetCarsAsync()
        {
            var result = await _repository.GetAllCarsAsync();
            return result.Select(c => c.ToDto()).ToList();
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await _repository.GetFirstOrDefaultAsync(id);
            if (car == null)
            {
                throw new EntityNotFoundException("Car", id);
            }
            return await _repository.DeleteCarAsync(id);
        }

        public async Task<CarDto> PatchCarAsync(PatchCarRequest payload)
        {
            Car? car = await _repository.GetCarAsync(payload.Id);

            if (car == null)
            {
                throw new EntityNotFoundException("Car", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchCarRequest, Car>(car, payload);

            car.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return car.ToDto();
        }

        public async Task<PagedResponse<CarDto>> GetCarsQueriedAsync(CarQueryRequestDto payload)
        {
            PagedResult<CarResult> result = await _repository.GetCarsQueriedAsync(payload.ToQueryingFilter());
            return new PagedResponse<CarDto>
            {
                Data = result.Data.Select(c => CarDto.ToDto(c)),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
            };
        }
    }
}
