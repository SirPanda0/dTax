using dTax.Data.Interfaces;
using dTax.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
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
        IPaymentTypeRepository PaymentTypeRepository { get; }
        IShiftRepository ShiftRepository { get; }
        IStatusesRepository StatusesRepository { get; }
        IDriverFileRepository DriverFileRepository { get; }
        ICabFileRepository CabFileRepository { get; }

        ICarBrandRepository CarBrandRepository { get; }
        ICarColorRepository CarColorRepository { get; }
        ICarModelRepository CarModelRepository { get; }
        ICarTypeRepository CarTypeRepository { get; }
        IFileContentRepository FileContentRepository { get; }
    }
}
