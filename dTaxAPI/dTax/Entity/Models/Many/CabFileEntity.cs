using dTax.Entity.Models.Base;
using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.FileStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Many
{
    public class CabFileEntity : BaseEntity
    {

        public Guid CabId { get; set; }
        public CabEntity Cab { get; set; }

        public Guid FileId { get; set; }
        public FileStorageEntity File { get; set; }

    }
}
