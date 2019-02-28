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
        IFileStorageRepository FileStorageRepository { get; }
        IDriverRepository DriverRepository { get; }

        ICabRepository CabRepository { get; }
        ICabRideRepository CabRideRepository { get; }
        ICabRideStatusRepository CabRideStatusRepository { get; }
        ICarModelsRepository CarModelsRepository { get; }
        IPaymentTypeRepository PaymentTypeRepository { get; }
        IShiftRepository ShiftRepository { get; }
        IStatusesRepository StatusesRepository { get; }
    }
}
