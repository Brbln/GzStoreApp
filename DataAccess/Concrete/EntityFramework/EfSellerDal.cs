using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSellerDal : EfRepositoryBase<Seller, GamzeDbContext>, ISellerDal
    {
        public Seller getByEmail(string email)
        {
            return Get(s => s.Email == email);
        }

        public Seller getBySName(string sName)
        {
            return Get(s => s.SellerName == sName);
        }

        public Seller GetById(int id)
        {
            return Get(u => u.SellerId == id);
        }
    }
}
