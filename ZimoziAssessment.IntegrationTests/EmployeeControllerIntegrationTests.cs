using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ZimoziAssesment.Dto;
using ZimoziAssesment.Error;

namespace ZimoziAssessment.IntegrationTests
{
    public class EmployeeControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmployeeControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnOkResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/Employees");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var employees = await response.Content.ReadFromJsonAsync<Response<List<EmployeeDto>>>();
            employees.Should().NotBeNull();
            employees!.StatusCode.Should().Be(200);
            employees.Data.Should().BeOfType<List<EmployeeDto>>();
        }

        [Fact]
        public async Task AddEmployee_ShouldReturnCreatedResponse()
        {
            // Arrange
            var newEmployee = new AddOrUpdateEmployeeDto { Name = "John Doe", Department = "IT", Salary = 50000 };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Employees", newEmployee);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<Response<EmployeeDto>>();
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Data.Name.Should().Be("John Doe");
        }
    }
}
