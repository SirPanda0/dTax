using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models.Dictionary
{
    public class DCarsModel
    {
        [Key]
        public long ModelCode { get; set; }
        public string ModelName { get; set; }
    }
}
