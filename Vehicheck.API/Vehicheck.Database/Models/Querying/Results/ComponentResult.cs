using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Database.Models.Querying.Results
{
    public class ComponentResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public static ComponentResult ToResult(Component self)
        {
            return new ComponentResult
            {
                Id = self.Id,
                Name = self.Name,
                Price = self.Price,
            };
        }
    }
}
