using Microsoft.EntityFrameworkCore;
using SampleCRM.Data.Repository;
using SampleCRM.Entities;
using SampleCRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Data.EFCore
{
    public class UserRepository : IUserRepository
    {
        private readonly CrmDbContext _db;

        public UserRepository(CrmDbContext crmDbContext)
        {
            _db = crmDbContext;
        }

        public async Task<User> Add(User user)
        {
            try
            {
                _db.Users.Add(user);
                _ = await this._db.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error adding user", e);
            }
        }

        public async Task<bool> Update(User user)
        {
            try
            {
                var entity = _db.Users.Find(user.Id);
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Email = user.Email;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error updating user", e);
            }
        }

        public async Task<bool> UpdatePassword(string id, string password)
        {
            try
            {
                var entity = _db.Users.Find(id);
                entity.Password = password;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error updating password", e);
            }
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                var data = await _db.Users.FirstOrDefaultAsync(info => info.Id == id);
                return data;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error fetching user details", e);
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                var data = await _db.Users.FirstOrDefaultAsync(info => info.Email == email);
                return data;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error fetching user details", e);
            }
        }
    }
}
