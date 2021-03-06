using Logic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository) => _userRepository = userRepository;

    [HttpGet("[controller]")]
    public async Task<IActionResult> GetUsersAsync() => Ok(await _userRepository.GetUsersAsync());
}
