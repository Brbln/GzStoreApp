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
            return Get(u => u.Id == id);
        }

        public Product GetByIdWithDeleted(int id)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .IgnoreQueryFilters()
                          .FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetAllWithDeleted()
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .IgnoreQueryFilters()
                          .Include(p => p.Category)
                          .ToList();}
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
            return context.Products
                          .Where(p => p.PName.ToLower().Contains(name.ToLower()))
                          .ToList();
        }

        public List<Product> GetByStock(int minStock)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .Where(p => p.PStock >= minStock)
                          .ToList();
        }

        public List<Product> GetCatById(int id)
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .Include(p => p.Category)
                          .Where(p => p.CategoryId == id)
                          .ToList();
        }

        public bool Any(Expression<Func<Product, bool>> filter)
        {
            using var context = new GamzeDbContext();
            return context.Products
                      .IgnoreQueryFilters()
                      .Any(filter);
        }     
        public void HardDelete(Product product)
        {
            using var context = new GamzeDbContext();
            context.Products.Attach(product);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void UpdateImages(int productId, List<string> images)
        {

            //using var context = new GamzeDbContext();

            //var product = context.Products
            //                     .IgnoreQueryFilters()
            //                     .FirstOrDefault(p => p.Id == productId);

            //if (product == null)
            //    throw new Exception("Ürün bulunamadı.");

            //product.Images = images;
            //product.UpdatedDate = DateTime.Now;

            //context.Products.Update(product);
            //context.SaveChanges();
        }

        public List<Product> GetDeletedProducts()
        {
            using var context = new GamzeDbContext();
            return context.Products
                          .IgnoreQueryFilters()
                          .Where(p => p.IsDeleted)
                          .Include(p => p.Category)
                          .ToList();
        }
    }
}
