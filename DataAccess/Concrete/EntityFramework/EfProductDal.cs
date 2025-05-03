using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfRepositoryBase<Product, GamzeDbConcext>, IProductDal
    {
        public List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByProductName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByStock(int minStock)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetCatById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateImages(int productId, List<string> images)
        {
            throw new NotImplementedException();
        }
    }
}
