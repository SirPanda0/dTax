using dTax.Entity.Models;
using dTax.Entity.Models.Base;
using dTax.Entity.Models.FileContents;
using dTax.Entity.Models.Many;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.FileStorages
{
    public class FileStorageEntity : BaseEntity
    {

        public Guid FileContentId { get; set; }
        public FileContentEntity FileContent { get; set; }

        public string FileName { get; set; }

        public ICollection<DriverFileEntity> DriversLink { get; set; }
        public ICollection<CabFileEntity> CabLink { get; set; }

        //Фото документов или машины или фото пользователя
        //public string Type { get; set; }

    }
}
