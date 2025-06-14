using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Post
{
    public class AddComponentManufacturerRequest
    {
        public string Name { get; set; }
        public int YearOfFounding { get; set; }

        public ComponentManufacturer ToEntity()
        {
            return new ComponentManufacturer
            {
                Name = Name,
                YearOfFounding = YearOfFounding,
            };
        }
    }
}
