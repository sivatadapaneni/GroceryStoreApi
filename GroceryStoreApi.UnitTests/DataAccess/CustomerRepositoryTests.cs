using GroceryStoreApi.DataAccess;
using GroceryStoreApi.Model;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using GroceryStoreApi.BusinessLayer;
using System.IO;
using System;

namespace GroceryStoreApi.UnitTests
{
    public class CustomerRepositoryTests
    {
        private List<Customer> _customers;
        Mock<ICustomerContext> _mock;

        public CustomerRepositoryTests()
        {
            _customers = new List<Customer>();
            _customers.Add(new Customer() { Id = 1, FirstName = "David" });

            _mock = new Mock<ICustomerContext>();
        }

        [Fact]
        public void GetCustomers_ShouldReturnValidData_ForSuccessResponse_FromCustomerContext()
        {

            _mock.Setup(mc => mc.ReadFileAsync()).Returns(Task.FromResult(_customers));

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            var customerData = repo.GetCustomers().Result;

            customerData.Should().BeEquivalentTo(_customers);

        }


        [Fact]
        public void GetCustomers_ShouldThrowException_ForErrorResponse_FromCustomerContext()
        {

            _mock.Setup(mc => mc.ReadFileAsync()).Throws<FileNotFoundException>();

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            repo.Invoking(x => x.GetCustomers()).Should().Throw<FileNotFoundException>();

        }

        [Fact]
        public void GetCustomerById_ShouldReturnValidData_ForSuccessResponse_FromCustomerContext()
        {
            _mock.Setup(mc => mc.ReadFileAsync()).Returns(Task.FromResult(_customers));

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            var customerData = repo.GetCustomerById(1).Result;

            customerData.Should().BeEquivalentTo(_customers.Where(c => c.Id == 1).FirstOrDefault());

        }
        [Fact]
        public void GetCustomerById_ShouldReturnNull_IfCustomerContextReturns_NoDataForId()
        {

            _mock.Setup(mc => mc.ReadFileAsync()).Returns(Task.FromResult(_customers));

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            var customerData = repo.GetCustomerById(2).Result;

            customerData.Should().BeNull();

        }

        [Fact]
        public void GetCustomerById_ShouldThrowException_ForErrorResponse_FromCustomerContext()
        {

            _mock.Setup(mc => mc.ReadFileAsync()).Throws<FileNotFoundException>();

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            repo.Invoking(x => x.GetCustomerById(1)).Should().Throw<FileNotFoundException>();

        }

        [Fact]
        public void AddCustomer_ShouldReturnValidId_AfterSuccessfulInsert()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };

            _mock.Setup(mc => mc.ReadFileAsync()).Returns(Task.FromResult(_customers));


            ICustomerRepository repo = new CustomerRepository(_mock.Object);
             var actual = repo.AddCustomer(customer).Result;

            _mock.Verify(mc => mc.WriteFileAsync(It.IsAny<List<Customer>>()),Times.Exactly(1));
            
            actual.Id.Should().BeGreaterThan(0);
            actual.FirstName.Should().BeEquivalentTo("Test");
            actual.LastName.Should().BeEquivalentTo("Test");
            actual.Gender.Should().BeEquivalentTo("Male");

        }

        [Fact]
        public void AddCustomer_ShouldReturnException_WhenCustomerContext_throwsException()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            repo.Invoking(x => x.AddCustomer(customer)).Should().Throw<Exception>();

        }

        [Fact]
        public void UpdateCustomer_ShouldReturnValidRes_AfterSuccessfulUpdate()
        {
            Customer customer = new Customer() { Id = 1, FirstName = "Test", LastName = "Test", Gender = "Male" };

            _mock.Setup(mc => mc.ReadFileAsync()).Returns(Task.FromResult(_customers));

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            var actual = repo.UpdateCustomer(customer).Result;

            actual.Id.Should().Be(1);
            actual.FirstName.Should().BeEquivalentTo("Test");
            actual.LastName.Should().BeEquivalentTo("Test");
            actual.Gender.Should().BeEquivalentTo("Male");

        }

        [Fact]
        public void UpdateCustomer_ShouldReturnException_WhenCustomerContext_throwsException()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };

            ICustomerRepository repo = new CustomerRepository(_mock.Object);
            repo.Invoking(x => x.UpdateCustomer(customer)).Should().Throw<Exception>();

        }
    }
}
