using Business.DTOs.userDto;
using Business.DTOs.UserDTOs;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(UserCreateDto dto);
        IResult UpdateUser(UserUpdateDto dto);
        IResult Delete(int id);
        IResult HardDelete(int id);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByEmail(string email);
        IDataResult<User> GetByUserName(string username);
        IDataResult<List<User>> GetDeletedUsers();
        bool IsEmailExists(string email);
        bool IsUNameExists(string username);
        IDataResult<User>? ValidateUser(string email, string password);
        Task<IResult> ForgotPassword(ForgotPasswordDto dto);
        IResult ResetPassword(ResetPasswordDto dto);
    }
}
