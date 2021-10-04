using SampleCRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Services.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Add a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Customer> Add(Customer customer);

        /// <summary>
        /// Get customer details by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Customer> GetByEmail(string email);

        /// <summary>
        /// Get customer details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Customer> GetById(int id);

        /// <summary>
        /// Get customer list with basic information
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CustomerBasicInfo>> GetAll(string filterKeyword = null, string sortOrder = null, string SortColumn = null);

        /// <summary>
        /// Update customer details
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<bool> Update(Customer customer);

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(int id);
    }

}
