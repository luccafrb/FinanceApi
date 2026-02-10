using FinanceApi.Models;

namespace FinanceApi.Services.Admins
{
    public interface IAdminAccountService
    {
        public Task PromoteUserAsync(Guid userId);
        public Task<IEnumerable<Account>> GetAllAsync(Guid? userId);
    }
}