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
            using var context = new GamzeDbContext();
            return context.Sellers.FirstOrDefault(s => s.Email == email);
        }

        public Seller getBySName(string sName)
        {
            using var context = new GamzeDbContext();
            return context.Sellers.FirstOrDefault(s => s.SellerName == sName);
        }
    }
}
