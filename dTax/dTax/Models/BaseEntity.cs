using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }



        public DateTime Created { get; set; } = DateTime.Now.ToLocalTime();
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }
}
