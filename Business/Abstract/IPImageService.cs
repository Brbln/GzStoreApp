using Business.DTOs.ImageDTOs;
using Core.Utilities.Results;
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
        IResult Add(AddImgDto pImg);
        IResult Update(UpdImgDto pImg);
        IResult Delete(int id);
        IDataResult<PImageDto> GetById(int id);
        IDataResult<List<PImageDto>> GetByProductId(int productId);
        IDataResult<List<PImageDto>> GetAll();
    }
}
