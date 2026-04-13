using AutoMapper;
using Business.Abstract;
using Business.DTOs.ImageDTOs;
using Core.Utilities.Results;
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
        private readonly IMapper _mapper;
        private readonly IProductDal _productDal;

        public PImageManager(IPImageDal pImageDal,IMapper mapper, IProductDal productDal)
        {
            _pImageDal = pImageDal;
            _mapper =mapper;
            _productDal = productDal;
        }

        public IResult Add(AddImgDto dto)
        {
            var product = _productDal.Get(p => p.Id == dto.ProductId);
            if (product == null)
                return new ErrorResult("Böyle bir ürün bulunamadı.");
            var imgCount=_pImageDal.GetAll(x=>x.ProductId==dto.ProductId).Count;
            if (imgCount >= 5)
                return new ErrorResult("En fazla 5 resim eklenebilir.");

            var entity = _mapper.Map<PImage>(dto);

            entity.CreatedDate = DateTime.Now;

            _pImageDal.Add(entity);

            return new SuccessResult("Resim eklendi.");
        }

        public IResult Update(UpdImgDto dto)
        {
            var image = _pImageDal.Get(x => x.Id == dto.Id);

            if (image == null)
                return new ErrorResult("Resim bulunamadı.");

            image.ImageUrl = dto.ImageUrl;

            _pImageDal.Update(image);

            return new SuccessResult("Resim güncellendi.");
        }

        public IResult Delete(int id)
        {
            var image = _pImageDal.Get(x => x.Id == id);

            if (image == null)
                return new ErrorResult("Resim bulunamadı.");

            _pImageDal.Delete(image);

            return new SuccessResult("Resim silindi.");
        }

        public IDataResult<PImageDto> GetById(int id)
        {
            var image = _pImageDal.Get(x => x.Id == id);

            if (image == null)
                return new ErrorDataResult<PImageDto>("Resim bulunamadı.");

            var dto = _mapper.Map<PImageDto>(image);

            return new SuccessDataResult<PImageDto>(dto);
        }

        public IDataResult<List<PImageDto>> GetByProductId(int productId)
        {
            var images = _pImageDal.GetAll(x => x.ProductId == productId);

            var dto = _mapper.Map<List<PImageDto>>(images);

            return new SuccessDataResult<List<PImageDto>>(dto);
        }

        public IDataResult<List<PImageDto>> GetAll()
        {
            var images = _pImageDal.GetAll();

            var dto = _mapper.Map<List<PImageDto>>(images);

            return new SuccessDataResult<List<PImageDto>>(dto);
        }
    }
}
