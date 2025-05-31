using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Repositories.Interfaces;
using Vehicheck.Core.Mapping;
using Vehicheck.Database.Entities;

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
            return await _repository.DeleteCarManufacturerAsync(id);
        }

    }
}
