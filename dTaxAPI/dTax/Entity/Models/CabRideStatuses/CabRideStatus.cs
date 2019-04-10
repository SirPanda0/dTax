using dTax.Entity.Models.CabRides;
using dTax.Entity.Models.Shifts;
using dTax.Entity.Models.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.CabRideStatuses
{
    public class CabRideStatus
    {
        public Guid Id { get; set; }
        public Guid CabRideId { get; set; } //поездка
        public CabRide CabRide { get; set; }

        public int StatusId { get; set; } //id статуса
        public Status Status { get; set; }

        public DateTime StatusTime { get; set; }//время когда была создана запись о поездке

        public DateTime? RideStartTime { get; set; }//началась поездка=>занят
        public DateTime? RideEndTime { get; set; }//закончилась поездка=>свободен

        public Guid? ShiftId { get; set; } // id смены
        public Shift Shift { get; set; }
        

    }
}
