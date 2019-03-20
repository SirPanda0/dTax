using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models.Dictionary
{
    public class DCarsBrand
    {
        [Key]
        public long BrandCode { get; set; }
        public string BrandName { get; set; }
    }
}
