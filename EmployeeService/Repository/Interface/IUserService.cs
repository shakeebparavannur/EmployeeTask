using EmployeeService.Models;
using EmployeeService.Models.Dto;

namespace EmployeeService.Repository.Interface
{
    public interface IUserService
    {
        bool IsUniqueUser(string username);
        bool IsUniqueEmail(string email);
        bool IsUniquePhone(string phone);
        Task<User> CreateUser(User user);
        Task<LoginResponseDto> Login(Login login);
    }
}
