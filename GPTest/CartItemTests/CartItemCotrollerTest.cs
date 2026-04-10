using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;
using GamzeProje.Controllers;
using System.Collections.Generic;
using WebAPI.Controllers;
using Business.DTOs.CartDTOs;

namespace GPTest.CartItemTests
{
    public class CartItemsControllerTest
    {
        private readonly Mock<ICartItemService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CartItemsController _controller;

        public CartItemsControllerTest()
        {
            _mockService = new Mock<ICartItemService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CartItemsController(_mockService.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetCartItems_ReturnsNotFound_WhenCartIsEmpty()
        {
            // Arrange
            int cartId = 1;
            _mockService.Setup(s => s.GetCartItemsDto(cartId)).Returns(new List<CartItemDto>());

            // Act
            var result = _controller.GetCartItems(cartId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Sepet boş.", notFoundResult.Value);
        }

        [Fact]
        public void GetCartItems_ReturnsCartDto_WhenCartHasItems()
        {
            // Arrange
            int cartId = 1;
            var items = new List<CartItemDto>
        {
            new CartItemDto { CartItemId = 1, ProductName = "Ürün1", Quantity = 2, UnitPrice = 10 }
        };
            _mockService.Setup(s => s.GetCartItemsDto(cartId)).Returns(items);

            // Act
            var result = _controller.GetCartItems(cartId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var cartDto = Assert.IsType<CartDto>(okResult.Value);
            Assert.Equal(cartId, cartDto.CartId);
            Assert.Equal(20, cartDto.TotalAmount); // 2*10 = 20
        }

        //[Fact]
        //public void Add_ReturnsOk_WhenModelIsValid()
        //{
        //    // Arrange
        //    var addDto = new Business.DTOs.AddCartItemDto
        //    {
        //        CartId = 1,
        //        ProductId = 1,
        //        Quantity = 2
        //    };

        //    _mockMapper.Setup(m => m.Map<CartItem>(addDto)).Returns(new CartItem
        //    {
        //        CartId = addDto.CartId,
        //        ProductId = addDto.ProductId,
        //        Quantity = addDto.Quantity
        //    });

        //    // Act
        //    var result = _controller.Add(addDto);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal("Sepete ürün başarıyla eklendi.", okResult.Value);
        //    _mockService.Verify(s => s.AddOrUpdate(It.IsAny<CartItem>()), Times.Once);
        //}

        [Fact]
        public void Delete_ReturnsNotFound_WhenCartItemDoesNotExist()
        {
            // Arrange
            int cartItemId = 1;
            _mockService.Setup(s => s.GetById(cartItemId)).Returns((CartItem)null);

            // Act
            var result = _controller.Delete(cartItemId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //[Fact]
        //public void Delete_ReturnsOk_WhenCartItemExists()
        //{
        //    // Arrange
        //    int cartItemId = 1;
        //    var cartItem = new CartItem { CartItemId = cartItemId };
        //    _mockService.Setup(s => s.GetById(cartItemId)).Returns(cartItem);

        //    // Act
        //    var result = _controller.Delete(cartItemId);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal("Ürün sepetten silindi.", okResult.Value);
        //    _mockService.Verify(s => s.Delete(cartItem), Times.Once);
        //}
    }
}