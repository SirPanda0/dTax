using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface ICustomerRepository: IBaseRepository<Customer>
    {
        Guid GetCustomerByUserId(Guid Id);
    }
}
