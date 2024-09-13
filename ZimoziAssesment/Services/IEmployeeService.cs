using ZimoziAssesment.Dto;
using ZimoziAssesment.Error;

namespace ZimoziAssesment.Services
{
    public interface IEmployeeService
    {
        Task<Response<List<EmployeeDto>>> GetAllEmployees();
        Task<Response<EmployeeDto>> AddEmployee(AddOrUpdateEmployeeDto employeeDto);
        Task<Response<EmployeeDto>> UpdateEmployee(int id, AddOrUpdateEmployeeDto employeeDto);
        Task<Response<EmployeeDto>> DeleteEmployee(int id);
    }
}
