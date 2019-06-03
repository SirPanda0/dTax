
using dTax.Entity.Models.PaymentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IPaymentTypeRepository : IBaseRepository<PaymentTypeEntity>
    {
    }
}
