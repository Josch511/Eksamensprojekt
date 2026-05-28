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
        if (string.IsNullOrWhiteSpace(user.email) || string.IsNullOrWhiteSpace(user.password))
        {
            return BadRequest("Email and password must be filled");
        }
        
        try
        {
            var existingUser = await _userRepository.LoginUser(user);

            if (existingUser == null)
            {
                return Unauthorized("Email or password is incorrect");
            }

            return Ok(existingUser);
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);

            return Ok(user);
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }
}
