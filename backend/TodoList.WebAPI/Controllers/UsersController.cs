using Microsoft.AspNetCore.Mvc;
using TodoList.BLL.Interfaces;
using TodoList.Common.DTO.User;

namespace TodoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] NewUserDto newUserDto)
        {
            var newUser = await _userService.RegisterAsync(newUserDto);
            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userService.LoginAsync(userLoginDto);
            return Ok(user);
        }
    }
}
