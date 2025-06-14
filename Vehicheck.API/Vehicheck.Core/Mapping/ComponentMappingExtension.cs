using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Mapping
{
    public static class ComponentMappingExtension
    {
        public static GetComponentDto ToDto(this Component self)
        {
            return new GetComponentDto
            {
                Id = self.Id,
                Name = self.Name,
                Price = self.Price,
            };
        }
    }
}
