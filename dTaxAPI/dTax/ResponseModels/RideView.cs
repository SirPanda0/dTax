using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ResponseModels
{
    public class RideResponse
    {

        public Guid Id { get; set; }

        public string AddressStartPoint { get; set; }//Место начала поездки
        public string StartPointGPS { get; set; }//
        public string AddressEndPoint { get; set; }//Место конца поездки
        public string EndPointGPS { get; set; }//

        public Guid PaymentTypeId { get; set; }//Тип оплаты

        public decimal Price { get; set; }//Цена за поездку

        public string BookDetails { get; set; }// доп информация о заказе
    }
}
