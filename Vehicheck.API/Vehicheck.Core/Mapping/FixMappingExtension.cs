using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Mapping
{
    public static class FixMappingExtension
    {
        public static GetFixDto ToDto(this Fix self)
        {
            return new GetFixDto
            { 
                Id = self.Id,
                Description = self.Description,
                Price = self.Price,
                Components = self.Components
                    .Where(cf => cf.DeletedAt == null && cf.Component != null)
                    .Select(cf => new GetComponentDto
                    {
                        Id = cf.Component!.Id,
                        Name = cf.Component!.Name,
                        Price = cf.Component!.Price,
                        Manufacturer = cf.Component!.Manufacturer!.ToDto()
                    }).ToList()
            };
        }
    }
}
