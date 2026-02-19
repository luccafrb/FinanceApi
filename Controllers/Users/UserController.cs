using Microsoft.AspNetCore.Mvc;
using LedgerCore.DTOs.Create;
using LedgerCore.Services.Users;
using Microsoft.AspNetCore.Authorization;

namespace LedgerCore.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService, IAccountService accountService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAccountService _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserCreateDto userCreateDto)
        {
            var user = await _userService.CreateAsync(userCreateDto);
            return CreatedAtAction(nameof(CreateAsync), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is null
                ? NotFound()
                : Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UserCreateDto userDto)
        {
            await _userService.UpdateAsync(id, userDto);
            return Ok();
        }

    }
}