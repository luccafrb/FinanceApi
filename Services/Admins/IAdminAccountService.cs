using LedgerCore.Models;

namespace LedgerCore.Services.Admins
{
    public interface IAdminAccountService
    {
        public Task<IEnumerable<Account>> GetAllAsync(Guid? userId);
    }
}