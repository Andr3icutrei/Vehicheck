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
    public static class CarManufacturerMappingExtension
    {
        public static CarManufacturerDto ToDto(this CarManufacturer self)
        {
            return new CarManufacturerDto
            {
                Id = self.Id,
                Name = self.Name,
            };
        }
    }
}
