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
    public static class FixMappingExtension
    {
        public static FixDto ToDto(this Fix self)
        {
            return new FixDto
            { 
                Id = self.Id,
                Description = self.Description,
                Price = self.Price,
                FixableComponents = self.Components.Select(c => c.Component.Name + ", " + c.Component.Manufacturer.Name).ToList()
            };
        }
    }
}
