using LedgerCore.DTOs;
using LedgerCore.DTOs.Create;

namespace LedgerCore.Services.Users;

public interface IAuthService
{
    Task<string> LoginAsync(UserLoginDto loginDto);
}