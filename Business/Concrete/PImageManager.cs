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
    public class PImageManager : IPImageService
    {
        private readonly IPImageDal _pImageDal;

        public PImageManager(IPImageDal pImageDal)
        {
            _pImageDal = pImageDal;
        }

        public void Add(PImage pImg)
        {
            if (pImg == null || string.IsNullOrEmpty(pImg.ImageUrl))
            {
                throw new ArgumentException("Resim URL'si boş olamaz ve geçerli bir resim sağlanmalıdır.");
            }

            pImg.CreatedDate = DateTime.Now; // Resmin oluşturulma tarihini belirleyin
            _pImageDal.Add(pImg); // Veri erişim katmanına ekleme işlemi gönderiliyor
        }

        public void Delete(PImage pImg)
        {
            if (pImg == null || pImg.Id <= 0)
            {
                throw new ArgumentException("Silinecek geçerli bir resim belirtiniz.");
            }

            _pImageDal.Delete(pImg); // Veri erişim katmanında silme işlemi
        }

        public List<PImage> GetAll()
        {
            return _pImageDal.GetAll(); // Veri erişim katmanından tüm resimleri getir
        }

        public List<PImage> GetByProductId(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Geçerli bir ürün ID'si giriniz.");
            }

            return _pImageDal.GetAll(p => p.ProductId == productId); // Belirli ürüne ait resimler
        }

        public void Update(PImage pImg)
        {
            if (pImg == null || pImg.Id <= 0)
            {
                throw new ArgumentException("Güncellenecek geçerli bir resim belirtiniz.");
            }

            _pImageDal.Update(pImg); // Veri erişim katmanında güncelleme işlemi
        }
    }
}
