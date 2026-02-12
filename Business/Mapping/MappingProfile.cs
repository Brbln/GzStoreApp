using AutoMapper;
using Business.DTOs;
using Business.DTOs.ProductDTOs;
using Business.DTOs.userDto;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<AddCartItemDto, CartItem>()
         .ForMember(dest => dest.Id, opt => opt.Ignore())
         .ForMember(dest => dest.Product, opt => opt.Ignore());  

            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<Product, ProductDto>(); 
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.PName))
                .ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<Seller, SellerDto>().ReverseMap();
            CreateMap<PImage, PImageDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CatCreateDto>().ReverseMap();
        }
    }
}
