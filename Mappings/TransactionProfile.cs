using AutoMapper;
using LedgerCore.DTOs.Responses;
using LedgerCore.DTOs.Create; // Adicione o using do seu DTO de criação
using LedgerCore.Models;

namespace LedgerCore.Mappings
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