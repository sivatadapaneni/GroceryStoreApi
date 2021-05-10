using GroceryStoreApi.Model;
using GroceryStoreApi.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApi.BusinessLayer
{
    /// <summary>
    /// CustomerService
    /// Add business logic 
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public async Task<List<Customer>> GetCustomers()
        {
            return await _repo.GetCustomers();
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _repo.GetCustomerById(id);
        }

        /// <inheritdoc />
        public async Task<Customer> AddCustomer(Customer customer)
        {
            return await _repo.AddCustomer(customer);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            
            return await _repo.UpdateCustomer(customer);
        }


    }
}
