using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApi.DTOs.Responses
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public List<AccountSummaryDto>? Accounts { get; set; }
    }

    public class AccountSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}