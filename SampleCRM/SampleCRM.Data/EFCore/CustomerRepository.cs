using Microsoft.EntityFrameworkCore;
using SampleCRM.Data.Repository;
using SampleCRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Data.EFCore
{



    public class CustomerRepository : ICustomerRespository
    {
        private readonly CrmDbContext _db;

        public CustomerRepository(CrmDbContext crmDbContext)
        {
            _db = crmDbContext;
        }

        public async Task<Customer> Add(Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                await this._db.SaveChangesAsync();
                return customer;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error adding customer", e);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                _db.Customers.Remove(this._db.Customers.Find(id));
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error deleting customer", e);
            }
        }

        // NEEDS TO BE REFACTOR FOR QUERY OPTIMIZATION, OMIT BIRTHDAY, ADDRESS AND PHONE FIELDS IN THE QUERY
        public async Task<IEnumerable<CustomerBasicInfo>> GetAll(string filterKeyword = null, string sortOrder = null, string sortColumn = null)
        {
            try
            {
                var sqlSb = new StringBuilder("SELECT * from [dbo].[Customers] ");

                if (!String.IsNullOrEmpty(filterKeyword))
                {
                    if (int.TryParse(filterKeyword, out int id))
                    {
                        sqlSb.Append($"Where Id='{id}' ");
                    }
                    else
                    {
                        sqlSb.Append($"Where LastName LIKE '%{filterKeyword}%' OR " +
                            $" FirstName LIKE '%{filterKeyword}%' OR " +
                            $" Email LIKE '%{filterKeyword}%' OR " +
                            $" CustCode  LIKE '%{filterKeyword}%' ");
                    }
                }

                if (!String.IsNullOrEmpty(sortOrder) && !String.IsNullOrEmpty(sortColumn))
                {
                    sqlSb.Append($"ORDER BY {sortColumn} {sortOrder}");
                }
                else
                {
                    sqlSb.Append($"ORDER BY Id ASC");
                }

                var data = await _db.Customers.FromSqlRaw(sqlSb.ToString()).ToListAsync();
                return data;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error fetching customers", e);
            }
        }

        public async Task<Customer> GetById(int id)
        {
            try
            {
                var data = await _db.Customers.FirstOrDefaultAsync(info => info.Id == id);
                return data;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error fetching customer details", e);
            }
        }

        public async Task<Customer> GetByEmail(string email)
        {
            try
            {
                var data = await _db.Customers.FirstOrDefaultAsync(info => info.Email == email);
                return data;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error fetching customer details", e);
            }
        }

        public async Task<bool> Update(Customer customer)
        {
            try
            {
                var entity = _db.Customers.Find(customer.Id);
                entity.FirstName = customer.FirstName;
                entity.LastName = customer.LastName;
                entity.Birthday = customer.Birthday;
                entity.Phone = customer.Phone;
                entity.Email = customer.Email;
                entity.Address = customer.Address;
                entity.CustCode = customer.CustCode;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new DatabaseAccessException("Error updating customer", e);
            }
        }
    }
}
