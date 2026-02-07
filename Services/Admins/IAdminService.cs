namespace FinanceApi.Services
{
    public interface IAdminService
    {
        public Task PromoteUserAsync(Guid userId);
    }
}