﻿using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.PaymentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class PaymentTypeRepository : BaseRepository<PaymentTypeEntity>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(DbPostrgreContext context) : base(context)
        {
        }

        public IEnumerable<PaymentTypeEntity> GetPaymentTypes()
        {
            return GetQuery().Where(_ => _.IsDeleted != true).ToList();
        }

    }
}
