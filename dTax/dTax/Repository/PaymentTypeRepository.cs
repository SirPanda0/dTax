using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class PaymentTypeRepository : BaseRepository<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
