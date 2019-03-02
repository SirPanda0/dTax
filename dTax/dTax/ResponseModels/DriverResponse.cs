using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ResponseModels
{
    public class DriverResponse
    {
        public Guid Id { get; set; }

        public string FIO { get; set; }
        public long DrivingLicence { get; set; } //номер прав
        public DateTime ExpiryDate { get; set; } //дата окончания действия прав
        //Паспорт
        public DateTime RegistrationDate { get; set; }
        public string PassportSerial { get; set; }
        public string PassportNumber { get; set; }
        public long DriverFileStorageId { get; set; }

        //Cab
        public Cab Cab { get; set; }
        public CarModel CarModel { get; set; }
        
    }
}
