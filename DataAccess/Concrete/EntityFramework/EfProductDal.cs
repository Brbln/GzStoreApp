using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfRepositoryBase<Product, GamzeDbContext>, IProductDal
    {
        public List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByProductName(string name)
        {
            using (var context = new GamzeDbContext())
            {
                var pName = context.Products
                    .Where(i=> i.PName.ToLower().Contains(name.ToLower()))
                    .ToList();
                return pName;
            } 
        }

        public List<Product> GetByStock(int minStock)
        {
            using (var context = new GamzeDbContext())
            {
                var pStock = context.Products
                    .Where(i=> i.PStock >= minStock).ToList();
                return pStock;

            }
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
