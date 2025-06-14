using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Post
{
    public class AddCarManufacturerRequest
    {
        public string Name { get; set; }

        public CarManufacturer ToEntity()
        {
            return new CarManufacturer
            {
                Name = Name
            };
        }
    }
}
