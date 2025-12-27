using Business.Abstract;
using Business.DTOs.userDto;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public void Add(User user)
        {
            if (_userDal.IsUNameExists(user.UserName))
                throw new Exception("Bu kullanıcı adı zaten alınmış!"); // Conflict
            if (_userDal.IsEmailExists(user.Email))
                throw new Exception("Bu e-posta zaten kayıtlı!"); // Conflict

            _userDal.Add(user);
        }
        public void UpdateUser(UserUpdateDto dto)
        {
            var existingUser = _userDal.Get(u => u.UserId == dto.UserId);
            if (existingUser == null)
                throw new Exception("Kullanıcı bulunamadı.");

            if (existingUser.UserName != dto.UserName && _userDal.IsUNameExists(dto.UserName))
                throw new Exception("Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor.");

            if (existingUser.Email != dto.Email && _userDal.IsEmailExists(dto.Email))
                throw new Exception("Bu email başka bir kullanıcı tarafından kullanılıyor.");

            existingUser.UserName = dto.UserName;
            existingUser.Email = dto.Email;
            existingUser.Password = dto.Password;
            existingUser.Address = dto.Address;
            existingUser.PhoneNo = dto.PhoneNo;

            _userDal.Update(existingUser);
        }
        public void Update(User user) => _userDal.Update(user);

        public void Delete(User user) => _userDal.Delete(user);

        public List<User> GetAll() => _userDal.GetAll();

        public User GetByEmail(string email) => _userDal.Get(u => u.Email == email);

        public User GetById(int id) => _userDal.Get(u => u.UserId == id);

        public User GetByUserName(string username) => _userDal.Get(u => u.UserName == username);

        public bool IsEmailExists(string email) => _userDal.IsEmailExists(email);

        public bool IsUNameExists(string username) => _userDal.IsUNameExists(username);

    }
}
