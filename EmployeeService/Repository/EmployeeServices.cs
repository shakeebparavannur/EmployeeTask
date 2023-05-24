using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repository
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly EmployeeContext context;

        public EmployeeServices(EmployeeContext context)
        {
            this.context = context;
        }
        public async Task<Employee> CreateEmployee(Employee employee)
        {
            if(employee == null)
            {
                return null;
            }
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        public bool DeleteEmployee(int id)
        {
           var employee = context.Employees.FirstOrDefault(x => x.Id == id);
            if(employee == null)
            {
                return false;
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees =await context.Employees.ToListAsync();
            if(employees == null)
            {
                return null;
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(emp=>emp.Id == id);
            if(employee == null)
            {
                return null;
            }
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeBySalary(int sal)
        {
           var employees = await context.Employees.Where(emp=>emp.Salary>=sal).ToListAsync();
            if(employees == null)
            {
                return null;
            }
            return employees;
        }

        public async Task<Employee> UpdateUser(int id, Employee employee)
        {
            var emp = await context.Employees.FirstOrDefaultAsync(e=>e.Id == id);
            if(emp == null)
            {
                return null;
            }
            return emp;
        }
    }
}
