
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LedgerCore.DTOs.Create;
using LedgerCore.Services.Users;


namespace LedgerCore.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    public class AccountController(IAccountService accountService) : ControllerBase
    {

        protected Guid UserId => Guid.Parse(User.FindFirst("id")?.Value!);
        private readonly IAccountService _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var accounts = await _accountService.GetAllAsync(UserId);
            return accounts is null
                ? NotFound()
                : Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var account = await _accountService.GetByIdAsync(id, UserId);

            return account is null
                ? NotFound()
                : Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AccountCreateDto accountCreateDto)
        {
            var newAccount = await _accountService.CreateAsync(accountCreateDto, UserId);
            return newAccount is null
                ? NotFound()
                : Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(Guid id, AccountCreateDto accountDto)
        {
            try
            {
                await _accountService.UpdateByIdAsync(accountDto, id, UserId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            await _accountService.DeleteByIdAsync(id, UserId);
            return NoContent();
        }
    }
}