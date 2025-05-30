using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface ICarModelService
    {
        Task<GetCarModelDto?> GetCarModelAsync(int id);
        Task<List<GetCarModelDto>> GetCarModelsAsync();
        Task AddCarModelAsync(AddCarModelRequest payload);
    }
}
