using Business.Abstract;
using Business.DTOs.userDto;
using Core.Utilities.Security;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.Extensions;
using Entities.Enums;
public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public void Add(UserCreateDto dto)
    {
        var normUsname = dto.UserName.Normalize();
        var normEmail = dto.Email.Normalize();

        if (_userDal.IsUNameExists(normUsname))
            throw new Exception("Bu kullanıcı adı zaten alınmış!");

        if (_userDal.IsEmailExists(normEmail))
            throw new Exception("Bu e-posta zaten kayıtlı!");

        var user = new User
        {
            UserName = normUsname,
            Email = normEmail,
            Role =UserRoles.Customer,
            Address = dto.Address,
            PhoneNo = dto.PhoneNo,
            PasswordHash = HashHelper.Hash(dto.Password),
            Cart = new Cart()
        };

        _userDal.Add(user);
    }

    public void Delete(int id)
    {
        var user = _userDal.Get(u => u.Id == id);

        if (user == null)
            throw new Exception("Kullanıcı bulunamadı.");

        user.IsDeleted = true;
        _userDal.Update(user);
    }
    public void HardDelete(int id)
    {
        var user = _userDal.Get(u => u.Id == id);

        if (user == null)
            throw new Exception("Kullanıcı bulunamadı.");

        _userDal.Delete(user);
    }
    public List<User> GetAll()
    {
        return _userDal.GetAll();
    }

    public User GetByEmail(string email)
    {
        return _userDal.Get(u => u.Email == email.Normalize());
    }

    public User GetById(int id)
    {
        return _userDal.Get(u => u.Id == id);
    }

    public User GetByUserName(string username)
    {
        return _userDal.Get(u => u.UserName == username.Normalize());
    }

    public bool IsEmailExists(string email)
    {
        return _userDal.IsEmailExists(email);
    }

    public bool IsUNameExists(string username)
    {
        return _userDal.IsUNameExists(username);
    }     

    public void UpdateUser(UserUpdateDto dto)
    {
        var existingUser = _userDal.Get(u => u.Id == dto.UserId);
        if (existingUser == null)
            throw new Exception("Kullanıcı bulunamadı.");

        var lowUsname = dto.UserName.Normalize();
        var normEmail = dto.Email.Normalize();
        if (existingUser.UserName != lowUsname && _userDal.IsUNameExists(lowUsname))
            throw new Exception("Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor.");

        if (existingUser.Email != normEmail && _userDal.IsEmailExists(normEmail))
            throw new Exception("Bu email başka bir kullanıcı tarafından kullanılıyor.");

        existingUser.UserName = lowUsname;
        existingUser.Email = normEmail;
        existingUser.Address = dto.Address;
        existingUser.PhoneNo = dto.PhoneNo;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            existingUser.PasswordHash = HashHelper.Hash(dto.Password);
        }

        _userDal.Update(existingUser);
    }
    public List<User> GetDeletedUsers()
    {
        return _userDal.GetDeletedUsers();
    }

    public User? ValidateUser(string email, string password)
    {
        var user = _userDal.Get(u => u.Email == email.Normalize());
        if (user == null) return null;

        return VerifyPassword(password, user.PasswordHash) ? user : null;
    }
    private bool VerifyPassword(string password, string passwordHash)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashed = Convert.ToBase64String(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        return hashed == passwordHash;
    }
}
