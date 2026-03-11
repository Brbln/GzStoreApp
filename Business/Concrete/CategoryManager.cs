using Business.Abstract;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
            if (string.IsNullOrEmpty(cat.Slug))
            {
                cat.Slug = SlugHalper.GenerateSlug(cat.CName);
            }

            _catDal.Add(cat);
        }

        public IResult HardDelete(int id)
        {
            var cat = _catDal.Get(u => u.Id == id);

            if (cat == null)
               return new ErrorResult("Kategori bulunamadı.");
            _catDal.Delete(cat);
            return new SuccessResult("Kategori başarıyla silindi.");
        }

        public IResult SoftDelete(int id)
        {
            var cat = _catDal.Get(u => u.Id == id);

            if (cat == null)
                return new ErrorResult("Kategori bulunamadı.");

            cat.IsDeleted = true;
            _catDal.Update(cat);
            return new SuccessResult("Kategori başarıyla silindi.");
        }

        public IResult Restore(int catId)
        {
            var cat = _catDal.GetDeletedCat(catId);
            if (cat == null)
                return new ErrorResult("Kategori bulunamadı.");

            cat.IsDeleted = false;
            _catDal.Update(cat);
            return new SuccessResult("Kategori başarıyla geri yüklendi.");
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
            return _catDal.Get(a => a.Id == id);
        }


        public void Update(Category cat)
        {  
            cat.Slug = SlugHalper.GenerateSlug(cat.CName);           
            _catDal.Update(cat);
        }

    }
}
