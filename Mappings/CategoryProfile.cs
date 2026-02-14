using AutoMapper;
using FinanceApi.DTOs.Create;
using FinanceApi.Models;

namespace FinanceApi.Mappings
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