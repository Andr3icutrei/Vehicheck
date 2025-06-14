using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Database.Entities;

namespace Vehicheck.Core.Dtos.Requests.Patch
{
    public class PatchCarManufacturerRequest
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
    }
}
