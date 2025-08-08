
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

        public CartItemManager(ICartItemDal cItemDal, IMapper mapper)
        {
            _cItemDal = cItemDal;
            _mapper = mapper;
        }

        public void Add(CartItem cart)
        {
            _cItemDal.Add(cart);
        }

        public void AddOrUpdate(CartItem cart)
        {
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
                if (cart.Quantity > 0)
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
            return _cItemDal.Get(a => a.CartItemId == id);

        }

        public void Update(CartItem cart)
        {
            var existingItem = _cItemDal.Get(c => c.CartItemId == cart.CartItemId);
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
            var cartItemsDto = _mapper.Map<List<CartItemDto>>(cartItems);
            return cartItemsDto;
        }

    }
}
