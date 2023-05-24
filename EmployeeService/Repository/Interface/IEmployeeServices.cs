using EmployeeService.Models;

namespace EmployeeService.Repository.Interface
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> CreateEmployee(Employee employee);
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetEmployeeBySalary(int salary);
        bool DeleteEmployee(int id);
        Task <Employee> UpdateUser(int id, Employee employee);
    }
}
