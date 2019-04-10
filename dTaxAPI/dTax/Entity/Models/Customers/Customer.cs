using dTax.Entity.Models.Base;
using dTax.Entity.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Customers
{
    public class Customer : BaseEntity
    {
        

        public Guid UserId { get; set; }
        public User User { get; set; }

        //Фото (аватарка)
        //public Guid? FileStorageId { get; set; }
        //public FileStorage FileStorage { get; set; }

    }
}
