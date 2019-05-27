using dTax.Entity.Models.Customers;
using dTax.Entity.Models.PaymentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.CabRides
{
    public class CabRide
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; } //id клиента
        public Customer Customer { get; set; }

        public string AddressStartPoint { get; set; }//Место начала поездки
        public string StartPointGPS { get; set; }//
        public string AddressEndPoint { get; set; }//Место конца поездки
        public string EndPointGPS { get; set; }//

        public bool IsCanceled { get; set; } = false;//Отменена ли поездки

        public int TariffType { get; set; }
        public int PaymentTypeId { get; set; }//Тип оплаты
        public PaymentType PamentType { get; set; }
        public decimal Price { get; set; }//Цена за поездку

        public string BookDetails { get; set; }// доп информация о заказе

    }
}
