﻿using dTax.Entity.Models.Base;
using dTax.Entity.Models.FileStorages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.FileContents
{
    public class FileContent
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<FileStorage> FileStorage { get; set; }
        public byte[] ContentData { get; set; }
    }
}