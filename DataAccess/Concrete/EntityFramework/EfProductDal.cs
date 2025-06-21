using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfRepositoryBase<Product, GamzeDbContext>, IProductDal
    {
        public Product GetById(int id)
        {
            return Get(u => u.ProductId == id);
        }

        public List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            using var context = new GamzeDbContext();
            return context.Products
                    .Where(p => p.PPrice >= minPrice && p.PPrice <= maxPrice)
                    .ToList();            
        }

        public List<Product> GetByProductName(string name)
        {
            using var context = new GamzeDbContext();
            var pName = context.Products
                    .Where(i=> i.PName.ToLower().Contains(name.ToLower()))
                    .ToList();
                return pName;
        }

        public List<Product> GetByStock(int minStock)
        {
            using var context = new GamzeDbContext();
            var pStock = context.Products
                    .Where(i=> i.PStock >= minStock).ToList();
                return pStock;

        }

        public List<Product> GetCatById(int id)
        {
            using var context = new GamzeDbContext();
            var products = context.Products
                     .Include(i => i.Category)
                     .Where(p => p.CategoryId == id)
                     .ToList();
                return products;
               
        }

        public void UpdateImages(int productId, List<string> images)
        {
            throw new NotImplementedException();
        }
    }
}
