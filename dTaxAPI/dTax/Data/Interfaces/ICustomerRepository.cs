using dTax.Entity.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICustomerRepository: IBaseRepository<CustomerEntity>
    {
        Guid GetCustomerByUserId(Guid Id);
    }
}
