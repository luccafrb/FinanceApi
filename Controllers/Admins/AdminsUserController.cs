using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers.Admins
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/accounts")]
    public class AdminsUserController(IAdminService service) : ControllerBase
    {
        private readonly IAdminService _service = service;

        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(Guid userId)
        {
            await _service.PromoteUserAsync(userId);
            return NoContent();
        }
    }
}