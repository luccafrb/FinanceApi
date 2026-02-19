using FinanceApi.Data;
using FinanceApi.DTOs.Create;
using FinanceApi.Models;
using FinanceApi.Services;
using FinanceApi.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var newUser = new UserCreateDto
            {
                Name = userRegisterDto.Name,
                Email = userRegisterDto.Email,
                Phone = userRegisterDto.Phone,
                Password = userRegisterDto.Password,
            };
            var user = _userService.CreateAsync(newUser);
            return Ok("Usuário criado.");
        }
    }
}