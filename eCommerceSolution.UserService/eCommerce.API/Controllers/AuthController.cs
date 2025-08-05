using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/[controller]")] // api/auth
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")] //api/auth/register
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        //Check if RegisterRequest is null

        if (registerRequest == null)
            return BadRequest("Invalid registration data");

        //Call the UserService to handle registration

        AuthenticationResponse? authenticationResponse = await _userService.Register(registerRequest);

        if (authenticationResponse == null || authenticationResponse.Success == false)
            return BadRequest(authenticationResponse);

        return Ok(authenticationResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        //Check if LoginRequest is null
        if (loginRequest == null)
            return BadRequest("Invalid login request data");

        AuthenticationResponse? authenticationResponse = await _userService.Login(loginRequest);

        if(authenticationResponse == null || authenticationResponse.Success == false)
            return Unauthorized(authenticationResponse);

        return Ok(authenticationResponse);           
    }  
}
