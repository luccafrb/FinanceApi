using Microsoft.AspNetCore.Mvc;
using FinanceApi.DTOs.Create;
using FinanceApi.Services;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransationController(ITransactionService transactionService) : ControllerBase
    {
        private readonly ITransactionService _transactionService = transactionService;

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDto transactionCreateDto)
        {
            await _transactionService.CreateAsync(transactionCreateDto);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();

            return transactions is null
                ? NotFound()
                : Ok(transactions);
        }
    }


}