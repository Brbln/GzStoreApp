using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        public void Add(Category cat);
        public void Update(Category cat);
        public void Delete(Category cat);
        List<Category> GetAll();
        Category GetById(int id);
        List<Category> GetByCategoryName(string catName);
    }
}
