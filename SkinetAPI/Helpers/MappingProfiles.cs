using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using SkinetAPI.Dtos;

namespace SkinetAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(c => c.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(c => c.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(c => c.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
