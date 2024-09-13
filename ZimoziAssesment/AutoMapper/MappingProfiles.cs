using AutoMapper;
using ZimoziAssesment.Data.Entities;
using ZimoziAssesment.Dto;

namespace ZimoziAssesment.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddOrUpdateEmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
