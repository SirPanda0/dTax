using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{

    public class CarModel
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ModelType { get; set; }
        public string ModelColor { get; set; }

    }
}
