using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Mapping
{
    public static class ComponentMappingExtension
    {
        public static ComponentDto ToDto(this Component self)
        {
            return new ComponentDto
            {
                Id = self.Id,
                Name = self.Name,
                Price = self.Price,
                Manufacturer = self.Manufacturer.Name,
            };
        }
    }
}
