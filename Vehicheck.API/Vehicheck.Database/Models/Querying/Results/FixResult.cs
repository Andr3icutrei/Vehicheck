using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Results
{
    public class FixResult
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public List<string> FixableComponents { get; set; }

        public static FixResult ToResult(Fix self)
        {
            return new FixResult
            {
                Id = self.Id,
                Description = self.Description,
                Price = self.Price,
                FixableComponents = self.Components.Select(c => c.Component.Name + ", " + c.Component.Manufacturer.Name).ToList(),
            };
        }
    }
}
