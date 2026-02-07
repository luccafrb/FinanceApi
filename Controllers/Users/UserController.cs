
using Microsoft.AspNetCore.Mvc;
using FinanceApi.DTOs.Create;
using FinanceApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace FinanceApi.Controllers.Admins
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService, IAccountService accountService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAccountService _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            var user = await _userService.CreateAsync(userCreateDto);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is null
                ? NotFound()
                : Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserCreateDto userDto)
        {
            await _userService.Update(id, userDto);
            return Ok();
        }

    }
}