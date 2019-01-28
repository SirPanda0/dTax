using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class CabRide
    {
        public int Id { get; set; }
        public int ShiftId { get; set; } //id смены
        public Shift Shift { get; set; }
        public DateTime RideStartTime { get; set; }//началась поездка=>занят
        public DateTime RideEndTime { get; set; }//закончилась поездка=>свободен
        public string AddressStartPoint { get; set; }//Место начала поездки
        public string StartPointGPS { get; set; }//
        public string AddressEndPoint { get; set; }//Место конца поездки
        public string EndPointGPS { get; set; }//
        public bool Canceled { get; set; }//Отменена ли поездки
        public int PaymentTypeId { get; set; }//Тип оплаты
        public PaymentType PamentType { get; set; }
        public decimal Price { get; set; }//Цена за поездку

    }
}
