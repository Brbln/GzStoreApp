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
        void Add(Category cat);
        public void Update(Category cat);
        IResult SoftDelete(int id);
        IResult HardDelete(int id); 
        IResult Restore(int id);
        List<Category> GetAll();
        Category GetById(int id);
        List<Category> GetByCategoryName(string catName);
    }
}
