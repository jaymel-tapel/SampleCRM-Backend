using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Entities
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
