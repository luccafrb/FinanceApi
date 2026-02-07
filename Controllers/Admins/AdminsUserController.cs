using FinanceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers.Admins
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/users")]
    public class AdminsUserController(IAdminService service) : ControllerBase
    {
        private readonly IAdminService _service = service;

        [HttpPost("{id}/promote")]
        public async Task<IActionResult> PromoteToAdmin(Guid id)
        {
            await _service.PromoteUserAsync(id);
            return NoContent();
        }
    }
}