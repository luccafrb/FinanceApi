using LedgerCore.Data;
using LedgerCore.Models;
using LedgerCore.Services.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Controllers.Admins
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/accounts")]
    public class AdminAccountController(IAdminAccountService adminAccountService) : ControllerBase
    {
        protected Guid UserId => Guid.Parse(User.FindFirst("id")?.Value!);
        private readonly IAdminAccountService _adminAccountService = adminAccountService;
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(Guid? userId)
        {
            var accounts = await _adminAccountService.GetAllAsync(userId);
            return accounts is null
                ? NotFound()
                : Ok(accounts);
        }


    }
}