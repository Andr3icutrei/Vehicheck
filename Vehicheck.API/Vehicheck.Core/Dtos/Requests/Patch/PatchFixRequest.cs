using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Core.Dtos.Requests.Patch
{
    public class PatchFixRequest
    {
        public int Id {  get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [Precision(10, 2)]
        public decimal? Price { get; set; }
        public List<int>? PossibleComponentsToFixIds { get; set; }
    }
}
