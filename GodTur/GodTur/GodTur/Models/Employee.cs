namespace GodTur.Models
{
    public class Employee : User
    {
        public Employee (string firstName, string lastName, string email, string userName, string passwordHash, int employeeId, string role) : base(firstName, lastName, email, userName, passwordHash)
        {
            EmployeeId = employeeId;
            Role = role;
        }
        public int EmployeeId { get; set; }
        public string Role { get; set; }


    }
}
