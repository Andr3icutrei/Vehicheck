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
    public interface ICarManufacturerService
    {
        Task<GetCarManufacturerDto?> GetCarManufacturerAsync(int id);
        Task<List<GetCarManufacturerDto>> GetAllCarManufacturersAsync();
        Task<CarManufacturer> AddCarManufacturerAsync(AddCarManufacturerRequest payload);
        Task<bool> DeleteCarManufacturerAsync(int id);
    }
}
