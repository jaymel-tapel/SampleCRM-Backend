using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Entities
{
    public class Customer : CustomerBasicInfo
    {
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
