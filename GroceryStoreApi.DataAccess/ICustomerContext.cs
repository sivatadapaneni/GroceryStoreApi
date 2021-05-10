using System;
using System.Collections.Generic;
using GroceryStoreApi.Model;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApi.DataAccess
{
    public interface ICustomerContext
    {
        /// <summary>
        /// Reads data from json file and deserialize the data to customers 
        /// </summary>
        /// <returns>returns all customers</returns>
        Task<List<Customer>> ReadFileAsync();

        /// <summary>
        /// Writes customers data back to file
        /// </summary>
        /// <param name="Customer">Customers to Write</param>
        /// <returns>returns status</returns>
        void WriteFileAsync(List<Customer> customers);

    }
}
