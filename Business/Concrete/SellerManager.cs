using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SellerManager : ISellerService
    {
        ISellerDal _sellerDal;

        public SellerManager(ISellerDal sellerDal)
        {
            _sellerDal = sellerDal;
        }

        public void Add(Seller seller)
        {
            _sellerDal.Add(seller);
        }

        public void Delete(Seller seller)
        {
            _sellerDal.Delete(seller);  
        }

        public List<Seller> GetAll()
        {
           return _sellerDal.GetAll();
        }

        public Seller GetById(int id)
        {
            return _sellerDal.Get(a=>a.SellerId ==id);
        }

        public void Update(Seller seller)
        {
            _sellerDal.Update(seller);
        }
    }
}
