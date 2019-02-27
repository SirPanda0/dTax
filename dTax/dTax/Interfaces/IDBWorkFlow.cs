using dTax.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces
{
    public interface IDBWorkFlow
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        ICustomerRepository CustomerRepository { get; }
    }
}
