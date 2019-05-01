using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ResponseModels
{
    public class PriceResponse
    {
        public decimal Standart { get; set; }
        public decimal Comfort { get; set; }
        public decimal Emergency { get; set; }
        public decimal Minivan { get; set; }
    }
}
