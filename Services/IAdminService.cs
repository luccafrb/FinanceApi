using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApi.Services
{
    public interface IAdminService
    {
        public Task PromoteUserAsync(Guid userId);
    }
}