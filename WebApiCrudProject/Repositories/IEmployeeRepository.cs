using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCrudProject.Models;

namespace WebApiCrudProject.Repositories
{
    public interface IEmployeeRepository
    {
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> DeleteEmployeeById(int id);
        Task<Employee> EmployeeById(int id);
        Task<List<Employee>> Employees();
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> EmployeeIdExists(int id);
    }
}