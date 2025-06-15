using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Results
{
    public class ComponentManufacturerResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfFounding { get; set; }

        public static ComponentManufacturerResult ToResult(ComponentManufacturer self)
        {
            return new ComponentManufacturerResult
            {
                Id = self.Id,
                Name = self.Name,
                YearOfFounding = self.YearOfFounding,
            };
        }
    }
}
