using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Responses.Get
{
    public class GetCarDto
    {
        public int Id { get; set; }
        public int YearOfManufacture { get; set; }
        public int CarMileage { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string CarModel { get; set; }
        public string CarManufacturer { get; set; } 
        public List<GetComponentDto> Components { get; set; } = new List<GetComponentDto>();
    }
}
