 using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        DbPostrgreContext CommonDbContext;

        public CustomerRepository(DbPostrgreContext commonDbContext) : base(commonDbContext)
        {
            CommonDbContext = commonDbContext; ;
        }

        public Guid GetCustomerByUserId(Guid Id)
        {
            return GetQuery().FirstOrDefault(_ => _.UserId == Id).Id;
        }

        

    }
}
