using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers;


[ApiController]
[Route("user")]
public class UserCustomerController : ControllerBase
{
    private readonly IUserCustomerRepository _userRepository;

    public UserCustomerController(IUserCustomerRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserCustomer user)
    {
        var existingUser = await _userRepository.LoginUser(user);
        return Ok(existingUser);
    }
}
