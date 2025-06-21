using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Results
{
    public class CarModelResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public string Manufacturer { get; set; }

        public static CarModelResult ToResult(CarModel self)
        {
            return new CarModelResult
            {
                Id = self.Id,
                Name = self.Name,
                ReleaseYear = self.ReleaseYear,
                Manufacturer = self.Manufacturer.Name
            };
        }
    }
}
