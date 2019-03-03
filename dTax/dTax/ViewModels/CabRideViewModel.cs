using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ViewModels
{
    public class CabRideViewModel
    {
        public Guid Id { get; set; }
        public string AddressStartPoint { get; set; }
        public string AddressEndPoint { get; set; }
        public decimal Price { get; set; }
    }
}
