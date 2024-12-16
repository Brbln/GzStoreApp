using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _catDal;

        public CategoryManager(ICategoryDal catDal)
        {
            _catDal = catDal;
        }

        public void Add(Category cat)
        {
           _catDal.Add(cat);
        }

        public void Delete(Category cat)
        {
            _catDal.Delete(cat);
        }

        public List<Category> GetAll()
        {
            return _catDal.GetAll();    
        } 

        public List<Category> GetByCategoryName(string catName)
        {
            return _catDal.GetAll(a => a.CName == catName);
        }

        public Category GetById(int id)
        {
            return _catDal.Get(a => a.CategoryId == id);
        }

        public void Update(Category cat)
        {
            _catDal.Update(cat);
        }
    }
}
