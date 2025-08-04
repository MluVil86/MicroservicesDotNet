using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userID}")]
    public async Task<IActionResult> GetUserByUserID(Guid userID) 
    {
        if (userID == Guid.Empty)
            return BadRequest("Invalid User ID");

        GetUserRequest? response = await _userService.GetByUserID(userID);

        if (response == null)
            return NotFound();

        return Ok(response);
    
    }

    [HttpGet]
    public IActionResult Get()
    {
        

        return Ok("You're not crzy there is something wring");

    }
}
