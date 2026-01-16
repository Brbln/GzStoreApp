
using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartItemManager : ICartItemService
    {
        private readonly ICartItemDal _cItemDal;
        private readonly IMapper _mapper;
        private readonly IProductDal _productDal; 
        private readonly ICartDal _cartDal;       

        public CartItemManager(ICartItemDal cItemDal, IMapper mapper, IProductDal productDal, ICartDal cartDal)
        {
            _cItemDal = cItemDal;
            _mapper = mapper;
            _productDal = productDal;
            _cartDal = cartDal;
        }         

        public void Add(CartItem cart)
        {
            if (_productDal.Get(p => p.Id == cart.ProductId) == null)
                throw new InvalidOperationException("Ürün bulunamadı.");

            if (_cartDal.Get(c => c.Id == cart.CartId) == null)
                throw new InvalidOperationException("Sepet bulunamadı.");

            if (cart.Quantity <= 0)
                throw new InvalidOperationException("Miktar 1’den küçük olamaz.");

            _cItemDal.Add(cart);
        }

        public void AddOrUpdate(CartItem cart)
        {
            if (_productDal.Get(p => p.Id == cart.ProductId) == null)
                throw new InvalidOperationException("Ürün bulunamadı.");

            if (_cartDal.Get(c => c.Id == cart.CartId) == null)
                throw new InvalidOperationException("Sepet bulunamadı.");

            if (cart.Quantity <= 0)
                throw new InvalidOperationException("Miktar 1’den küçük olamaz.");

            var existing = _cItemDal.Get(c => c.CartId == cart.CartId && c.ProductId == cart.ProductId);
            if (existing != null)
            {
                existing.Quantity += cart.Quantity;

                if (existing.Quantity <= 0)
                    _cItemDal.Delete(existing);
                else
                    _cItemDal.Update(existing);
            }
            else
            {
                _cItemDal.Add(cart);
            }
        }

        public void Delete(CartItem cart)
        {
            _cItemDal.Delete(cart);
        }

        public CartItem GetByCartAndProduct(int cartId, int productId)
        {
            return _cItemDal.Get(a => a.CartId == cartId && a.ProductId == productId);
        }

        public List<CartItem> GetByCartId(int cartId)
        {
            return _cItemDal.GetAll(a => a.CartId == cartId);
        }

        public CartItem GetById(int id)
        {
            return _cItemDal.Get(a => a.Id == id);
        }

        public void Update(CartItem cart)
        {
            var existingItem = _cItemDal.Get(c => c.Id == cart.Id);
            if (existingItem == null)
                throw new InvalidOperationException("Sepetinizde bu ürün bulunamadığı için güncellenemedi.");

            if (cart.Quantity <= 0)
            {
                _cItemDal.Delete(existingItem);
            }
            else
            {
                existingItem.Quantity = cart.Quantity;
                _cItemDal.Update(existingItem);
            }
        }

        public List<CartItemDto> GetCartItemsDto(int cartId)
        {
            var cartItems = _cItemDal.GetAll(ci => ci.CartId == cartId).ToList();

            var cartItemsDto = cartItems.Select(ci => new CartItemDto
            {
                CartItemId = ci.Id,
                Quantity = ci.Quantity,
                ProductName = ci.Product?.PName,
                UnitPrice = ci.Product?.PPrice ?? 0
            }).ToList();

            return cartItemsDto;
        }
    }
}
