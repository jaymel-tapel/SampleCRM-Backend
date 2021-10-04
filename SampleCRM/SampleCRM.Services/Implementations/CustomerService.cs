using SampleCRM.Data.Repository;
using SampleCRM.Entities;
using SampleCRM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.API.Services
{

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
