using dTax.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Statuses
{
    public class StatusEntity : BaseEntity
    {
        public string StatusName { get; set; }
        public int StatusNumber { get; set; }
    }
}
