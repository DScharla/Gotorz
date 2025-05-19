using GodTur.Models.Context;
using Microsoft.EntityFrameworkCore;
using GodTur.Models;
using Shared;

namespace GodTur.Services
{
    public class UserService
    {
        private readonly TravelPackageContext _context;
        public UserService(TravelPackageContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(EmployeeDTO employeeDTO) 
        {
            Employee employee = new Employee(
                employeeDTO.FirstName,
                employeeDTO.LastName,
                employeeDTO.Email,
                employeeDTO.UserName,
                employeeDTO.Password,
                employeeDTO.EmployeeId,
                employeeDTO.Role
                );

            if (!_context.Employees.Any(employee =>
                                       employee.EmployeeId == employeeDTO.EmployeeId))
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return employee;
                
            }
            else
            {
                return null;
            }
        }
    }
}
