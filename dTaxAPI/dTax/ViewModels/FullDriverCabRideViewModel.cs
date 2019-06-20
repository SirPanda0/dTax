﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ViewModels
{
    public class FullDriverCabRideViewModel
    {
        public Guid Id { get; set; }
        public string AddressStartPoint { get; set; }//Место начала поездки
        public string AddressEndPoint { get; set; }//Место конца поездки
        public int TariffTypeId { get; set; }
        public string TariffType { get; set; }

        public Guid PaymentTypeId { get; set; }
        public string PaymentType { get; set; }

        public Guid RideStatusId { get; set; }
        public string RideStatus { get; set; }
        public decimal Price { get; set; }

        public string BookDetails { get; set; }
    }
}