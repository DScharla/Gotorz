using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodTur.Models
{
    public class Employee : User
    {
        public Employee (string firstName, string lastName, string email, string userName, string passwordHash, int employeeId, string role) : base(firstName, lastName, email, userName, passwordHash)
        {
            EmployeeId = employeeId;
            Role = role;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        public string Role { get; set; }


    }
}
