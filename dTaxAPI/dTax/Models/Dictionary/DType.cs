using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models.Dictionary
{
    public class DType
    {
        [Key]
        public long TypeCode { get; set; }
        public string TypeName { get; set; }
    }
}
