using Business.DTOs.ProductDTOs;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        void Add(ProductCreateDto dto);
        void Update(ProductUpdateDto dto);
        public void Delete(int id);
        List<Product> GetAll();
        Product GetById(int id);
        List<Product> GetCatById(int id);
        List<Product> GetByProductName(string name);
        List<Product> GetByStock(int minStock);
        List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice);
        void UpdateImages(int productId, List<string> images); 
    }
}
