using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Patch
{
    public class PatchCarModelRequest
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        public int? ReleaseYear { get; set; }

        public int? CarManufacturerId { get; set; }
    }
}
