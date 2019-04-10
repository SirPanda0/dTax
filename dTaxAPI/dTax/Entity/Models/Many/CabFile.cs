using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.FileStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Many
{
    public class FilesToCab
    {
        [Key]
        public Guid ID { get; set; }

        public Guid CabId { get; set; }
        public Cab Cab { get; set; }

        public Guid FileId { get; set; }
        public FileStorage File { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
