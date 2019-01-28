using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class CabRideStatus
    {
        public int Id { get; set; }
        public int CabRideId { get; set; } //поездка
        public CabRide CabRide { get; set; }
        public int StatusId { get; set; } //id статуса
        public Status Status { get; set; }
        public DateTime StatusTime { get; set; }//время когда была создана запись о поездке
        public int CustomerId { get; set; } //id клиента
        public Customer Customer { get; set; }
        public int ShiftId { get; set; } // id смены
        public Shift Shift { get; set; }
        public string StatusDetails { get; set; }// доп информация о заказе

    }
}
