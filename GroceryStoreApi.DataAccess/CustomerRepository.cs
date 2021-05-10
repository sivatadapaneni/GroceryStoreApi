using GroceryStoreApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApi.DataAccess
{
    /// <summary>
    /// CustomerRepository
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerContext _context;

        public CustomerRepository(ICustomerContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.ReadFileAsync();
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            List<Customer> customers = await _context.ReadFileAsync();
            if (customers != null && customers.Any())
            {
                return customers.Where(c => c.Id == id).FirstOrDefault();
            }
            return null;
        }

        /// <inheritdoc />
        public async Task<Customer> AddCustomer(Customer customer)
        {
            if (customer.IsValid())
            {
                // find the max id from current customer and increment one and use it as id for the new customer
                var customers = await _context.ReadFileAsync();
                int maxId = customers.Max(c => c.Id);
                customer.Id = maxId + 1;

                customers.Add(customer);
                await Task.Run(() => _context.WriteFileAsync(customers));

                return customer;
            }
            else
            {
                throw new Exception("Invalid custoder data provided in the input");
            }
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {

            if (customer.IsValid())
            {
                var customers = await _context.ReadFileAsync();

                //check if the customer id from input exists in the file
                Customer updated = customers.Where(c => c.Id == customer.Id).FirstOrDefault();
                if (updated == null)
                    throw new Exception("Customer not present with provided Id");
                else
                {
                    updated.Update(customer);
                    await Task.Run(() => _context.WriteFileAsync(customers));
                    return customer;
                }
            }
            else
            {
                throw new Exception("Invalid customer data in the input.");
            }
        }


    }
}
