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

namespace Vehicheck.Core.Services
{
    public class CarManufacturerService : ICarManufacturerService
    {
        private readonly ICarManufacturerRepository _repository;

        public CarManufacturerService(ICarManufacturerRepository repository)
        {
            _repository = repository;
        }

        public async Task AddCarManufacturerAsync(AddCarManufacturerRequest payload)
        {
            await _repository.AddCarManufacturerAsync(payload.ToEntity());
        }

        public async Task<GetCarManufacturerDto?> GetCarManufacturerAsync(int id)
        {
            var carManufacturer = await _repository.GetCarManufacturerAsync(id);
            return carManufacturer?.ToDto();
        }

        public async Task<List<GetCarManufacturerDto>> GetCarManufacturersAsync()
        {
            List<GetCarManufacturerDto> toReturn = new List<GetCarManufacturerDto>();
            foreach(var carManufacturer in await _repository.GetCarManufacturersAsync())
            {
                toReturn.Add(carManufacturer.ToDto());  
            }
            return toReturn;
        }

    }
}
