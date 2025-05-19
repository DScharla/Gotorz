using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using GodTur.Models;
using GodTur.Services;

namespace GodTur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost, Route("Create")]
        public async Task<ActionResult<EmployeeDTO>> PostCreateEmployeeAsync([FromBody] EmployeeDTO employeeDTO)
        {
            Employee createdEmployee = await _userService.CreateEmployeeAsync(employeeDTO);
            if (createdEmployee == null)
                return BadRequest("Unable to create user. Check that the user already exists.");

            return Ok(employeeDTO);
        }
    }
}
