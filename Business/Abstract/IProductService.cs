using Business.DTOs.ProductDTOs;
using Core.Utilities.Results;
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
        IResult Add(ProductCreateDto dto);
        IResult Update(ProductUpdateDto dto);
         
        IResult Delete(int id);           
        IResult Restore(int productId);   
        IResult HardDelete(int productId); 
        IDataResult<List<Product>> GetAllForAdmin();
        IDataResult<List<Product>> GetDeletedProducts();
        IDataResult<Product> GetByIdForAdmin(int id); 
         
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
         
        IDataResult<List<Product>> GetCatById(int id);
        IDataResult<List<Product>> GetByProductName(string name);
        IDataResult<List<Product>> GetByStock(int minStock);
        IDataResult<List<Product>> GetByPriceRange(decimal minPrice, decimal maxPrice);
         
        IResult UpdateImages(int productId, List<string> images);
    }
}
