using SampleCRM.Data.Repository;
using SampleCRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.API.Services
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

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRespository _customerRepository;
        public CustomerService(ICustomerRespository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> Add(Customer customer)
        {
            return await _customerRepository.Add(customer);
        }

        public async Task<bool> Delete(int id)
        {
            return await _customerRepository.Delete(id);
        }

        public async Task<IEnumerable<CustomerBasicInfo>> GetAll(string filterKeyword = null, string sortOrder = null, string SortColumn = null)
        {
            return await _customerRepository.GetAll(filterKeyword, sortOrder, SortColumn);
        }

        public async Task<bool> Update(Customer customer)
        {
            return await _customerRepository.Update(customer);
        }

        public async Task<Customer> GetById(int id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await _customerRepository.GetByEmail(email);
        }
    }
}
