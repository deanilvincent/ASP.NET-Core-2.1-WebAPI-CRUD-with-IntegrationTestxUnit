using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCrudProject.Data;
using WebApiCrudProject.Models;

namespace WebApiCrudProject.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PayrollDbContext context;

        public EmployeeRepository(PayrollDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Employee>> Employees()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<Employee> EmployeeById(int id)
        {
            return await context.Employees.FindAsync(id);
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            try
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeById(int id)
        {
            try
            {
                var employee = await context.Employees.FindAsync(id);
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EmployeeIdExists(int id)
        {
            return await context.Employees.AnyAsync(x=>x.EmployeeId.Equals(id));
        }
    }
}
