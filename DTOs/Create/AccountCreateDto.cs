using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApi.DTOs.Create
{
    public class AccountCreateDto
    {
        public Guid UserId { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}