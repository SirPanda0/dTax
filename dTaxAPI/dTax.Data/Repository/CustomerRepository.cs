using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
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

        public Guid GetCustomerByUserId(Guid Id)
        {
            return GetQuery().FirstOrDefault(_ => _.UserId == Id).Id;
        }

        

    }
}
