using AutoMapper;
using Business.DTOs;
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
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.PName))
                .ReverseMap();

            CreateMap<Cart, CartDto>()
                .ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.PName))
                .ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User,UserCreateDto>().ReverseMap();

            CreateMap<PImage, PImageDto>().ReverseMap();
        }
    }
}
