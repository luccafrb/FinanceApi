using FinanceApi.Services.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers.Admins
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/users")]
    public class AdminsUserController(IAdminUserService adminUserService) : ControllerBase
    {
        private readonly IAdminUserService _adminUserService = adminUserService;

        [HttpPost("{id}/promote")]
        public async Task<IActionResult> PromoteToAdmin(Guid id)
        {
            await _service.PromoteUserAsync(id);
            return NoContent();
        }
    }
}