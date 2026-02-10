using FinanceApi.Models;

namespace FinanceApi.Services.Admins
{
    public interface IAdminService
    {
        public Task PromoteUserAsync(Guid userId);
        public Task<IEnumerable<Account>> GetAllAsync(Guid? userId);
    }
}