using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface ICarService
    {
        Task<GetCarDto?> GetCarAsync(int id);
        Task<List<GetCarDto>> GetCarsAsync();
        Task<Car> AddCarAsync(AddCarRequest payload);
        Task<bool> DeleteCarAsync(int id);
    }
}
