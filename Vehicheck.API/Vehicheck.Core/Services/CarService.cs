using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Mapping;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Repositories.Interfaces;

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
            return result.ToDto();
        }

        public async Task<List<GetCarDto>> GetCarsAsync()
        {
            var result = await _repository.GetAllCarsAsync();
            return result.Select(c => c.ToDto()).ToList();
        }
    }
}
