using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ViewModels
{
    public class DriverViewModel
    {
        public Guid DriverId { get; set; }

        public string FIO { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
