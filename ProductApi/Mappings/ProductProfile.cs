using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Models;
using ProductApi.DTOs;


namespace ProductApi.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
             CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Product, ProductDetailsDto>();
        }
        
    }
}