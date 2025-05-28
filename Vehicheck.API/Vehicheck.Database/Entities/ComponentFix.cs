using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicheck.Database.Entities
{
    public class ComponentFix : BaseEntity
    {
        public int MillageAtFix {  get; set; }

        //navigation properties
        public int? FixId { get; set; }
        public Fix? Fix { get; set; }

        public int? ComponentId { get; set; }
        public Component? Component { get; set; }
    }
}
