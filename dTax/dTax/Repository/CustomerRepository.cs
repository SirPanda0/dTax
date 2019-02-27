using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        DbPostrgreContext CommonDbContext;

        public CustomerRepository(DbPostrgreContext commonDbContext) : base(commonDbContext)
        {
            CommonDbContext = commonDbContext; ;
        }


    }
}
