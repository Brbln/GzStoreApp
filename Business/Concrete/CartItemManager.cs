
using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Core.Utilities.Results;
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

        public IResult Add(CartItem cart)
        {
            if (_productDal.Get(p => p.Id == cart.ProductId) == null)
                return new ErrorResult("Ürün bulunamadı.");

            if (_cartDal.Get(c => c.Id == cart.CartId) == null)
                return new ErrorResult("Sepet bulunamadı.");

            if (cart.Quantity <= 0)
                return new ErrorResult("Miktar 1’den küçük olamaz.");

            _cItemDal.Add(cart);

            return new SuccessResult("Ürün sepete eklendi.");
        }

        public IResult AddOrUpdate(CartItem cart)
        {
            if (_productDal.Get(p => p.Id == cart.ProductId) == null)
                return new ErrorResult("Ürün bulunamadı.");

            if (_cartDal.Get(c => c.Id == cart.CartId) == null)
                return new ErrorResult("Sepet bulunamadı.");

            if (cart.Quantity <= 0)
                return new ErrorResult("Miktar 1’den küçük olamaz.");

            var existing = _cItemDal.Get(c =>
                c.CartId == cart.CartId &&
                c.ProductId == cart.ProductId);

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

            return new SuccessResult("Sepet güncellendi.");
        }

        public IDataResult<CartItem> GetByCartAndProduct(int cartId, int productId)
        {
            var item = _cItemDal.Get(a => a.CartId == cartId && a.ProductId == productId);

            if (item == null)
                return new ErrorDataResult<CartItem>("Sepette ürün bulunamadı.");

            return new SuccessDataResult<CartItem>(item);
        }

        public IDataResult<List<CartItem>> GetByCartId(int cartId)
        {
            var items = _cItemDal.GetAll(a => a.CartId == cartId);

            return new SuccessDataResult<List<CartItem>>(items);
        }

        public IDataResult<CartItem> GetById(int id)
        {
            var item = _cItemDal.Get(a => a.Id == id);

            if (item == null)
                return new ErrorDataResult<CartItem>("Ürün bulunamadı.");

            return new SuccessDataResult<CartItem>(item);
        }

        public IResult Update(CartItem cart)
        {
            var existingItem = _cItemDal.Get(c => c.Id == cart.Id);

            if (existingItem == null)
                return new ErrorResult("Sepette ürün bulunamadı.");

            if (cart.Quantity <= 0)
            {
                _cItemDal.Delete(existingItem);
                return new SuccessResult("Ürün sepetten kaldırıldı.");
            }

            existingItem.Quantity = cart.Quantity;
            _cItemDal.Update(existingItem);

            return new SuccessResult("Ürün miktarı güncellendi.");
        }

        public IDataResult<List<CartItemDto>> GetCartItemsDto(int cartId)
        {
            var cartItems = _cItemDal.GetAllWithProduct(ci => ci.CartId == cartId);

            var dto = _mapper.Map<List<CartItemDto>>(cartItems);

            return new SuccessDataResult<List<CartItemDto>>(dto);
        }

        public IResult Delete(int id)
        {
            var cartItem = _cItemDal.Get(c => c.Id == id);

            if (cartItem == null)
                return new ErrorResult("Silinecek ürün bulunamadı.");

            _cItemDal.Delete(cartItem);

            return new SuccessResult("Ürün sepetten silindi.");
        }
        public IResult ClearCart(int cartId)
        {
            var items = _cItemDal.GetByCartId(cartId);

            foreach (var item in items)
            {
                _cItemDal.Delete(item);
            }

            return new SuccessResult("Sepet temizlendi.");
        }
    }
}
