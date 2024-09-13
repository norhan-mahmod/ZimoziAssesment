using AutoMapper;
using ZimoziAssesment.Data.Entities;
using ZimoziAssesment.Dto;
using ZimoziAssesment.Error;
using ZimoziAssesment.Repositories.UnitOfWorkRepo;

namespace ZimoziAssesment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Response<EmployeeDto>> AddEmployee(AddOrUpdateEmployeeDto employeeDto)
        {
            try
            {
                var employee = mapper.Map<Employee>(employeeDto);
                await unitOfWork.Repository<Employee>().Add(employee);
                await unitOfWork.Save();
                var result = mapper.Map<EmployeeDto>(employee);
                return new Response<EmployeeDto>(200, result, "Employee added successfully");
            }
            catch(Exception ex)
            {
                return new Response<EmployeeDto>(400, new EmployeeDto(), ex.InnerException.Message);
            }
        }

        public async Task<Response<EmployeeDto>> DeleteEmployee(int id)
        {
            try
            {
                var employee = await unitOfWork.Repository<Employee>().GetAsyncById(id);
                if (employee is null)
                    return new Response<EmployeeDto>(404, new EmployeeDto(), "There is No Employee with that Id");
                unitOfWork.Repository<Employee>().Delete(employee);
                await unitOfWork.Save();
                var result = mapper.Map<EmployeeDto>(employee);
                return new Response<EmployeeDto>(200, result, "Employee Deleted successfully");
            }
            catch (Exception ex)
            {
                return new Response<EmployeeDto>(400, new EmployeeDto(), ex.InnerException.Message);
            }
        }

        public async Task<Response<List<EmployeeDto>>> GetAllEmployees()
        {
            try
            {
                var employees = await unitOfWork.Repository<Employee>().GetAllAsync();
                var result = mapper.Map<List<EmployeeDto>>(employees);
                return new Response<List<EmployeeDto>>(200, result, "Get Employees successfully");
            }
            catch (Exception ex)
            {
                return new Response<List<EmployeeDto>>(400, new List<EmployeeDto>(), ex.InnerException.Message);
            }
        }

        public async Task<Response<EmployeeDto>> UpdateEmployee(int id ,AddOrUpdateEmployeeDto employeeDto)
        {
            try
            {
                var employee = await unitOfWork.Repository<Employee>().GetAsyncById(id);
                if (employee is null)
                    return new Response<EmployeeDto>(404, new EmployeeDto(), "There is No Employee with that Id");
                employee.Name = employeeDto.Name;
                employee.Department = employeeDto.Department;
                employee.Salary = employeeDto.Salary;
                await unitOfWork.Save();
                var result = mapper.Map<EmployeeDto>(employee);
                return new Response<EmployeeDto>(200, result, "Employee Updated successfully");
            }
            catch(Exception ex)
            {
                return new Response<EmployeeDto>(400, new EmployeeDto(), ex.InnerException.Message);
            }
        }
    }
}
