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
    public class CarModelDto
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string Manufacturer { get; set; }

        public static CarModelDto ToDto(CarModelResult result)
        {
            return new CarModelDto
            {
                Id = result.Id,
                Name = result.Name,
                ReleaseYear = result.ReleaseYear,
                Manufacturer = result.Manufacturer
            };
        }
    }
}
