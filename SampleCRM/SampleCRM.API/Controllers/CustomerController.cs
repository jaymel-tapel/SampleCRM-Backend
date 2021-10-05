using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleCRM.API.DTOs;
using SampleCRM.API.Services;
using SampleCRM.Entities;
using SampleCRM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.API.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // Get All Customer
        [HttpPost("api/customer/getall")]
        public async Task<IActionResult> GetAllCustomer([FromBody] CustomerGetAll customerGetAll)
        {
            _logger.LogInformation("Fetching customer information");
            var Customers = await _customerService.GetAll(customerGetAll.FilterKeyword, customerGetAll.SortOrder, customerGetAll.SortColumn);
            _logger.LogInformation("Fetching customer success");
            return Ok(Customers);
        }

        // Add New Customer
        [HttpPost("api/customer/add")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerAdd customerAdd)
        {
            _logger.LogInformation("Customer add attempt");
            Customer customer = await _customerService.GetByEmail(customerAdd.Email);
            if (customer != null)
            {
                ModelState.AddModelError("Email", "Email already existing.");
            }

            Customer newCustomer = new()
            {
                Email = customerAdd.Email,
                FirstName = customerAdd.FirstName,
                LastName = customerAdd.LastName,
                Address = customerAdd.Address,
                Phone = customerAdd.Phone
            };

            // parse submitted date string into DateTime object
            if (DateTime.TryParse(customerAdd.Birthday, out DateTime birthday))
            {
                newCustomer.Birthday = birthday;
            }
            else
            {
                ModelState.AddModelError("Birthday", "Invalid Birthday Date Format.");
            }

            if (ModelState.IsValid)
            {
                // Generate customer code
                var custCodeSb = new StringBuilder();
                custCodeSb.Append(newCustomer.FirstName.ToLower());
                custCodeSb.Append(newCustomer.LastName.ToLower());
                custCodeSb.Append(newCustomer.Birthday.ToString("yyyyMMdd"));
                newCustomer.CustCode = custCodeSb.ToString();
                _logger.LogInformation("Customer add success");
                return Ok(await _customerService.Add(newCustomer));
            }
            else
            {
                _logger.LogInformation("Customer add failed");
                return BadRequest(ModelState);
            }
        }

        // Update Customer
        [HttpPost("api/customer/update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdate customerUpdate)
        {
            _logger.LogInformation("Customer update attempt");
            Customer checkCustomer = await _customerService.GetById(customerUpdate.Id);
            if (checkCustomer == null)
            {
                ModelState.AddModelError("Id", "User not found!");
            }

            Customer updatedCustomer = new()
            {
                Id = customerUpdate.Id,
                Email = customerUpdate.Email,
                FirstName = customerUpdate.FirstName,
                LastName = customerUpdate.LastName,
                Address = customerUpdate.Address,
                Phone = customerUpdate.Phone
            };

            // parse submitted date string into DateTime object
            if (DateTime.TryParse(customerUpdate.Birthday, out DateTime birthday))
            {
                updatedCustomer.Birthday = birthday;
            }
            else
            {
                ModelState.AddModelError("Birthday", "Invalid Birthday Date Format.");
            }

            if (ModelState.IsValid)
            {
                // Generate customer code
                var custCodeSb = new StringBuilder();
                custCodeSb.Append(updatedCustomer.FirstName.ToLower());
                custCodeSb.Append(updatedCustomer.LastName.ToLower());
                custCodeSb.Append(updatedCustomer.Birthday.ToString("yyyyMMdd"));
                updatedCustomer.CustCode = custCodeSb.ToString();
                _logger.LogInformation("Customer update success");
                return Ok(await _customerService.Update(updatedCustomer));
            }
            else
            {
                _logger.LogInformation("Customer update failed");
                return BadRequest(ModelState);
            }
        }

        // Get Details Customer
        [HttpPost("api/customer/get/{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            _logger.LogInformation("Fetching customer information with id:" + id);
            Customer customer = await _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound("Customer not found!");
            }

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Fetch customer information success");
                return Ok(customer);
            }
            else
            {
                _logger.LogInformation("Fetch customer information failed");
                return BadRequest(ModelState);
            }
        }

        // Delete Customer
        [HttpPost("api/customer/delete/{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            _logger.LogInformation("Deleting customer with id:" + id);
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Delete customer success");
                return Ok(await _customerService.Delete(id));
            }
            else
            {
                _logger.LogInformation("Delete customer failed");
                return BadRequest(ModelState);
            }
        }

    }
}
