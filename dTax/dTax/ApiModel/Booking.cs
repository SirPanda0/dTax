using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class Booking
    {
        public Guid UserId { get; set; } //id клиента

        public string AddressStartPoint { get; set; }
        public string AddressEndPoint { get; set; }

        public int PaymentTypeId { get; set; }//Тип оплаты

        public string BookDetails { get; set; }// доп информация о заказе

        public int Distance { get; set; }// дистанция от объекта к объекту
    }
}
