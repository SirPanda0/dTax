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
            ICustomerRepository customerRepository,
            IFileStorageRepository fileStorageRepository,
            IDriverRepository driverRepository,
            ICabRepository cabRepository,

            ICabRideRepository cabRideRepository,
            ICabRideStatusRepository cabRideStatusRepository,
            ICarModelsRepository carModelsRepository,
            IPaymentTypeRepository paymentTypeRepository,
            IShiftRepository shiftRepository,
            IStatusesRepository statusesRepository,
            IDriverFileRepository driverFileRepository,
            ICabFileRepository cabFileRepository,
            DbPostrgreContext db
            )
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            CustomerRepository = customerRepository;
            CabRepository = cabRepository;
            CabRideRepository = cabRideRepository;
            CabRideStatusRepository = cabRideStatusRepository;
            CarModelsRepository = carModelsRepository;
            PaymentTypeRepository = paymentTypeRepository;
            ShiftRepository = shiftRepository;
            StatusesRepository = statusesRepository;
            DriverRepository = driverRepository;
            FileStorageRepository = fileStorageRepository;
            DriverFileRepository = driverFileRepository;
            CabFileRepository = cabFileRepository;
        }

        public IUserRepository UserRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public ICustomerRepository CustomerRepository { get; }

        public IFileStorageRepository FileStorageRepository { get; }

        public IDriverRepository DriverRepository { get; }

        public ICabRepository CabRepository { get; }

        public ICabRideRepository CabRideRepository { get; }
        public ICabRideStatusRepository CabRideStatusRepository { get; }
        public ICarModelsRepository CarModelsRepository { get; }
        public IPaymentTypeRepository PaymentTypeRepository { get; }
        public IShiftRepository ShiftRepository { get; }
        public IStatusesRepository StatusesRepository { get; }

        public IDriverFileRepository DriverFileRepository { get; }

        public ICabFileRepository CabFileRepository { get;}
    }
}
