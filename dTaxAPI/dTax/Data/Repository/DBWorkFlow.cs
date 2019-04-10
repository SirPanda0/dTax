using dTax.Data.Interfaces;
using dTax.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
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

            ICarBrandRepository carBrandRepository,
            ICarColorRepository carColorRepository,
            ICarModelRepository carModelRepository,
            ICarTypeRepository carTypeRepository,
            IFileContentRepository fileContentRepository,
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
            CarBrandRepository = carBrandRepository;
            CarColorRepository = carColorRepository;
            CarModelRepository = carModelRepository;
            CarTypeRepository = carTypeRepository;
            FileContentRepository = fileContentRepository;
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

        public ICarBrandRepository CarBrandRepository { get; }
        public ICarColorRepository CarColorRepository { get; }
        public ICarModelRepository CarModelRepository { get; }
        public ICarTypeRepository CarTypeRepository { get; }
        public IFileContentRepository FileContentRepository { get; }

    }
}
