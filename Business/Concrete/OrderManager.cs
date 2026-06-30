using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserDal _userDal;
        private readonly IEmailService _emailService;

        public OrderManager(
            IOrderDal orderDal,
            IOrderItemDal orderItemDal,
            ICartItemDal cartItemDal,
            IProductDal productDal,
            ICartDal cartDal,
            IUserDal userDal,
            IEmailService emailService)
        {
            _orderDal = orderDal;
            _orderItemDal = orderItemDal;
            _cartItemDal = cartItemDal;
            _productDal = productDal;
            _cartDal = cartDal;
            _userDal = userDal;
            _emailService = emailService;
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
            var orders = _orderDal.GetOrdersWithItems(o => o.Id == id);
            var order = orders.FirstOrDefault();

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

        public async Task<IResult> CreateOrderFromCart(int userId)
        {
            Order order;
            decimal total;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var cart = _cartDal.Get(c => c.UserId == userId);
                if (cart == null)
                    return new ErrorResult("Sepet bulunamadı");

                var cartItems = _cartItemDal.GetAll(c => c.CartId == cart.Id);
                if (cartItems == null || cartItems.Count == 0)
                    return new ErrorResult("Sepet boş");

                order = new Order
                {
                    UserId = userId,
                    OrderTime = DateTime.Now,
                    Status = OrderStatus.Pending
                };

                _orderDal.Add(order);

                total = 0;

                foreach (var item in cartItems)
                {
                    var product = _productDal.GetByIdWithImages(item.ProductId);

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
                        Quantity = item.Quantity,
                        ImageUrl = product.Images?.FirstOrDefault()?.ImageUrl
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
            }
             
            // Mail başarısız olsa bile sipariş işlemi geri alınmaz
            try
            {
                var user = _userDal.Get(u => u.Id == userId);
                if (user != null)
                {
                    await _emailService.SendOrderConfirmationEmail(user.Email, order.Id, total);
                }
            }
            catch
            {
                // Mail gönderimi başarısız olsa da sipariş başarılı sayılır
            }

            return new SuccessResult("Sipariş başarıyla oluşturuldu");
        }

        public async Task<IResult> UpdateStatusWithEmail(int orderId, OrderStatus newStatus, string? trackingNumber = null)
        {
            var order = _orderDal.Get(o => o.Id == orderId);
            if (order == null)
                return new ErrorResult("Sipariş bulunamadı.");

            order.Status = newStatus;

            if (!string.IsNullOrWhiteSpace(trackingNumber))
            {
                order.TrackingNumber = trackingNumber;
            }

            _orderDal.Update(order);

            var emailWorthyStatuses = new[] { OrderStatus.Shipped, OrderStatus.Cancelled };

            if (emailWorthyStatuses.Contains(newStatus))
            {
                try
                {
                    var user = _userDal.Get(u => u.Id == order.UserId);
                    if (user != null)
                    {
                        await _emailService.SendOrderStatusEmail(user.Email, orderId, newStatus.ToString(), order.TrackingNumber);
                    }
                }
                catch { }
            }

            return new SuccessResult("Sipariş durumu güncellendi.");
        }
        public IDataResult<List<Order>> GetPendingPayments()
        {
            var orders = _orderDal.GetOrdersWithItems(o => o.PaymentStatus == PaymentStatus.Pending);
            var sorted = orders.OrderBy(o => o.OrderTime).ToList();
            return new SuccessDataResult<List<Order>>(sorted);
        }

        public async Task<IResult> ConfirmPayment(int orderId)
        {
            var order = _orderDal.Get(o => o.Id == orderId);
            if (order == null)
                return new ErrorResult("Sipariş bulunamadı.");

            if (order.PaymentStatus == PaymentStatus.Paid)
                return new ErrorResult("Bu ödeme zaten onaylanmış.");

            order.PaymentStatus = PaymentStatus.Paid;
             
            if (order.Status == OrderStatus.Pending)
            {
                order.Status = OrderStatus.Processing;
            }

            _orderDal.Update(order);

            try
            {
                var user = _userDal.Get(u => u.Id == order.UserId);
                if (user != null)
                {
                    await _emailService.SendPaymentConfirmedEmail(user.Email, orderId);
                }
            }
            catch { }

            return new SuccessResult("Ödeme onaylandı, sipariş hazırlanmaya başladı.");
        }

        public async Task<IResult> RejectPayment(int orderId, string? reason = null)
        {
            var order = _orderDal.Get(o => o.Id == orderId);
            if (order == null)
                return new ErrorResult("Sipariş bulunamadı.");

            order.PaymentStatus = PaymentStatus.Rejected;
            order.Status = OrderStatus.Cancelled;
            _orderDal.Update(order);

            try
            {
                var user = _userDal.Get(u => u.Id == order.UserId);
                if (user != null)
                {
                    await _emailService.SendPaymentRejectedEmail(user.Email, orderId, reason);
                }
            }
            catch { }

            return new SuccessResult("Ödeme reddedildi, sipariş iptal edildi.");
        }
    }
}