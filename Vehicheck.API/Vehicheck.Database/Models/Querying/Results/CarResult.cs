using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Results
{
    public class CarResult
    {
        public int Id { get; set; }
        public int YearOfManufacture { get; set; }
        public int CarMileage { get; set; }
        public string CarModel { get; set; }
        public string CarManufacturer { get; set; }
        public List<string> Components { get; set; }

        public static CarResult ToResult(Car self)
        {
            return new CarResult
            {
                Id = self.Id,
                YearOfManufacture = self.YearOfManufacture,
                CarMileage = self.CarMileage,
                CarManufacturer = self.CarManufacturer.Name,
                CarModel = self.CarModel.Name,
                Components = self.CarModel.Components.Select(c => c.Component.Name).ToList()
            };
        }
    }
}
