using FinanceApi.DTOs;
using FinanceApi.DTOs.Create;

namespace FinanceApi.Services.Users;

public interface IAuthService
{
    Task<string> LoginAsync(UserLoginDto loginDto);
}