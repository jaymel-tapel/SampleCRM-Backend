using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCRM.API.DTOs
{
    public class CustomerDelete
    {
        [Required]
        public int Id { get; set; }
    }
}
