using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers;


[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var existingUser = await _userRepository.LoginUser(user);
        return Ok(existingUser);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        return Ok(user);
    }

    [HttpGet("department/{departmentId}")]
    public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
    {
        var employees = await _userRepository.GetEmployeesByDepartment(departmentId);
        return Ok(employees);
    }
}
