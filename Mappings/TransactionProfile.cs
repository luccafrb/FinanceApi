using AutoMapper;
using FinanceApi.DTOs.Responses;
using FinanceApi.DTOs.Create; // Adicione o using do seu DTO de criação
using FinanceApi.Models;

namespace FinanceApi.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            // Saída (GET)
            CreateMap<Transaction, TransactionResponseDto>();
            CreateMap<Transaction, AccountTransactionResponseDto>();

            // Entrada (POST/CREATE) - ADICIONE ESTA LINHA:
            CreateMap<TransactionCreateDto, Transaction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}