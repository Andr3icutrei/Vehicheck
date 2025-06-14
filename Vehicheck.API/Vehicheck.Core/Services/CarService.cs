using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
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

        public async Task<GetCarDto?> GetCarAsync(int id)
        {
            var result = await _repository.GetCarAsync(id);

            if(result == null)
            {
                throw new EntityNotFoundException("Car", id);
            }

            return result.ToDto();
        }

        public async Task<List<GetCarDto>> GetCarsAsync()
        {
            var result = await _repository.GetAllCarsAsync();
            return result.Select(c => c.ToDto()).ToList();
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            return await _repository.DeleteCarAsync(id);
        }

        public async Task<GetCarDto> PatchCarAsync(PatchCarRequest payload)
        {
            Car? car = await _repository.GetCarAsync(payload.Id);
            PatchRequestToEntity.PatchFrom<PatchCarRequest, Car>(car, payload);

            car.ModifiedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return car.ToDto();
        }
    }
}
