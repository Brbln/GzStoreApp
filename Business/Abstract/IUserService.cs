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
        void Add(User user);
        void Update(User user);
        void UpdateUser(UserUpdateDto dto);
        void Delete(User user);
        List<User> GetAll();
        User GetById(int id);
        User GetByEmail(string email);
        User GetByUserName(string username);
        bool IsEmailExists(string email);
        bool IsUNameExists(string username);
    }
}
