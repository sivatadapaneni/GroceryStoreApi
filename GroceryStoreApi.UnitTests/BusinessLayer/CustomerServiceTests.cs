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
    public class CustomerServiceTests
    {
        private List<Customer> _customers;
        Mock<ICustomerRepository> _mock;

        public CustomerServiceTests()
        {
            _customers = new List<Customer>();
            _customers.Add(new Customer() { Id = 1, FirstName = "David" });

            _mock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public void GetCustomers_ShouldReturnValidData_ForSuccessResponse_FromCustomerContext()
        {

            _mock.Setup(mc => mc.GetCustomers()).Returns(Task.FromResult(_customers));

            ICustomerService repo = new CustomerService(_mock.Object);
            var customerData = repo.GetCustomers().Result;

            customerData.Should().BeEquivalentTo(_customers);

        }


        [Fact]
        public void GetCustomers_ShouldThrowException_ForErrorResponse_FromCustomerContext()
        {

            _mock.Setup(mc => mc.GetCustomers()).Throws<FileNotFoundException>();

            ICustomerService repo = new CustomerService(_mock.Object);
            repo.Invoking(x => x.GetCustomers()).Should().Throw<FileNotFoundException>();

        }

        [Fact]
        public void GetCustomerById_ShouldReturnValidData_ForSuccessResponse_FromCustomerContext()
        {
            _mock.Setup(mc => mc.GetCustomerById(It.IsAny<int>())).Returns(Task.FromResult(_customers[0]));

            ICustomerService repo = new CustomerService(_mock.Object);
            var customerData = repo.GetCustomerById(1).Result;

            customerData.Should().BeEquivalentTo(_customers.Where(c => c.Id == 1).FirstOrDefault());

        }
        [Fact]
        public void GetCustomerById_ShouldReturnNull_IfCustomerContextReturns_NoDataForId()
        {

            _mock.Setup(mc => mc.GetCustomerById(It.IsAny<int>())).Returns(Task.FromResult<Customer>(null));

            ICustomerService repo = new CustomerService(_mock.Object);
            var customerData = repo.GetCustomerById(2).Result;

            customerData.Should().BeNull();

        }

        [Fact]
        public void GetCustomerById_ShouldThrowException_ForErrorResponse_FromCustomerContext()
        {

            _mock.Setup(mc => mc.GetCustomerById(It.IsAny<int>())).Throws<FileNotFoundException>();

            ICustomerService repo = new CustomerService(_mock.Object);
            repo.Invoking(x => x.GetCustomerById(1)).Should().Throw<FileNotFoundException>();

        }

        [Fact]
        public void AddCustomer_ShouldReturnValidId_AfterSuccessfulInsert()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };

            _mock.Setup(mc => mc.AddCustomer(customer)).Returns(Task.FromResult(customer));

            ICustomerService repo = new CustomerService(_mock.Object);
            var actual = repo.AddCustomer(customer).Result;

            actual.Id.Should().BeGreaterThan(0);
            actual.FirstName.Should().BeEquivalentTo("Test");
            actual.LastName.Should().BeEquivalentTo("Test");
            actual.Gender.Should().BeEquivalentTo("Male");

        }

        [Fact]
        public void AddCustomer_ShouldReturnException_WhenCustomerContext_throwsException()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };

            _mock.Setup(mc => mc.AddCustomer(customer)).Throws<Exception>();


            ICustomerService repo = new CustomerService(_mock.Object);
            repo.Invoking(x => x.AddCustomer(customer)).Should().Throw<Exception>();

        }

        [Fact]
        public void UpdateCustomer_ShouldReturnValidRes_AfterSuccessfulUpdate()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };

            _mock.Setup(mc => mc.UpdateCustomer(customer)).Returns(Task.FromResult(customer));

            ICustomerService repo = new CustomerService(_mock.Object);
            var actual = repo.UpdateCustomer(customer).Result;

            actual.Id.Should().Be(2);
            actual.FirstName.Should().BeEquivalentTo("Test");
            actual.LastName.Should().BeEquivalentTo("Test");
            actual.Gender.Should().BeEquivalentTo("Male");

        }

        [Fact]
        public void UpdateCustomer_ShouldReturnException_WhenCustomerContext_throwsException()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };

            _mock.Setup(mc => mc.UpdateCustomer(customer)).Throws<Exception>();


            ICustomerService repo = new CustomerService(_mock.Object);
            repo.Invoking(x => x.UpdateCustomer(customer)).Should().Throw<Exception>();

        }
    }
}
