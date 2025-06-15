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

namespace Vehicheck.Core.Services
{
    public class CarManufacturerService : ICarManufacturerService
    {
        private readonly ICarManufacturerRepository _repository;

        public CarManufacturerService(ICarManufacturerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CarManufacturer> AddCarManufacturerAsync(AddCarManufacturerRequest payload)
        {
            var result = await _repository.AddCarManufacturerAsync(payload.ToEntity());
            return result;
        }

        public async Task<GetCarManufacturerDto?> GetCarManufacturerAsync(int id)
        {
            var carManufacturer = await _repository.GetCarManufacturerAsync(id);

            if(carManufacturer == null)
            {
                throw new EntityNotFoundException("CarManufacturer", id);
            }

            return carManufacturer?.ToDto();
        }

        public async Task<List<GetCarManufacturerDto>> GetAllCarManufacturersAsync()
        {
            List<GetCarManufacturerDto> toReturn = new List<GetCarManufacturerDto>();
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

        public async Task<GetCarManufacturerDto> PatchCarManufacturerAsync(PatchCarManufacturerRequest payload)
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
    }
}
