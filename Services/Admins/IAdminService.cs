namespace FinanceApi.Services.Admins
{
    public interface IAdminService
    {
        public Task PromoteUserAsync(Guid userId);
    }
}