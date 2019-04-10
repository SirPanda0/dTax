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
    public class FilesToDriver
    {

        [Key]
        public Guid ID { get; set; }
        
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }
        
        public Guid FileId { get; set; }
        public FileStorage File { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
