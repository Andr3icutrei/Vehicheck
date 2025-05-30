using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Mapping
{
    public static class CarMappingExtension
    {
        public static GetCarDto ToDto(this Car self)
        {
            return new GetCarDto
            {
                Id = self.Id,
                YearOfManufacture = self.YearOfManufacture,
                CarMileage = self.CarMileage,
                User = self.User!.ToDto(),
                CarManufacturer = self.CarManufacturer!.ToDto(),
                CarModel = self.CarModel!.ToDto(),
                Components = self.Components
                    .Where(cc => cc.DeletedAt == null && cc.Component != null)
                    .Select(cc => new GetComponentDto
                    {
                        Id = cc.Component!.Id,
                        Name = cc.Component.Name,
                        Price = cc.Component.Price
                    }).ToList(),
            };
        }
    }
}
