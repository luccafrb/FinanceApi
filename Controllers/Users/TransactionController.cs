using Microsoft.AspNetCore.Mvc;
using FinanceApi.DTOs.Create;
using FinanceApi.Services.Users;
using Microsoft.AspNetCore.Authorization;
using SQLitePCL;

namespace FinanceApi.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/transactions")]
    public class TransationController(ITransactionService service) : ControllerBase
    {
        private readonly ITransactionService _service = service;

        protected Guid UserId => Guid.Parse(User.FindFirst("id")?.Value ?? Guid.Empty.ToString());

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDto transactionCreateDto)
        {
            await _service.CreateAsync(transactionCreateDto, UserId);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var transactions = await _service.GetAllAsync(UserId);

            return transactions is null
                ? NotFound()
                : Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByAsync(Guid id)
        {
            var transactionResponseDto = _service.GetByIdAsync(id, UserId);

            return transactionResponseDto is null
                ? NotFound()
                : Ok(transactionResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(Guid id, TransactionCreateDto transactionCreateDto)
        {
            await _service.UpdateByIdAsync(id, transactionCreateDto, UserId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleByIdAsync(Guid id)
        {
            await _service.DeleteByIdAsync(id, UserId);
            return NoContent();
        }
    }


}