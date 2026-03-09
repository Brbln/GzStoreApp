using AutoMapper;
using Business.Abstract;
using Business.DTOs.ProductDTOs;
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
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }
        public IResult Add(ProductCreateDto dto)
        {
            bool exists = _productDal.Any(p =>
                p.PName == dto.PName &&
                p.CategoryId == dto.CategoryId &&
                !p.IsDeleted);

            if (exists)
                return new ErrorResult("Bu ürün bu kategori altında zaten mevcut.");

            var product = _mapper.Map<Product>(dto);
            product.CreatedDate = DateTime.Now;

            _productDal.Add(product);

            return new SuccessResult("Ürün başarıyla eklendi.");
        }
         
        public IResult Update(ProductUpdateDto dto)
        {
            var product = _productDal.GetById(dto.ProductId);

            if (product == null)
                return new ErrorResult("Ürün bulunamadı.");

            if (product.IsDeleted)
                return new ErrorResult("Silinmiş ürün güncellenemez.");

            _mapper.Map(dto, product);
            product.UpdatedDate = DateTime.Now;

            _productDal.Update(product);

            return new SuccessResult("Ürün güncellendi.");
        }
         
        public IResult Delete(int id)
        {
            var product = _productDal.GetById(id);

            if (product == null)
                return new ErrorResult("Ürün bulunamadı.");

            if (product.IsDeleted)
                return new ErrorResult("Ürün zaten silinmiş.");

            product.IsDeleted = true;
            product.UpdatedDate = DateTime.Now;

            _productDal.Update(product);

            return new SuccessResult("Ürün silindi (soft delete).");
        }
         
        public IResult Restore(int productId)
        {
            var product = _productDal.GetByIdWithDeleted(productId);

            if (product == null)
                return new ErrorResult("Ürün bulunamadı.");

            if (!product.IsDeleted)
                return new ErrorResult("Ürün zaten aktif.");

            product.IsDeleted = false;
            product.UpdatedDate = DateTime.Now;

            _productDal.Update(product);

            return new SuccessResult("Ürün geri yüklendi.");
        }
         
        public IResult HardDelete(int productId)
        {
            var product = _productDal.GetByIdWithDeleted(productId);

            if (product == null)
                return new ErrorResult("Ürün bulunamadı.");

            _productDal.HardDelete(product);

            return new SuccessResult("Ürün kalıcı olarak silindi.");
        }
         
        public IDataResult<List<Product>> GetAll()
        {
            var products = _productDal.GetAll();
            return new SuccessDataResult<List<Product>>(products);
        }
         
        public IDataResult<List<Product>> GetAllForAdmin()
        {
            var products = _productDal.GetAllWithDeleted();
            return new SuccessDataResult<List<Product>>(products);
        }
         
        public IDataResult<Product> GetById(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<Product>("Geçersiz ürün ID.");

            var product = _productDal.Get(p => p.Id == id && !p.IsDeleted);

            if (product == null)
                return new ErrorDataResult<Product>("Ürün bulunamadı.");

            return new SuccessDataResult<Product>(product);
        }
         
        public IDataResult<Product> GetByIdForAdmin(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<Product>("Geçersiz ürün ID.");

            var product = _productDal.GetByIdWithDeleted(id);

            if (product == null)
                return new ErrorDataResult<Product>("Ürün bulunamadı.");

            return new SuccessDataResult<Product>(product);
        }
         
        public IDataResult<List<Product>> GetCatById(int id)
        {
            var products = _productDal.GetAll(p => p.CategoryId == id && !p.IsDeleted);
            return new SuccessDataResult<List<Product>>(products);
        }
         
        public IDataResult<List<Product>> GetByProductName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new ErrorDataResult<List<Product>>("Ürün adı boş olamaz.");

            var products = _productDal.GetAll(p =>
                p.PName.ToLower().Contains(name.ToLower()) && !p.IsDeleted);

            return new SuccessDataResult<List<Product>>(products);
        } 
        public IDataResult<List<Product>> GetByStock(int minStock)
        {
            if (minStock < 0)
                return new ErrorDataResult<List<Product>>("Stok negatif olamaz.");

            var products = _productDal.GetAll(p =>
                p.PStock >= minStock && !p.IsDeleted);

            return new SuccessDataResult<List<Product>>(products);
        } 
        public IDataResult<List<Product>> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
                return new ErrorDataResult<List<Product>>("Fiyatlar negatif olamaz.");

            if (minPrice > maxPrice)
                return new ErrorDataResult<List<Product>>("Minimum fiyat maksimumdan büyük olamaz.");

            var products = _productDal.GetAll(p =>
                p.PPrice >= minPrice &&
                p.PPrice <= maxPrice &&
                !p.IsDeleted);

            return new SuccessDataResult<List<Product>>(products);
        }
         
        public IResult UpdateImages(int productId, List<string> images)
        {
            return new ErrorResult("Henüz implemente edilmedi.");
        }
    }
}

