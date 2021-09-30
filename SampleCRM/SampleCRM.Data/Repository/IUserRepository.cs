using SampleCRM.Entities;
using SampleCRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Data.Repository
{
    public interface IUserRepository
    {
        public Task<User> Add(User user);
        public Task<bool> Update(User user);
        public Task<User> GetById(string id);
        public Task<User> GetByEmail(string email);
        public Task<bool> UpdatePassword(string id, string password);
    }
}
