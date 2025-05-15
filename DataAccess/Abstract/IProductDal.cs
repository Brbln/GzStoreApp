using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        Product GetById(int id);
        List<Product> GetCatById(int id);
        List<Product> GetByProductName(string name);
        List<Product> GetByStock(int minStock);
        List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice);
        void UpdateImages(int productId, List<string> images);
    }
}
