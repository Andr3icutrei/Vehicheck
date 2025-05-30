using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Core.Dtos.Requests
{
    public class AddFixRequest
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
