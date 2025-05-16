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
            var manager = new CartItemManager(mockDal.Object);
            var item = new CartItem { CartId = 1, ProductId = 1, Quantity = 2 };

            // Act
            manager.Add(item);

            // Assert
            mockDal.Verify(d => d.Add(item), Times.Once);
        }
    }
}