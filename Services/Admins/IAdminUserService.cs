using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedgerCore.Services.Admins
{
    public interface IAdminUserService
    {
        public Task PromoteUserAsync(Guid userId);
    }
}