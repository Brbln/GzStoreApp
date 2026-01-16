using AutoMapper;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Moq;
using System.Linq.Expressions;

namespace GPTest.CartItemTests
{
    public class CartItemManagerTest
    {
        [Fact]
        public void AddTest()
        {

            var mockDal = new Mock<ICartItemDal>();
            var mockMapper = new Mock<IMapper>();
            var mockProductDal = new Mock<IProductDal>();
            var mockCartDal = new Mock<ICartDal>();

            // Expression tipine uyumlu Setup
            mockProductDal.Setup(p => p.Get(It.IsAny<Expression<Func<Product, bool>>>()))
                          .Returns(new Product { Id = 1, PName = "Test", PPrice = 10 });
            mockCartDal.Setup(c => c.Get(It.IsAny<Expression<Func<Cart, bool>>>()))
                       .Returns(new Cart { Id = 1, UserId = 1 });

            var manager = new CartItemManager(mockDal.Object, mockMapper.Object, mockProductDal.Object, mockCartDal.Object);
            var item = new CartItem { CartId = 1, ProductId = 1, Quantity = 2 };

            // Act
            manager.Add(item);

            // Assert
            mockDal.Verify(d => d.Add(item), Times.Once);
        }

        [Fact]
        public void Add_CallsDalAddExactlyOnce()
        {
            // Arrange
            var mockDal = new Mock<ICartItemDal>();
            var mockMapper = new Mock<IMapper>();
            var mockProductDal = new Mock<IProductDal>();
            var mockCartDal = new Mock<ICartDal>();

            mockProductDal.Setup(p => p.Get(It.IsAny<Expression<Func<Product, bool>>>()))
                          .Returns(new Product { Id = 2, PName = "Test2", PPrice = 20 });
            mockCartDal.Setup(c => c.Get(It.IsAny<Expression<Func<Cart, bool>>>()))
                       .Returns(new Cart { Id = 1, UserId = 1 });

            var manager = new CartItemManager(mockDal.Object, mockMapper.Object, mockProductDal.Object, mockCartDal.Object);
            var item = new CartItem { CartId = 1, ProductId = 2, Quantity = 3 };

            // Act
            manager.Add(item);

            // Assert
            mockDal.Verify(d => d.Add(It.IsAny<CartItem>()), Times.Once);
        }
    }
}