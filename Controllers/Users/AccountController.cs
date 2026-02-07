
using Microsoft.AspNetCore.Mvc;
using FinanceApi.Services;
using Microsoft.AspNetCore.Authorization;
using FinanceApi.DTOs.Create;


namespace FinanceApi.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    public class AccountController(IAccountService accountService) : ControllerBase
    {

        protected Guid UserId => Guid.Parse(User.FindFirst("id")?.Value!);
        private readonly IAccountService _accountService = accountService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAll(UserId);
            return accounts is null
                ? NotFound()
                : Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await _accountService.GetById(id);

            return account is null
                ? NotFound()
                : Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountCreateDto accountCreateDto)
        {
            var newAccount = await _accountService.CreateAsync(accountCreateDto);
            return newAccount is null
                ? NotFound()
                : Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(Guid id, AccountCreateDto accountDto)
        {
            try
            {
                await _accountService.UpdateById(id, accountDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await _accountService.DeleteById(id);
            return NoContent();
        }
    }
}