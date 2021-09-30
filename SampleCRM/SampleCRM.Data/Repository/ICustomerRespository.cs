using SampleCRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Data.Repository
{
    public interface ICustomerRespository
    {

        /// <summary>
        /// Gets all the customer. Filters and sorting can be supplied.
        /// FilterKeyword = Accepts any string
        /// Sort = ASC or DESC
        /// Column = 
        /// </summary>
        /// <param name="filterKeyword"></param>
        /// <param name="sort"></param>
        /// <param name="columnForSort"></param>
        /// <returns></returns>
        public Task<IEnumerable<CustomerBasicInfo>> GetAll(string filterKeyword = null, string sortOrder = null, string sortColumn = null);
        public Task<Customer> GetById(int id);
        public Task<Customer> GetByEmail(string email);
        public Task<Customer> Add(Customer customer);
        public Task<bool> Update(Customer customer);
        public Task<bool> Delete(int id);
    }
}
