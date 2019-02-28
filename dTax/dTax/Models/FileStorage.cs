using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class FileStorage
    {
        [Key]
        public Guid FileId { get; set; }

        public byte[] ContentData { get; set; }

        public string FileName { get; set; }

        //Фото документов или машины или фото пользователя
        public string Type { get; set; }

    }
}
