using Business.DTOs.userDto;
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
        void Add(UserCreateDto dto); 
        void UpdateUser(UserUpdateDto dto);
        void Delete(int id);
        void HardDelete(int id);
        List<User> GetAll();
        User GetById(int id);
        User GetByEmail(string email);
        User GetByUserName(string username);
        List<User> GetDeletedUsers();
        bool IsEmailExists(string email);
        bool IsUNameExists(string username);
        User? ValidateUser(string email, string password);
    }
}
