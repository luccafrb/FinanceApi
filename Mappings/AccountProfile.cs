using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceApi.DTOs.Responses;
using FinanceApi.Models;

namespace FinanceApi.Mappings
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

            CreateMap<Transaction, AccountTransactionResponseDto>();
        }
    }
}