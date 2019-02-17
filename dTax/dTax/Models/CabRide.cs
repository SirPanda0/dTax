using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class CabRide
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; } //id клиента
        public Customer Customer { get; set; }

        public string AddressStartPoint { get; set; }//Место начала поездки
        public float? StartPointGPS { get; set; }//
        public string AddressEndPoint { get; set; }//Место конца поездки
        public float? EndPointGPS { get; set; }//

        public bool Canceled { get; set; }//Отменена ли поездки

        public int PaymentTypeId { get; set; }//Тип оплаты
        public PaymentType PamentType { get; set; }
        public decimal Price { get; set; }//Цена за поездку

        public string BookDetails { get; set; }// доп информация о заказе

    }
}
