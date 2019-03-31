using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models.Dictionary
{
    public class CarBrand : BaseEntity
    {
        public long Code { get; set; }
        public string Name { get; set; }
    }
}
