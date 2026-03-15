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
        IProductDal _productDal;

        public CategoryManager(ICategoryDal catDal, IProductDal productDal)
        {
            _catDal = catDal;
            _productDal = productDal;
        }
        public IResult Add(Category cat)
        {
            if (string.IsNullOrWhiteSpace(cat.CName))
                return new ErrorResult("Kategori adı boş olamaz.");

            var existing = _catDal.Get(c => c.CName.ToLower() == cat.CName.ToLower() && !c.IsDeleted);
            if (existing != null)
                return new ErrorResult("Aynı isimde bir kategori zaten mevcut.");

            if (string.IsNullOrEmpty(cat.Slug))
                cat.Slug = SlugHalper.GenerateSlug(cat.CName);

            _catDal.Add(cat);
            return new SuccessResult("Kategori başarıyla eklendi."); 
        }

        public IResult HardDelete(int id)
        {
            var cat = _catDal.Get(u => u.Id == id);

            if (cat == null)
                return new ErrorResult("Kategori bulunamadı.");

            var products = _productDal.GetAll(p => p.CategoryId == id);
            if (products.Any())
                return new ErrorResult("Kategori altında ürün var. Önce ürünleri silmelisiniz.");
             
            _catDal.Delete(cat);
            return new SuccessResult("Kategori başarıyla silindi.");
        }

        public IResult SoftDelete(int id)
        {
            var cat = _catDal.Get(u => u.Id == id);

            if (cat == null)
                return new ErrorResult("Kategori bulunamadı.");

            var products = _productDal.GetAll(p => p.CategoryId == id && !p.IsDeleted);
            foreach (var product in products) 
            { 
                product.IsDeleted = true;
                _productDal.Update(product); 
            }

            cat.IsDeleted = true;
            _catDal.Update(cat);
            return new SuccessResult("Kategori başarıyla silindi.");
        }

        public IResult Restore(int catId)
        {
            var cat = _catDal.GetDeletedCat(catId);
            if (cat == null)
                return new ErrorResult("Kategori bulunamadı.");
            var products = _productDal.GetAll(p => p.CategoryId == catId && p.IsDeleted);
            foreach (var product in products) 
            { 
                product.IsDeleted = false;
                _productDal.Update(product);
            }
            cat.IsDeleted = false;
            _catDal.Update(cat);
            return new SuccessResult("Kategori başarıyla geri yüklendi.");
        }
        public IDataResult<List<Category>> GetAll()
        {
            var cat = _catDal.GetAll();
            return new SuccessDataResult<List<Category>>(cat);
        }

        public IDataResult<List<Category>> GetByCategoryName(string catName)
        {
            if (string.IsNullOrWhiteSpace(catName))
                return new ErrorDataResult<List<Category>>("Geçersiz kategori adı.");
            var cat = _catDal.GetAll(a => a.CName == catName);
            return new SuccessDataResult<List<Category>>(cat);
        }

        public IDataResult<Category> GetById(int id)
        {
            if (id <= 0) 
                return new ErrorDataResult<Category>("Gecersiz kategori ID.");
            var category = _catDal.Get(a => a.Id == id);
            return new SuccessDataResult<Category>(category);
        }


        public IResult Update(Category cat)
        {
            if (cat == null)
                return new ErrorResult("Kategori bulunamadı.");
            var existing = _catDal.Get(c => c.CName.ToLower() == cat.CName.ToLower() && c.Id != cat.Id && !c.IsDeleted);
            if (existing != null) 
                return new ErrorResult("Aynı isimde başka bir kategori zaten mevcut."); 
            cat.Slug = SlugHalper.GenerateSlug(cat.CName); 
            _catDal.Update(cat);
            return new SuccessResult("Kategori başarıyla güncellendi.");
        }
         
    }
}
