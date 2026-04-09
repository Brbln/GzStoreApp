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
    public class EfCategoryDal : EfRepositoryBase<Category>, ICategoryDal
    {
        public EfCategoryDal(GamzeDbContext context) : base(context)
        {
        }

        public Category GetByCategoryName(string categoryName)
        {
            return _context.Categories.FirstOrDefault(c=> c.CName==categoryName);            
        }
        public Category GetDeletedCat(int id)
        {  
            return _context.Categories
                          .IgnoreQueryFilters()
                          .FirstOrDefault(p => p.Id == id && p.IsDeleted);  
        }
        public List<Category> GetAllWithDeleted()
        { 
            return _context.Categories
                          .IgnoreQueryFilters()
                          .Include(c => c.Products) 
                          .ToList();
        }
    }
}
