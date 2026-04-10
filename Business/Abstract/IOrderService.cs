using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult Add(Order order);
        IResult Update(Order order);
        IResult Delete(Order order);
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int id);
        IDataResult<List<Order>> GetByUserId(int userId);
        IResult CreateOrderFromCart(int userId);

    }
}
