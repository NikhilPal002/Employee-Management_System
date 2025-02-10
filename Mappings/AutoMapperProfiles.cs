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
            CreateMap<Leave, LeaveDto>().ReverseMap();
            CreateMap<AddLeaveDto, Leave>().ReverseMap();
            CreateMap<UpdateLeaveDto, Leave>().ReverseMap();
            CreateMap<UpdateLeaveStatusDto, Leave>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<CheckInDto, Attendance>().ReverseMap();
            CreateMap<CheckOutDto, Attendance>().ReverseMap();
        }
    }
}