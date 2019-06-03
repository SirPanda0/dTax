using dTax.Entity.Models.Base;
using dTax.Entity.Models.Drivers;
using dTax.Entity.Models.FileStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Many
{
    public class DriverFileEntity : BaseEntity
    {


        
        public Guid DriverId { get; set; }
        public DriverEntity Driver { get; set; }
        
        public Guid FileId { get; set; }
        public FileStorageEntity File { get; set; }


    }
}
