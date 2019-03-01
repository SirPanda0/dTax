using dTax.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    public class OperatorController : BaseUtilsController
    {
        private IDBWorkFlow DBWorkflow;
        public OperatorController(IDBWorkFlow dBWorkFlow)
        {
            DBWorkflow = dBWorkFlow;
        }


    }
}
