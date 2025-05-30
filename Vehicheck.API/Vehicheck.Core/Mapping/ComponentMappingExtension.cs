using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Mapping
{
    public static class ComponentMappingExtension
    {
        public static GetComponentDto ToDto(this Component self)
        {
            return new GetComponentDto
            {
                Id = self.Id,
                Name = self.Name,
                Price = self.Price,
                Cars = self.Cars
                .Where(cc => cc.DeletedAt == null && cc.Car != null)
                .Select(cc => new GetCarDto
                {
                    Id = cc.Car!.Id,
                    YearOfManufacture = cc.Car.YearOfManufacture,
                    CarModel = cc.Car.CarModel!.ToDto(),
                    CarManufacturer = cc.Car.CarManufacturer!.ToDto()
                }).ToList()
            };
        }
    }
}
