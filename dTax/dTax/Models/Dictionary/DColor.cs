using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models.Dictionary
{
    public class DColor
    {
        [Key]
        public long ColorCode { get; set; }
        public string Color { get; set; }
    }
}
