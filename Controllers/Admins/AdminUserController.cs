using LedgerCore.Services.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedgerCore.Controllers.Admins
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/users")]
    public class AdminUserController(IAdminUserService service) : ControllerBase
    {
        private readonly IAdminUserService _service = service;

        [HttpPost("{id}/promote")]
        public async Task<IActionResult> PromoteToAdmin(Guid id)
        {
            await _service.PromoteUserAsync(id);
            return NoContent();
        }
    }
}