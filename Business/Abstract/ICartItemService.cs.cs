using Business.DTOs;
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
        void Add(CartItem cart);
        void Update(CartItem cart);
        void Delete(CartItem cart); 
        CartItem GetById(int id);
        List<CartItem> GetByCartId(int cartId); 
        CartItem GetByCartAndProduct(int cartId, int productId);
        void AddOrUpdate(CartItem cart);
        List<CartItemDto> GetCartItemsDto(int cartId);

    }
}
