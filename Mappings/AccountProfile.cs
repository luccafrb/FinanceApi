using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LedgerCore.DTOs.Responses;
using LedgerCore.Models;

namespace LedgerCore.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountResponseDto>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src =>
                    src.Transactions.Any() ?
                        src.Transactions
                        .Sum(t => t.Type == TransactionType.Income ? t.Value : -t.Value) : 0));
        }
    }
}