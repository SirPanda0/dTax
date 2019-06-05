using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class OrderUpdateModel
    {
        public Guid Id { get; set; }

        public string AddressStartPoint { get; set; }//Место начала поездки
        public string StartPointGPS { get; set; }//
        public string AddressEndPoint { get; set; }//Место конца поездки
        public string EndPointGPS { get; set; }//

        public int TariffType { get; set; }
        public Guid PaymentTypeId { get; set; }//Тип оплаты

        public double Distance { get; set; }

        public string BookDetails { get; set; }
    }
}
