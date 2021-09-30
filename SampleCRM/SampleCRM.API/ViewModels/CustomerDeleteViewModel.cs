using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCRM.API.ViewModels
{
    public class CustomerDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
