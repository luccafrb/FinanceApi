using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedgerCore.Controllers.Admins
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/accounts")]
    public class AdminsTransationController : ControllerBase
    {

    }
}