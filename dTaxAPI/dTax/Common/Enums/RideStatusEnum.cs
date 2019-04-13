using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Common.Enums
{
    public enum RideStatusEnum
    {
        New = 1, //Новая поездка
        Assigned = 2, //Поездка, назначена водителю
        Started = 3, //Поездка началась
        Ended = 4, //Поездка завершилась
        Canceled = 5 //Поездка отменена
    }
}
