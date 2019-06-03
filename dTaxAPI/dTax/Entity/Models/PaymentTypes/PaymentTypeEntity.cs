using dTax.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.PaymentTypes
{
    public class PaymentTypeEntity : BaseEntity
    {
        public string TypeName { get; set; }
    }
}
