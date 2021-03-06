using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCRM.API.DTOs
{
    public class UserUpdate
    {
        [Required(ErrorMessage = "ID is missing.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [EmailAddress(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
    }
}
