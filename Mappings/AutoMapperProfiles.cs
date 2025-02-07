using AutoMapper;
using Employee_management.Models;

namespace Employee_management.mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<AddUpdateEmployeeDto, Employee>().ReverseMap();
        }
    }
}