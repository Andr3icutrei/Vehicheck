using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Services;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Mapping
{
    public static class ComponentManufacturerMappingExtension
    {
        public static GetComponentManufacturerDto ToDto(this ComponentManufacturer self)
        {
            return new GetComponentManufacturerDto
            {
                Id = self.Id,
                Name = self.Name,
                YearOfFounding = self.YearOfFounding,
                Components = self.Components.Select(c => c.ToDto()).ToList()
            };
        }
    }
}
