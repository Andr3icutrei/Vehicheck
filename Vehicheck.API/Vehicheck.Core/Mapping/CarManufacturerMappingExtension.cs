using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Mapping
{
    public static class CarManufacturerMappingExtension
    {
        public static GetCarManufacturerDto ToDto(this CarManufacturer self)
        {
            return new GetCarManufacturerDto
            {
                Id = self.Id,
                Name = self.Name,
            };
        }
    }
}
