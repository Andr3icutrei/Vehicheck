using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;
using Vehicheck.Database.Models.Querying.Results;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class ComponentManufacturerDto
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public int YearOfFounding { get; set; }
        public List<ComponentDto> Components { get; set; } = new List<ComponentDto>();

        public static ComponentManufacturerDto ToDto(ComponentManufacturerResult result)
        {
            return new ComponentManufacturerDto
            {
                Id = result.Id,
                Name = result.Name,
                YearOfFounding = result.YearOfFounding,
            };
        }
    }
}
