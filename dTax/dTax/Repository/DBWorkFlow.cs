﻿using dTax.Interfaces;
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
            ICustomerRepository customerRepository,
            IFileStorageRepository fileStorageRepository,
            IDriverRepository driverRepository,
            ICabRepository cabRepository,
            DbPostrgreContext db
            )
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            CustomerRepository = customerRepository;
            CabRepository = cabRepository;
        }

        public IUserRepository UserRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public ICustomerRepository CustomerRepository { get; }

        public IFileStorageRepository FileStorageRepository { get; }

        public IDriverRepository DriverRepository { get; }

        public ICabRepository CabRepository { get; }
    }
}
