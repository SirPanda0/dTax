using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class CabRideStatus
    {
        public Guid Id { get; set; }
        public Guid CabRideId { get; set; } //поездка
        public CabRide CabRide { get; set; }
        public Guid StatusId { get; set; } //id статуса
        public Status Status { get; set; }
        public DateTime StatusTime { get; set; }//время когда была создана запись о поездке
        public Guid CustomerId { get; set; } //id клиента
        public Customer Customer { get; set; }
        public Guid ShiftId { get; set; } // id смены
        public Shift Shift { get; set; }
        public string StatusDetails { get; set; }// доп информация о заказе

    }
}
