using AutoMapper;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Moq;

namespace GPTest
{
    public class CartItemManagerTest
    {
        [Fact]
        public void AddTest()
        {
            var mockDal = new Mock<ICartItemDal>();
            var mockMapper = new Mock<IMapper>();
            var manager = new CartItemManager(mockDal.Object, mockMapper.Object);
            var item = new CartItem { CartId = 1, ProductId = 1, Quantity = 2 };

            // Act
            manager.Add(item);

            // Assert
            mockDal.Verify(d => d.Add(item), Times.Once);


        }
        [Fact]
        public void Add_CallsDalAddExactlyOnce()
        {
            var mockDal = new Mock<ICartItemDal>();
            var mockMapper = new Mock<IMapper>();
            var manager = new CartItemManager(mockDal.Object, mockMapper.Object);
            var item = new CartItem { CartId = 1, ProductId = 2, Quantity = 3 };

            manager.Add(item);

            mockDal.Verify(d => d.Add(It.IsAny<CartItem>()), Times.Once);
        }
    }
}