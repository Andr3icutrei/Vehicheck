using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Post
{
    public class AddCarModelRequest
    {
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public int CarManufacturerId { get; set; }

        public CarModel ToEntity()
        {
            return new CarModel
            {
                Name = Name,
                ReleaseYear = ReleaseYear,
                CarManufacturerId = CarManufacturerId
            };
        }
    }
}
