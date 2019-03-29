using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Common
{
    public class Price
    {

        //TODO систему тарифов
        public int GetPrice(int distance)
        {
            return distance * 8;
        }


    }
}


