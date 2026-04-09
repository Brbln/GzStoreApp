using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public IResult Add(Cart cart)
        {
            _cartDal.Add(cart);
            return new SuccessResult("Sepet başarıyla oluşturuldu");
        }

        public IResult Delete(Cart cart)
        {
            _cartDal.Delete(cart);
            return new SuccessResult("Sepet silindi.");
        }

        public IDataResult<List<Cart>> GetAll()
        {
            var carts = _cartDal.GetAll();
            return new SuccessDataResult<List<Cart>>(carts);
        }

        public IDataResult<Cart> GetById(int id)
        {
            var cart = _cartDal.Get(a => a.Id == id);

            if (cart == null)
                return new ErrorDataResult<Cart>("Sepet bulunamadı.");

            return new SuccessDataResult<Cart>(cart);
        }

        public IDataResult<Cart> GetByUserId(int userId)
        {
            var cart = _cartDal.Get(c => c.UserId == userId);

            if (cart == null)
                return new ErrorDataResult<Cart>("Sepet bulunamadı.");

            return new SuccessDataResult<Cart>(cart);
        }

        public IResult Update(Cart cart)
        {
            _cartDal.Update(cart);
            return new SuccessResult("Sepet güncellendi.");
        }
    }
}

