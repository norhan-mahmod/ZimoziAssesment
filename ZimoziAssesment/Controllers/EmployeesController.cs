using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZimoziAssesment.Dto;
using ZimoziAssesment.Error;
using ZimoziAssesment.Services;

namespace ZimoziAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        [HttpGet]
        [Produces(typeof(Response<List<EmployeeDto>>))]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await employeeService.GetAllEmployees();
            return result.GetResponse(this);
        }
        [HttpPost]
        [Produces(typeof(Response<EmployeeDto>))]
        public async Task<IActionResult> AddEmployee(AddOrUpdateEmployeeDto employeeDto)
        {
            var result = await employeeService.AddEmployee(employeeDto);
            return result.GetResponse(this);
        }
        [HttpPut("{id}")]
        [Produces(typeof(Response<EmployeeDto>))]
        public async Task<IActionResult> UpdateEmployee(int id , AddOrUpdateEmployeeDto employeeDto)
        {
            var result = await employeeService.UpdateEmployee(id, employeeDto);
            return result.GetResponse(this);
        }
        [HttpDelete("{id}")]
        [Produces(typeof(Response<EmployeeDto>))]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await employeeService.DeleteEmployee(id);
            return result.GetResponse(this);
        }
    }
}
