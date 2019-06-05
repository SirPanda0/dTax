using dTax.Entity.Models.Base;
using dTax.Entity.Models.CabRides;
using dTax.Entity.Models.Shifts;
using dTax.Entity.Models.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.CabRideStatuses
{
    public class CabRideStatusEntity : BaseEntity
    {
        //public Guid Id { get; set; }
        public Guid CabRideId { get; set; } //поездка
        public CabRideEntity CabRide { get; set; }

        public Guid StatusId { get; set; } //id статуса
        public StatusEntity Status { get; set; }

        public DateTime StatusTime { get; set; }//время когда была создана запись о поездке

        public DateTime? RideStartTime { get; set; }//началась поездка=>занят
        public DateTime? RideEndTime { get; set; }//закончилась поездка=>свободен

        public Guid? ShiftId { get; set; } // id смены
        public ShiftEntity Shift { get; set; }
        

    }
}
