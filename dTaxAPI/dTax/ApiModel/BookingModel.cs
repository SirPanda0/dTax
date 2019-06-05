using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class BookingModel
    {
        //public Guid UserId { get; set; } //id клиента

        public string AddressStartPoint { get; set; }//Место начала поездки
        public string StartPointGPS { get; set; }//
        public string AddressEndPoint { get; set; }//Место конца поездки
        public string EndPointGPS { get; set; }//

        public int TariffType { get; set; } // Тариф

        public Guid PaymentTypeId { get; set; }//Тип оплаты

        public string BookDetails { get; set; }// доп информация о заказе

        public double Distance { get; set; }// дистанция от объекта к объекту

    }
}
