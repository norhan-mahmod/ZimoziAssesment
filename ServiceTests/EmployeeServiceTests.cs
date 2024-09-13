using AutoMapper;
using FluentAssertions;
using Moq;
using ZimoziAssesment.Data.Entities;
using ZimoziAssesment.Dto;
using ZimoziAssesment.Repositories.UnitOfWorkRepo;
using ZimoziAssesment.Services;

namespace ServiceTests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly EmployeeService employeeService;
        public EmployeeServiceTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            employeeService = new EmployeeService(unitOfWorkMock.Object, mapperMock.Object);
        }

        // unit test for Get All employees
        [Fact]
        public async Task GetAllEmployees_ShouldReturn_SuccessResponse_WhenEmployeesAreRetrived()
        {
            // Arrange
            var employees = new List<Employee>()
            {
                new Employee(){ EmployeeID = 1, Name = "John Doe", Department = "IT", Salary = 50000 },
                new Employee { EmployeeID = 2, Name = "Jane Smith", Department = "HR", Salary = 60000 }
            };

            var employeesDto = new List<EmployeeDto>()
            {
                new EmployeeDto(){ EmployeeID = 1, Name = "John Doe", Department = "IT", Salary = 50000 },
                new EmployeeDto() { EmployeeID = 2, Name = "Jane Smith", Department = "HR", Salary = 60000 }
            };

            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAllAsync()).ReturnsAsync(employees);
            mapperMock.Setup(m => m.Map<List<EmployeeDto>>(employees)).Returns(employeesDto);


            // Act
            var result = await employeeService.GetAllEmployees();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Message.Should().Be("Get Employees successfully");
            result.Data.Should().BeEquivalentTo(employeesDto);

        }
        [Fact]
        public async Task GetAllEmployees_ShouldReturn_ErrorResponse_WhenExceptionIsThrown()
        {
            // Arrange
            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAllAsync()).Throws(new Exception("An error occurred")); 

            // Act
            var result = await employeeService.GetAllEmployees();

            // Assert
            result.StatusCode.Should().Be(400);
            result.Message.Should().Contain("An error occurred");
            result.Data.Should().BeEquivalentTo(new List<EmployeeDto>());
        }

        // unit test for Add Employee
        [Fact]
        public async Task AddEmployee_ShouldReturn_SuccessResponse_WhenEmployeeIsAdded()
        {
            // Arrange
            var employeeDto = new AddOrUpdateEmployeeDto() { Name = "John Doe", Department = "IT", Salary = 50000 };
            var employee = new Employee() { EmployeeID = 1, Name = "John Doe", Department = "IT", Salary = 50000 };
            var returnDto = new EmployeeDto() { EmployeeID = 1, Name = "John Doe", Department = "IT", Salary = 50000 };

            mapperMock.Setup(m => m.Map<Employee>(employeeDto)).Returns(employee);
            mapperMock.Setup(m => m.Map<EmployeeDto>(employee)).Returns(returnDto);
            unitOfWorkMock.Setup(u => u.Repository<Employee>().Add(employee)).Verifiable();
            unitOfWorkMock.Setup(u => u.Save()).ReturnsAsync(1);

            // Act
            var result = await employeeService.AddEmployee(employeeDto);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Message.Should().Be("Employee added successfully");
            result.Data.Should().BeEquivalentTo(returnDto);
        }

        [Fact]
        public async Task AddEmployee_ShouldReturn_ErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var employeeDto = new AddOrUpdateEmployeeDto() { Name = "John Doe", Department = "IT", Salary = 50000 };

            mapperMock.Setup(m => m.Map<Employee>(employeeDto)).Throws(new Exception("An error occurred"));

            // Act
            var result = await employeeService.AddEmployee(employeeDto);

            // Assert
            result.StatusCode.Should().Be(400);
            result.Message.Should().Contain("An error occurred");
            result.Data.Should().BeEquivalentTo(new EmployeeDto());
        }

        // unit test for Delete Employee
        [Fact]
        public async Task DeleteEmployee_ShouldReturn_SuccessResponse_WhenEmployeeIsDeleted()
        {
            // Arrange
            int id = 1;
            var employee = new Employee() { EmployeeID = id, Name = "John Doe", Department = "IT", Salary = 50000 };
            var employeeDto = new EmployeeDto() { EmployeeID = id, Name = "John Doe", Department = "IT", Salary = 50000 };

            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAsyncById(id)).ReturnsAsync(employee);
            unitOfWorkMock.Setup(u => u.Save()).ReturnsAsync(1);
            mapperMock.Setup(m => m.Map<EmployeeDto>(employee)).Returns(employeeDto);

            // Act
            var result = await employeeService.DeleteEmployee(id);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Message.Should().Be("Employee Deleted successfully");
            result.Data.Should().BeEquivalentTo(employeeDto);

        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturn_NotFoundResponse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            int id = 1;
            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAsyncById(id)).ReturnsAsync((Employee) null);

            // Act
            var result = await employeeService.DeleteEmployee(id);

            // Assert
            result.StatusCode.Should().Be(404);
            result.Message.Should().Be("There is No Employee with that Id");
            result.Data.Should().BeEquivalentTo(new EmployeeDto());
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturn_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAsyncById(id)).Throws(new Exception("An error occurred"));

            // Act
            var result = await employeeService.DeleteEmployee(id);

            // Assert
            result.StatusCode.Should().Be(400);
            result.Message.Should().Contain("An error occurred");
            result.Data.Should().BeEquivalentTo(new EmployeeDto());
        }

        // unit test for Update Employee
        [Fact]
        public async Task UpdateEmployee_ShouldReturn_SuccessResponse_WhenEmployeeIsUpdated()
        {
            // Arrang
            int id = 1;
            var employeeDto = new AddOrUpdateEmployeeDto() { Name = "John Doe", Department = "HR", Salary = 40000 };
            var employee = new Employee() { EmployeeID = id , Name = "John Doe", Department = "IT", Salary = 50000 };
            var updatedEmployeeDto = new EmployeeDto() { EmployeeID = id, Name = "John Doe", Department = "HR", Salary = 40000 };

            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAsyncById(id)).ReturnsAsync(employee);
            unitOfWorkMock.Setup(u => u.Save()).ReturnsAsync(1);
            mapperMock.Setup(m => m.Map<EmployeeDto>(employee)).Returns(updatedEmployeeDto);

            // Act
            var result = await employeeService.UpdateEmployee(id, employeeDto);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Message.Should().Be("Employee Updated successfully");
            result.Data.Should().BeEquivalentTo(updatedEmployeeDto);
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturn_NotFoundResponse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            int id = 1;
            var employeeDto = new AddOrUpdateEmployeeDto() { Name = "John Doe", Department = "HR", Salary = 40000 };

            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAsyncById(id)).ReturnsAsync((Employee)null);

            // Act
            var result = await employeeService.UpdateEmployee(id, employeeDto);

            // Assert
            result.StatusCode.Should().Be(404);
            result.Message.Should().Be("There is No Employee with that Id");
            result.Data.Should().BeEquivalentTo(new EmployeeDto());
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturn_ErrorResponse_WhenExceptionIsThrown()
        {
            // Arrang
            int id = 1;
            var employeeDto = new AddOrUpdateEmployeeDto() { Name = "John Doe", Department = "HR", Salary = 40000 };

            unitOfWorkMock.Setup(u => u.Repository<Employee>().GetAsyncById(id)).Throws(new Exception("An error occurred"));

            // Act
            var result = await employeeService.UpdateEmployee(id, employeeDto);

            // Assert
            result.StatusCode.Should().Be(400);
            result.Message.Should().Contain("An error occurred");
            result.Data.Should().BeEquivalentTo(new EmployeeDto());
        }

    }
}