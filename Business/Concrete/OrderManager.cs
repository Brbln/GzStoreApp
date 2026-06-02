using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IOrderItemDal _orderItemDal;
        private readonly ICartItemDal _cartItemDal;
        private readonly IProductDal _productDal;
        private readonly ICartDal _cartDal;

        public OrderManager(
            IOrderDal orderDal,
            IOrderItemDal orderItemDal,
            ICartItemDal cartItemDal,
            IProductDal productDal,
            ICartDal cartDal)
        {
            _orderDal = orderDal;
            _orderItemDal = orderItemDal;
            _cartItemDal = cartItemDal;
            _productDal = productDal;
            _cartDal = cartDal;
        }
        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult("Order oluşturuldu.");
        }
        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult("Order güncellendi.");
        }
        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult("Order silindi.");
        }
        public IDataResult<Order> GetById(int id)
        {
            var order = _orderDal.Get(o => o.Id == id);

            if (order == null)
                return new ErrorDataResult<Order>("Order bulunamadı.");

            return new SuccessDataResult<Order>(order);
        }
        public IDataResult<List<Order>> GetAll()
        {
            var orders = _orderDal.GetOrdersWithItems();
            return new SuccessDataResult<List<Order>>(orders);
        }
        public IDataResult<List<Order>> GetByUserId(int userId)
        {
            var orders = _orderDal.GetOrdersWithItems(o => o.UserId == userId);
            return new SuccessDataResult<List<Order>>(orders);
        }
        public IResult CreateOrderFromCart(int userId)
        {
            using (var scope = new TransactionScope())
            {
                var cart = _cartDal.Get(c => c.UserId == userId);

                if (cart == null)
                    return new ErrorResult("Sepet bulunamadı");

                var cartItems = _cartItemDal.GetAll(c => c.CartId == cart.Id);

                if (cartItems == null || cartItems.Count == 0)
                    return new ErrorResult("Sepet boş");

                var order = new Order
                {
                    UserId = userId,
                    OrderTime = DateTime.Now,
                    Status = OrderStatus.Pending
                };

                _orderDal.Add(order);

                decimal total = 0;

                foreach (var item in cartItems)
                {
                    var product = _productDal.Get(p => p.Id == item.ProductId);

                    if (product == null)
                        return new ErrorResult($"Ürün bulunamadı (ProductId: {item.ProductId})");

                    if (product.PStock < item.Quantity)
                        return new ErrorResult($"Yeterli stok yok: {product.PName}");

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        ProductName = product.PName,
                        UnitPrice = product.PPrice,
                        Quantity = item.Quantity
                    };

                    _orderItemDal.Add(orderItem);
                    product.PStock -= item.Quantity;
                    _productDal.Update(product);
                    total += orderItem.UnitPrice * orderItem.Quantity;
                }

                order.SetTotal(total);
                _orderDal.Update(order);

                foreach (var item in cartItems)
                {
                    _cartItemDal.Delete(item);
                }
                scope.Complete();
                return new SuccessResult("Sipariş başarıyla oluşturuldu");
            }
        }
    }
}
