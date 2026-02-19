using AutoMapper;
using LedgerCore.DTOs.Create;
using LedgerCore.Models;

namespace LedgerCore.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryProfile, CategoryCreateDto>();
        }
    }
}