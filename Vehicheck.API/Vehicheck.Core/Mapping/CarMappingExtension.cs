using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Mapping
{
    public static class CarMappingExtension
    {
        public static CarDto ToDto(this Car self)
        {
            return new CarDto
            {
                Id = self.Id,
                YearOfManufacture = self.YearOfManufacture,
                CarMileage = self.CarMileage,
                CarManufacturer = self.CarManufacturer.Name,
                CarModel = self.CarModel.Name,
                Components = self.Components
                    .Where(cc => cc.DeletedAt == null && cc.Component != null)
                    .Select(cc => new ComponentDto
                    {
                        Id = cc.Component!.Id,
                        Name = cc.Component.Name,
                        Price = cc.Component.Price
                    }).ToList(),
            };
        }
    }
}
