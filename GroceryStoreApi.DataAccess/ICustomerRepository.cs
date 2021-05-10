using System;
using System.Collections.Generic;
using GroceryStoreApi.Model;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApi.DataAccess
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>Gets all customers</returns>
        Task<List<Customer>> GetCustomers();
        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>Get Customer by Id</returns>
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
        Task<Customer> UpdateCustomer(Customer Customer);
    }
}
