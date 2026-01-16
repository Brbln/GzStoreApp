using Business.Abstract;
using Business.DTOs.userDto;
using Core.Utilities.Security;
using DataAccess.Abstract;
using Entities.Concrete;

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
            throw new Exception("Bu kullanıcı adı zaten alınmış!");

        if (_userDal.IsEmailExists(user.Email))
            throw new Exception("Bu e-posta zaten kayıtlı!");

        user.PasswordHash = HashHelper.Hash(user.PasswordHash);
        user.Cart = new Cart();

        _userDal.Add(user);
    }

    public void Delete(User user)
    {
        user.IsDeleted = true;
        _userDal.Update(user);
    }

    public List<User> GetAll()
    {
        return _userDal.GetAll();
    }

    public User GetByEmail(string email)
    {
        return _userDal.Get(u => u.Email == email);
    }

    public User GetById(int id)
    {
        return _userDal.Get(u => u.Id == id);
    }

    public User GetByUserName(string username)
    {
        return _userDal.Get(u => u.UserName == username);
    }

    public bool IsEmailExists(string email)
    {
        return _userDal.IsEmailExists(email);
    }

    public bool IsUNameExists(string username)
    {
        return _userDal.IsUNameExists(username);
    }

    public void Update(User user)
    {
        _userDal.Update(user);
    }

    public void UpdateUser(UserUpdateDto dto)
    {
        var existingUser = _userDal.Get(u => u.Id == dto.UserId);
        if (existingUser == null)
            throw new Exception("Kullanıcı bulunamadı.");

        if (existingUser.UserName != dto.UserName && _userDal.IsUNameExists(dto.UserName))
            throw new Exception("Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor.");

        if (existingUser.Email != dto.Email && _userDal.IsEmailExists(dto.Email))
            throw new Exception("Bu email başka bir kullanıcı tarafından kullanılıyor.");

        existingUser.UserName = dto.UserName;
        existingUser.Email = dto.Email;
        existingUser.Address = dto.Address;
        existingUser.PhoneNo = dto.PhoneNo;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            existingUser.PasswordHash = HashHelper.Hash(dto.Password);
        }

        _userDal.Update(existingUser);
    }
}
