using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class Booking
    {
        //public Guid UserId { get; set; } //id клиента

        public string AddressStartPoint { get; set; }//Место начала поездки
        public float? StartPointGPS { get; set; }//
        public string AddressEndPoint { get; set; }//Место конца поездки
        public float? EndPointGPS { get; set; }//

        public int TariffType { get; set; } // Тариф

        public int PaymentTypeId { get; set; }//Тип оплаты

        public string BookDetails { get; set; }// доп информация о заказе

        public string Distance { get; set; }// дистанция от объекта к объекту

    }
}
