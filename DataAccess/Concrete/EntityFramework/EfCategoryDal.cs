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
    public class EfCategoryDal : EfRepositoryBase<Category, GamzeDbContext>, ICategoryDal
    {
        
        public Category GetByCategoryName(string categoryName)
        {
            using var context = new GamzeDbContext();
            return context.Categories.FirstOrDefault(c=> c.CName==categoryName);            
        }
        public Category GetDeletedCat(int id)
        { 
            using var context = new GamzeDbContext();
            return context.Categories
                          .IgnoreQueryFilters()
                          .FirstOrDefault(p => p.Id == id && p.IsDeleted);  
        }
        public List<Category> GetAllWithDeleted()
        {
            using var context = new GamzeDbContext();
            return context.Categories
                          .IgnoreQueryFilters()
                          .Include(c => c.Products) 
                          .ToList();
        }
    }
}
