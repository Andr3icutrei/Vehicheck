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

        public async Task<GetCarModelDto?> GetCarModelAsync(int id)
        {
            var result =  await _repository.GetCarModelAsync(id);
            return result?.ToDto();
        }

        public async Task<List<GetCarModelDto>> GetCarModelsAsync()
        {
            var result = await _repository.GetAllCarModelsAsync();
            return result.Select(cm => cm.ToDto()).ToList();
        }

        public async Task<bool> DeleteCarModelAsync(int id)
        {
            return await _repository.DeleteCarModelAsync(id);
        }
    }
}
