using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
using Tryitter.Services;

namespace Tryitter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Auth([FromBody] AuthRequest userAuth)
    {
        var response = await _userService.GetAuth(userAuth);
        return Ok(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUser();
        return Ok(users);
    }
    
    [HttpGet("byId/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetAllUserById(id);
        return Ok(user);
    }
    
    [HttpGet("byName/{userName}")]
    public async Task<IActionResult> GetUserByUserName(string userName)
    {
        var user = await _userService.GetAllUserByName(userName);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
    {
        var userCreated = await _userService.CreateUser(userRequest);
        return Ok(userCreated);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest userRequest)
    {
        var userUpdated = await _userService.UpdateUser(id, userRequest);

        return Ok(userUpdated);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userDeleted = await _userService.DeleteUser(id);

        return Ok(userDeleted);
    }
}