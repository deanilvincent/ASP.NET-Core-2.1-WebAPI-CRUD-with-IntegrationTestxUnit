using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using WebApiCrudProject.Models;
using Xunit;

namespace WebApiCrudProject.Tests
{
    public class EmployeesControllerTests
    {
        private readonly HttpClient httpClient;
        private HttpContent httpContent;
        private const string ApiUrl = "/api/employees/";

        public EmployeesControllerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            httpClient = server.CreateClient();
        }

        [Fact]
        public void Get_GetAllEmployees_ReturnOk()
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), ApiUrl);

            //act
            var response = httpClient.SendAsync(request).Result;

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // Should have existing ID and so return OK
        [Theory]
        [InlineData(2)]
        public void Get_GetEmployeeByIdSavedInDatabase_ReturnsOk(int id)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"{ApiUrl}{id}");

            // act
            var response = httpClient.SendAsync(request).Result;

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // Shouldn't have existing ID so return BadRequest
        [Theory]
        [InlineData(1)]
        public void Get_GetEmployeeByIdNOTSavedInDatabase_ReturnsBadRequest(int id)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"{ApiUrl}{id}");

            // act 
            var response = httpClient.SendAsync(request).Result;

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_PostEmployeeDetails_ReturnOk()
        {
            // arrange
            var employee = new Employee
            {
                Firstname = "Jessica",
                Lastname = "Brown",
                Position = "Sales"
            };

            var serializeEmployee = JsonConvert.SerializeObject(employee);
            httpContent = new StringContent(serializeEmployee);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var response = await httpClient.PostAsync(ApiUrl, httpContent);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public async Task Update_PutEmployeeDetailByIdSavedFromDb_ReturnOk(int id)
        {
            // arrange
            var employee = new Employee
            {
                Firstname = "Jessica Updated",
                Lastname = "Brown Updated",
                Position = "Sales Updated"
            };

            var serializeEmployee = JsonConvert.SerializeObject(employee);
            httpContent = new StringContent(serializeEmployee);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var response = await httpClient.PutAsync($"{ApiUrl}{id}", httpContent);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(11)]
        public async Task Update_PutEmployeeDetailByIdNotSavedFromDb_ReturnBadRequest(int id)
        {
            // arrange
            var employee = new Employee
            {
                Firstname = "Update",
                Lastname = "Update",
                Position = "Update"
            };

            var serializeObject = JsonConvert.SerializeObject(employee);
            httpContent = new StringContent(serializeObject);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var response = await httpClient.PutAsync($"{ApiUrl}{id}", httpContent);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

