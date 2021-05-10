using GroceryStoreApi.Controllers;
using GroceryStoreApi.Model;
using GroceryStoreApi.BusinessLayer;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreApi.UnitTests
{
    public class CustomerControllerTests
    {
        private List<Customer> _customers;
        Mock<ICustomerService> _mockService;
        Mock<ILogger<CustomerController>> _mockLogger;

        public CustomerControllerTests()
        {
            _customers = new List<Customer>();
            _customers.Add(new Customer() { Id = 1, FirstName = "David" });

            _mockService = new Mock<ICustomerService>();
            _mockLogger = new Mock<ILogger<CustomerController>>();
            //_mockLogger.Setup(mc => mc.LogError(It.IsAny<string>(), It.IsAny<Type>())).Verifiable();
        }

        [Fact]
        public void Get_ShouldReturnOKResponse_ForSuccessResponse_FromCustomerService()
        {

            _mockService.Setup(mc => mc.GetCustomers()).Returns(Task.FromResult(_customers));

            CustomerController repo = new CustomerController(_mockLogger.Object,_mockService.Object);
            var response = repo.Get();

            var okResult = response as OkObjectResult;

            // assert
            okResult.Should().NotBeNull(); ;
            okResult.StatusCode.Should().Be(200);
            var customerRes = okResult.Value as List<Customer>;
            customerRes.Should().HaveCount(n => n > 0);
            customerRes.Where(c => c.Id == 1).First().FirstName.Should().BeEquivalentTo("David");

        }

        [Fact]
        public void Get_ShouldThrowException_ForErrorResponse_FromCustomerService()
        {

            _mockService.Setup(mc => mc.GetCustomers()).Throws<FileNotFoundException>();

            CustomerController repo = new CustomerController(_mockLogger.Object,_mockService.Object);

            var response = repo.Get();

            var okResult = response as NotFoundResult;
            // assert
            okResult.Should().NotBeNull(); 
            okResult.StatusCode.Should().Be(404);

        }

        [Fact]
        public void GetCustomerById_ShouldReturnValidData_ForSuccessResponse_FromCustomerService()
        {

            var moc = new Mock<ICustomerService>();
            _mockService.Setup(mc => mc.GetCustomerById(It.IsAny<int>())).Returns(Task.FromResult(_customers[0]));

            CustomerController repo = new CustomerController(_mockLogger.Object, _mockService.Object);
            var response = repo.Get(1);

            var okResult = response as OkObjectResult;

            // assert
            okResult.Should().NotBeNull(); ;
            okResult.StatusCode.Should().Be(200);
            var customerRes = okResult.Value as Customer;
            customerRes.Id.Should().Be(1);
            customerRes.FirstName.Should().BeEquivalentTo("David");

        }
        [Fact]
        public void GetCustomerById_ShouldReturnNull_IfCustomerServiceReturns_NoDataForId()
        {

            var moc = new Mock<ICustomerService>();
            _mockService.Setup(mc => mc.GetCustomerById(It.IsAny<int>())).Returns(Task.FromResult<Customer>(null));

            CustomerController repo = new CustomerController(_mockLogger.Object,_mockService.Object);
            
            var response = repo.Get(2);

            var okResult = response as NotFoundResult;
            // assert
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(404);

        }

        [Fact]
        public void GetCustomerById_ShouldThrowException_ForErrorResponse_FromCustomerService()
        {

            var moc = new Mock<ICustomerService>();
            _mockService.Setup(mc => mc.GetCustomers()).Throws<FileNotFoundException>();

            CustomerController repo = new CustomerController(_mockLogger.Object, _mockService.Object);

            var response = repo.Get(1);

            var okResult = response as NotFoundResult;
            // assert
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(404);

        }

        [Fact]
        public void AddCustomer_ShouldReturnValidId_AfterSuccessfulInsert()
        {

            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };
            var moc = new Mock<ICustomerService>();
            _mockService.Setup(mc => mc.AddCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(customer));

            CustomerController repo = new CustomerController(_mockLogger.Object, _mockService.Object);

            var response = repo.Post(customer);

            var okResult = response as OkObjectResult;

            // assert
            okResult.Should().NotBeNull(); ;
            okResult.StatusCode.Should().Be(200);
            var actual = okResult.Value as Customer;
            actual.Id.Should().Be(2);
            actual.LastName.Should().BeEquivalentTo("Test");
            actual.Gender.Should().BeEquivalentTo("Male");

        }

        [Fact]
        public void AddCustomer_ShouldReturnException_WhenCustomerService_throwsException()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };
            var moc = new Mock<ICustomerService>();
            _mockService.Setup(mc => mc.AddCustomer(It.IsAny<Customer>())).Throws<Exception>();

            CustomerController repo = new CustomerController(_mockLogger.Object,_mockService.Object);
            var response = repo.Post(customer);

            var okResult = response as BadRequestResult;
            // assert
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(400);

        }

        [Fact]
        public void UpdateCustomer_ShouldReturnValidResponse_AfterSuccessfulUpdate()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };
            var moc = new Mock<ICustomerService>();
            _mockService.Setup(mc => mc.UpdateCustomer(It.IsAny<Customer>())).Returns(Task.FromResult(customer));

            CustomerController repo = new CustomerController(_mockLogger.Object, _mockService.Object);

            var response = repo.Put(customer);

            var okResult = response as OkObjectResult;

            // assert
            okResult.Should().NotBeNull(); ;
            okResult.StatusCode.Should().Be(200);
            var actual = okResult.Value as Customer;
            actual.Id.Should().Be(2);
            actual.LastName.Should().BeEquivalentTo("Test");
            actual.Gender.Should().BeEquivalentTo("Male");

        }

        [Fact]
        public void UpdateCustomer_ShouldReturnException_WhenCustomerService_throwsException()
        {
            Customer customer = new Customer() { Id = 2, FirstName = "Test", LastName = "Test", Gender = "Male" };
            var moc = new Mock<ICustomerService>();
            _mockService.Setup(mc => mc.UpdateCustomer(It.IsAny<Customer>())).Throws<Exception>();

            CustomerController repo = new CustomerController(_mockLogger.Object, _mockService.Object);
            var response = repo.Put(customer);

            var okResult = response as BadRequestResult;
            // assert
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(400);
        }
    }
}
