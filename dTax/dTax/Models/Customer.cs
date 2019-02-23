using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        //Фото (аватарка)
        public Guid? FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }

    }
}
