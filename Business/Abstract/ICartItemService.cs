using Business.DTOs.CartDTOs;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartItemService
    {
        IResult Add(CartItem cart);
        IResult Update(CartItem cart);
        IResult Delete(int id);
        public IResult ClearCart(int cartId);
        IDataResult<CartItem> GetById(int id);
        IDataResult<List<CartItem>> GetByCartId(int cartId);
        IDataResult<CartItem> GetByCartAndProduct(int cartId, int productId);
        IResult AddOrUpdate(CartItem cart);
        IDataResult<List<CartItemDto>> GetCartItemsDto(int cartId);

    }
}
