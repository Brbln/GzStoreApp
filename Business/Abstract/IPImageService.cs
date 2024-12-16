using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPImageService
    {
        void Add(PImage pImg);
        void Update(PImage pImg);
        void Delete(PImage pImg);
        List<PImage> GetByProductId(int productId);
        List<PImage> GetAll();
    }
}
