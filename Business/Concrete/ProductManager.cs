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
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Geçersiz ürün ID'si.", nameof(id));
            return _productDal.Get(a => a.ProductId == id);
        }

        public List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
                throw new ArgumentException("Fiyatlar negatif olamaz.");

            if (minPrice > maxPrice)
                throw new ArgumentException("Minimum fiyat, maksimum fiyattan büyük olamaz.", nameof(minPrice));

            var products = _productDal.GetAll(a => a.PPrice >= minPrice && a.PPrice <= maxPrice);

            return products ?? new List<Product>();
        }

        public List<Product> GetByProductName(string name)
        {
            return _productDal.GetAll(a => a.PName == name);
        }

        public List<Product> GetByStock(int minStock)
        {
            if (minStock == 0)
                return _productDal.GetAll(a => a.PStock == 0) ?? new List<Product>();

            if (minStock < 5)
                Console.WriteLine("Uyarı: Stoklar tükenmek üzere.");

            return _productDal.GetAll(a => a.PStock >= minStock) ?? new List<Product>();

        }

        public List<Product> GetCatById(int id)
        {
            return _productDal.GetAll(a=>a.CategoryId== id);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void UpdateImages(int productId, List<string> images)
        {
            throw new NotImplementedException();
        }
    }
}
