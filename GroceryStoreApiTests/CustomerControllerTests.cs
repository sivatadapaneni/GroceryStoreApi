using FluentAssertions;
using GroceryStoreApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Text;

namespace GroceryStoreApi.IntegrationTests
{
    public class CustomerControllerTests
    {
        private TestServer _server;

        public HttpClient Client { get; private set; }
        public CustomerControllerTests()
        {
            SetUpClient();
        }

        private void SetUpClient()
        {

            var builder = new WebHostBuilder()
                .UseStartup<GroceryStoreApi.Startup>();

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }


        [Fact]
        public async Task Test_GetAllCustomers()
        {
            // Get All Customers 
            var response = await Client.GetAsync("/customer");
            //response.StatusCode.Should().BeEquivalentTo(200);

            var customerList = await response.ReadBody<List<Customer>>();
            //var customerList = JsonConvert.DeserializeObject<List<Customer>>(response.Content.ReadAsStringAsync().Result);
            customerList.Should().HaveCount(n => n > 0);
            customerList.Where(c=>c.Id == 1).First().FirstName.Should().BeEquivalentTo("Bob");

        }


        [Theory]
        [InlineData(1,"Bob")]
        [InlineData(3,"Joe")]
        public async Task Test_GetCustomerById(int id, string firstName)
        {
            // Get customer by id
            var response = await Client.GetAsync(string.Format("/customer/{0}",id));
            response.StatusCode.Should().BeEquivalentTo(200);

            var customer = await response.ReadBody<Customer>();
            customer.FirstName.Should().BeEquivalentTo(firstName);

        }


        [Theory]
        [InlineData("Test","Test","Male")]
        public async Task Test_AddCustomer_SucessfulInsert(string firstName, string lastName, string gender)
        {
            // Get customer by id
            var customer = new Customer() { Id =0, FirstName = firstName, LastName = lastName, Gender = gender };
            var response = await Client.PostAsync("/customer", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().BeEquivalentTo(200);

            var actual = await response.ReadBody<Customer>();
            actual.Id.Should().BeGreaterThan(0);
            actual.FirstName.Should().BeEquivalentTo(firstName);
            actual.LastName.Should().BeEquivalentTo(lastName);
            actual.Gender.Should().BeEquivalentTo(gender);

        }
        [Theory]
        [InlineData("Test", "Test", "")]
        [InlineData("", "Test", "Male")]
        public async Task Test_AddCustomer_BadRequest(string firstName, string lastName, string gender)
        {
            // Get customer by id
            var customer = new Customer() { Id = 0, FirstName = firstName, LastName = lastName, Gender = gender };
            var response = await Client.PostAsync("/customer", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().BeEquivalentTo(400);

        }

        [Theory]
        [InlineData(2,"Test", "Test", "Male")]
        public async Task Test_UpdateCustomer_SuccesfulUpdate(int id, string firstName, string lastName, string gender)
        {
            // Get customer by id
            var customer = new Customer() { Id = id, FirstName = firstName, LastName = lastName, Gender = gender };
            var response = await Client.PutAsync("/customer", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().BeEquivalentTo(200);

            var actual = await response.ReadBody<Customer>();
            actual.Id.Should().Be(id);
            actual.FirstName.Should().BeEquivalentTo(firstName);
            actual.LastName.Should().BeEquivalentTo(lastName);
            actual.Gender.Should().BeEquivalentTo(gender);

        }

        [Theory]
        [InlineData(2, "Test", "Test", "")]
        [InlineData(2, "", "Test", "Male")]
        [InlineData(100, "Test", "Test", "Test")]
        public async Task Test_UpdateCustomer_BadRequest(int id, string firstName, string lastName, string gender)
        {
            // Get customer by id
            var customer = new Customer() { Id = id, FirstName = firstName, LastName = lastName, Gender = gender };
            var response = await Client.PutAsync("/customer", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().BeEquivalentTo(400);


        }

    }
}
