using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Post
{
    public class AddFixRequest
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Fix ToEntity()
        {
            return new Fix
            {
                Description = Description,
                Price = Price
            };
        }
    }
}
