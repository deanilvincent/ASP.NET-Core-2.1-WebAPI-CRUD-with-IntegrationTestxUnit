using Microsoft.EntityFrameworkCore;
using WebApiCrudProject.Models;

namespace WebApiCrudProject.Data
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=MyAppDb001;Integrated Security=True");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
