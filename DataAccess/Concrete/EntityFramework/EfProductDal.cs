using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfRepositoryBase<Product, GamzeDbContext>, IProductDal
    {
        public Product GetById(int id)
        {
            return Get(u => u.Id == id && !u.IsDeleted);
        }

        public List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .Where(p => !p.IsDeleted && p.PPrice >= minPrice && p.PPrice <= maxPrice)
                          .ToList();
        }

        public List<Product> GetByProductName(string name)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .Where(p => !p.IsDeleted && p.PName.ToLower().Contains(name.ToLower()))
                          .ToList();
        }

        public List<Product> GetByStock(int minStock)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .Where(p => !p.IsDeleted && p.PStock >= minStock)
                          .ToList();
        }

        public List<Product> GetCatById(int id)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .Include(p => p.Category)
                          .Where(p => !p.IsDeleted && p.CategoryId == id)
                          .ToList();
        }

        public bool Any(Expression<Func<Product, bool>> filter)
        {
            using var context = new GamzeDbContext(); 
            return context.Products.Any(p => !p.IsDeleted && filter.Compile()(p));
        }

        public void UpdateImages(int productId, List<string> images)
        {
            throw new NotImplementedException();
        }

        public Product GetByIdWithDeleted(int id)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .FirstOrDefault(p => p.Id == id);
        }
        public List<Product> GetAllWithDeleted()
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .Include(p => p.Category)
                          .ToList();
        }
        public void HardDelete(Product product)
        {
            using var context = new GamzeDbContext();
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
