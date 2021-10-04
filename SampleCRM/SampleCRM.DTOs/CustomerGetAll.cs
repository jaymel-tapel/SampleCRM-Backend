using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCRM.API.DTOs
{
    public class CustomerGetAll
    {
        public string FilterKeyword { get; set; }
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }

    }
}
