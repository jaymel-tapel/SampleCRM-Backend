using SampleCRM.Entities;
using SampleCRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCRM.API.ViewModels
{
    public class UserWithToken
    {
        public User User { get; set; }
        public TokenWithExpireDate Token { get; set; }
    }
}
