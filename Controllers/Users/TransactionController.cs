using Microsoft.AspNetCore.Mvc;
using FinanceApi.DTOs.Create;
using FinanceApi.Services.Users;
using Microsoft.AspNetCore.Authorization;

namespace FinanceApi.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/transactions")]
    public class TransationController(ITransactionService transactionService) : ControllerBase
    {
        private readonly ITransactionService _transactionService = transactionService;

        protected Guid UserId => Guid.Parse(User.FindFirst("id")?.Value ?? Guid.Empty.ToString());

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDto transactionCreateDto)
        {
            await _transactionService.CreateAsync(transactionCreateDto, UserId);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync(UserId);

            return transactions is null
                ? NotFound()
                : Ok(transactions);
        }
    }


}