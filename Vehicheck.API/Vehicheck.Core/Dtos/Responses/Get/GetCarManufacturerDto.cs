using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class GetCarManufacturerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetCarModelDto> Models { get; set; } = new List<GetCarModelDto>();
    }
}
