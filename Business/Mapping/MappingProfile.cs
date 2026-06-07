using AutoMapper;
using Business.DTOs;
using Business.DTOs.CartDTOs;
using Business.DTOs.ImageDTOs;
using Business.DTOs.OrderDTOs;
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
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Cart, opt => opt.Ignore());
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductId, o => o.MapFrom(s => s.Id))
                .ForMember(p => p.Images, o => o.MapFrom(s => s.Images));
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.OrderItemId, o => o.MapFrom(s => s.Id));
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.OrderId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.OrderDate, o => o.MapFrom(s => s.OrderTime))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.OrderItems));
            CreateMap<Order, OrderDetailDto>()
                .ForMember(d => d.Items, o => o.MapFrom(s => s.OrderItems));
            CreateMap<User, UserUpdateDto>()
                .ForMember(a => a.UserId, o => o.MapFrom(s => s.Id));
            CreateMap<UserUpdateDto, User>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.UserId));
            CreateMap<User, UserDto>()
               .ForMember(a => a.UserId, o => o.MapFrom(s => s.Id));
            CreateMap<UserDto, User>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.UserId));
            CreateMap<User, UserCreateDto>().ReverseMap(); 
            CreateMap<PImage, PImageDto>().ReverseMap();
            CreateMap<PImage, AddImgDto>().ReverseMap();
            CreateMap<PImage, UpdImgDto>().ReverseMap();
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Products, o => o.Ignore());
            CreateMap<CategoryDto, Category>()
                .ForMember(d => d.Id, o => o.MapFrom(a => a.CategoryId));
            CreateMap<Category, CatCreateDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.CartItemId,
                 opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId,
                 opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName,
                 opt => opt.MapFrom(src => src.Product.PName))
                .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.Product.PPrice))
                .ForMember(dest => dest.Quantity,
                opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.TotalPrice,
                opt => opt.MapFrom(src => src.Quantity * src.Product.PPrice))
                .ForMember(dest => dest.Stock,
                opt => opt.MapFrom(s => s.Product.PStock));
            CreateMap<Cart, CartDto>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems)).ReverseMap();
        }
    }
}
