using Core.Utilities.Results;
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
<<<<<<< HEAD
        void Add(Category cat);
        public void Update(Category cat);
        IResult SoftDelete(int id);
        IResult HardDelete(int id); 
        IResult Restore(int id);
        List<Category> GetAll();
        Category GetById(int id);
        List<Category> GetByCategoryName(string catName);
=======
        IResult Add(Category cat);
        IResult  Update(Category cat);
        IResult SoftDelete(int id);
        IResult HardDelete(int id); 
        IResult Restore(int id);
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int id);
        IDataResult<List<Category>> GetByCategoryName(string catName);
>>>>>>> Category ve fonksiyonlar güncellendi, soft/hard delete ve duplicate kontrol eklendi.
    }
}
