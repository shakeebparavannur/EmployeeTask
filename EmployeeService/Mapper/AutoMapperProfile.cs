using AutoMapper;
using EmployeeService.Models;
using EmployeeService.Models.Dto;

namespace EmployeeService.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}
