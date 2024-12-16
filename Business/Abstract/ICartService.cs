using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        public void Add(Cart cart);
        public void Update(Cart cart);
        public void Delete(Cart cart);
        public List<Cart> GetAll();
        Cart GetById(int id);
        Cart GetByUserId(int userId); 
    }
}
