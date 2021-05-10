using GroceryStoreApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApi.BusinessLayer
{
    /// <summary>
    /// TO DO: Move Business services to another project for the better readabily when more services are added
    /// ICustomerRepository interface
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>Gets all customers</returns>
        Task<List<Customer>> GetCustomers();

        /// <summary>
        /// Gets customer by id
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Gets customer by id</returns>
        Task<Customer> GetCustomerById(int id);

        /// <summary>
        /// Add new Customer
        /// </summary>
        /// <param name="Customer">Customer to add</param>
        /// <returns>Added Customer</returns>
        Task<Customer> AddCustomer(Customer customer);


        /// <summary>
        /// Saves updated Customer
        /// </summary>
        /// <param name="Customer">Customer to save</param>
        /// <returns>Saved Customer</returns>
        Task<Customer> UpdateCustomer(Customer customer);

    }
}
