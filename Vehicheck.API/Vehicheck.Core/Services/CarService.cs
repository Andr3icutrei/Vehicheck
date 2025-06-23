using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICarModelRepository _carModelRepository;
        private readonly ICarManufacturerRepository _carManufacturerRepository;

        public CarService(ICarRepository carRepository, 
                            IUserRepository userRepository,
                            ICarModelRepository carModelRepository,
                            ICarManufacturerRepository carManufacturerRepository)
        {
            _carRepository = carRepository;
            _userRepository = userRepository;
            _carModelRepository = carModelRepository;
            _carManufacturerRepository = carManufacturerRepository;
        }

        public async Task<CarDto> AddCarAsync(AddCarRequest payload)
        {
            var user = await _userRepository.GetUserAsync(payload.UserId);
            if (user == null)
            { 
                throw new EntityNotFoundException(nameof(payload.UserId), payload.UserId); 
            }

            var carModel = await _carModelRepository.GetCarModelAsync(payload.CarModelId);
            if (carModel == null)
            { 
                throw new EntityNotFoundException(nameof(payload.CarModelId), payload.CarModelId);
            }
            
            var carManufacturer = await _carManufacturerRepository.GetCarManufacturerAsync(payload.CarManufacturerId);
            if (carManufacturer == null)
            { 
                throw new EntityNotFoundException(nameof(payload.CarManufacturerId), payload.CarManufacturerId); 
            }

            return (await _carRepository.AddCarAsync(payload.ToEntity())).ToDto();
        }

        public async Task<CarDto?> GetCarAsync(int id)
        {
            var result = await _carRepository.GetCarAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("Car", id);
            }

            return result.ToDto();
        }

        public async Task<List<CarDto>> GetCarsAsync()
        {
            var result = await _carRepository.GetAllCarsAsync();
            return result.Select(c => c.ToDto()).ToList();
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await _carRepository.GetFirstOrDefaultAsync(id);
            if (car == null)
            {
                throw new EntityNotFoundException("Car", id);
            }
            return await _carRepository.DeleteCarAsync(id);
        }

        public async Task<CarDto> PatchCarAsync(PatchCarRequest payload)
        {
            Car? car = await _carRepository.GetCarAsync(payload.Id);
            if (car == null)
            {
                throw new EntityNotFoundException("Car", payload.Id);
            }

            PatchRequestToEntity.PatchFrom<PatchCarRequest, Car>(car, payload);
            car.ModifiedAt = DateTime.UtcNow;
            await _carRepository.SaveChangesAsync();

            var relatedData = (await _carRepository.GetAllCarsAsync())
                .Where(c => c.Id == car.Id)
                .Select(c => new
                {
                    CarManufacturer = c.CarManufacturer,
                    CarModel = c.CarModel,
                    CarModelManufacturer = c.CarModel.Manufacturer
                }).FirstOrDefault();

            car.CarManufacturer = relatedData?.CarManufacturer;
            if (car.CarModel != null && relatedData?.CarModel != null)
            {
                car.CarModel.Manufacturer = relatedData.CarModelManufacturer;
            }

            return car.ToDto();
        }

        public async Task<PagedResponse<CarDto>> GetCarsQueriedAsync(CarQueryRequestDto payload)
        {
            PagedResult<CarResult> result = await _carRepository.GetCarsQueriedAsync(payload.ToQueryingFilter());
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
