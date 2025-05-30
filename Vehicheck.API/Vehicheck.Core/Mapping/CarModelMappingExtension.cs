using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Mapping
{
    public static class CarModelMappingExtension
    {
        public static GetCarModelDto ToDto(this CarModel self)
        {
            return new GetCarModelDto
            {
                Name = self.Name,
                ReleaseYear = self.ReleaseYear,
                Manufacturer = self.Manufacturer!.ToDto(),
            };
        }
    }
}
