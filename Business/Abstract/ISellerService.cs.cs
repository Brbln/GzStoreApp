using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISellerService
    {
        public void Add(Seller seller);
        public void Update(Seller seller);
        public void Delete(Seller seller);
        List<Seller> GetAll();
        Seller GetById(int id);
    }
}
