using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {

        // Kategorilerin tümünü getirir
        List<Category> GetAllCategories();

        // Belirli bir kategori ID'sine göre kategori getirir
        Category GetByCategoryId(int categoryId);

        // Belirli bir kategori adını içeren kategoriyi getirir
        Category GetByCategoryName(string categoryName);
    }
}
