using dTax.Interfaces;
using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class DBWorkFlow : IDBWorkFlow
    {
        public DBWorkFlow(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            DbPostrgreContext db
            )
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        public IUserRepository UserRepository { get; }

        public IRoleRepository RoleRepository { get; }
    }
}
