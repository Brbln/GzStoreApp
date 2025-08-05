using AutoMapper;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
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
        }
    }
}
