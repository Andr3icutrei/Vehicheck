using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Results
{
    public class CarManufacturerResult
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static CarManufacturerResult ToResult(CarManufacturer self)
        {
            return new CarManufacturerResult
            {
                Id = self.Id,
                Name = self.Name,
            };
        }
    }
}
