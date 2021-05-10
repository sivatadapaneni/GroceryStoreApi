using GroceryStoreApi.BusinessLayer;
using GroceryStoreApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        /// <summary>
        /// Returns all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _customerService.GetCustomers().Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while retrieving customers", ex);
                return NotFound(); 
            }
        }

        /// <summary>
        /// Returns Customer details for provided id
        /// Returns Not found error if the provided id doesn't exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _customerService.GetCustomerById(id).Result;
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while retrieving customers", ex);
                return NotFound();
            }
        }

        /// <summary>
        /// Assigns id and writes the customer data to JSON
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        { 
            try
            {
                var result = _customerService.AddCustomer(customer).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while retrieving customers", ex);
                return BadRequest();
            }
        }


        /// <summary>
        /// updates the customer details to JSON
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            try
            {
                var result = _customerService.UpdateCustomer(customer).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while retrieving customers", ex);
                return BadRequest();
            }
        }
    }
}
