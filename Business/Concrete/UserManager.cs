using Business.Abstract;
using Business.DTOs.userDto;
using Core.Utilities.Security;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.Extensions;
using Entities.Enums;
using Core.Utilities.Results;
public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IResult Add(UserCreateDto dto)
    {
        var normUsname = dto.UserName.Trim().ToLowerInvariant();
        var normEmail = dto.Email.Trim().ToLowerInvariant();

        if (_userDal.IsUNameExists(normUsname))
            return new ErrorResult("Bu kullanıcı adı zaten alınmış!");

        if (_userDal.IsEmailExists(normEmail))
            return new ErrorResult("Bu e-posta zaten kayıtlı!");

        var user = new User
        {
            UserName = normUsname,
            Email = normEmail,
            Role = UserRoles.Customer,
            Address = dto.Address,
            PhoneNo = dto.PhoneNo,
            PasswordHash = HashHelper.Hash(dto.Password),
            Cart = new Cart()
        };

        _userDal.Add(user);
        return new SuccessResult("Kullanıcı başarıyla eklendi.");
    }

    public IResult Delete(int id)
    {
        var user = _userDal.Get(u => u.Id == id);
        if (user == null)
            return new ErrorResult("Kullanıcı bulunamadı.");

        user.IsDeleted = true;
        _userDal.Update(user);
        return new SuccessResult("Kullanıcı soft delete ile silindi.");
    }
    public IResult HardDelete(int id)
    {
        var user = _userDal.Get(u => u.Id == id);

        if (user == null)
            return new ErrorResult("Kullanıcı bulunamadı.");

        _userDal.Delete(user);
        return new SuccessResult("Kullanıcı kalıcı olarak silindi.");
    }
    public IDataResult<List<User>> GetAll()
    {
        var users = _userDal.GetAll();
        return new SuccessDataResult<List<User>>(users);
    }

    public IDataResult<User> GetByEmail(string email)
    {
        var user = _userDal.Get(u => u.Email == email.Trim().ToLowerInvariant());
        return user != null
            ? new SuccessDataResult<User>(user)
            : new ErrorDataResult<User>("Kullanıcı bulunamadı.");
    }

    public IDataResult<User> GetById(int id)
    {
        var user = _userDal.Get(u => u.Id == id);
        return user != null
            ? new SuccessDataResult<User>(user)
            : new ErrorDataResult<User>("Kullanıcı bulunamadı.");

    }

    public IDataResult<User> GetByUserName(string username)
    {
        var user = _userDal.Get(u => u.UserName == username.Trim().ToLowerInvariant());
        return user != null
            ? new SuccessDataResult<User>(user)
            : new ErrorDataResult<User>("Kullanıcı bulunamadı.");
    }

    public bool IsEmailExists(string email) => _userDal.IsEmailExists(email.Trim().ToLowerInvariant());

    public bool IsUNameExists(string username) => _userDal.IsUNameExists(username.Trim().ToLowerInvariant());

    public IResult UpdateUser(UserUpdateDto dto)
    {
        var existingUser = _userDal.Get(u => u.Id == dto.UserId);
        if (existingUser == null)
            return new ErrorResult("Kullanıcı bulunamadı.");

        var lowUsname = dto.UserName.Trim().ToLowerInvariant();
        var normEmail = dto.Email.Trim().ToLowerInvariant();
        if (existingUser.UserName != lowUsname && _userDal.IsUNameExists(lowUsname))
            return new ErrorResult("Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor.");

        if (existingUser.Email != normEmail && _userDal.IsEmailExists(normEmail))
            return new ErrorResult("Bu email başka bir kullanıcı tarafından kullanılıyor.");

        existingUser.UserName = lowUsname;
        existingUser.Email = normEmail;
        existingUser.Address = dto.Address;
        existingUser.PhoneNo = dto.PhoneNo;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            existingUser.PasswordHash = HashHelper.Hash(dto.Password);
        }

        _userDal.Update(existingUser);
        return new SuccessResult("Kullanıcı başarıyla güncellendi.");
    }
    public IDataResult<List<User>> GetDeletedUsers()
    {
        var users = _userDal.GetDeletedUsers();
        return new SuccessDataResult<List<User>>(users);
    }

    public IDataResult<User>? ValidateUser(string email, string password)
    {
        var user = _userDal.Get(u => u.Email == email.Trim().ToLowerInvariant());
        if (user == null)
            return new ErrorDataResult<User>("Geçersiz e-posta veya şifre.");

        return VerifyPassword(password, user.PasswordHash)
            ? new SuccessDataResult<User>(user)
            : new ErrorDataResult<User>("Geçersiz e-posta veya şifre.");
    }
    private bool VerifyPassword(string password, string passwordHash)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashed = Convert.ToBase64String(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        return hashed == passwordHash;
    }
}
