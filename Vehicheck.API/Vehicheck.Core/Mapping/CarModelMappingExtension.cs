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
    public static class CarModelMappingExtension
    {
        public static CarModelDto ToDto(this CarModel self)
        {
            return new CarModelDto
            {
                Id = self.Id,
                Name = self.Name,
                ReleaseYear = self.ReleaseYear,
                Manufacturer = self.Manufacturer.Name,
            };
        }
    }
}
