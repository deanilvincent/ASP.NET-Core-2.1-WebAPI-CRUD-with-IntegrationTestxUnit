using System.ComponentModel.DataAnnotations;

namespace WebApiCrudProject.Models
{
    public class Employee
    {
        [Key] public int EmployeeId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Position { get; set; }
    }
}
