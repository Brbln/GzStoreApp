using Business.Abstract;
using Business.DTOs.userDto;
using Business.DTOs.UserDTOs;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.Security;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly ICartDal _cartDal;
    private readonly IEmailService _emailService;

    public UserManager(IUserDal userDal, ICartDal cartDal, IEmailService emailService)
    {
        _userDal = userDal;
        _cartDal = cartDal;
        _emailService = emailService;
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
            PasswordHash = HashHelper.Hash(dto.Password)
        };
        _userDal.Add(user);

        var cart = new Cart
        {
            UserId = user.Id,
            CreatedDate = DateTime.Now
        };
        _cartDal.Add(cart);
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
         
        var userWithSameName = _userDal.Get(u => u.UserName.ToLower() == lowUsname);
        if (userWithSameName != null && userWithSameName.Id != dto.UserId)
            return new ErrorResult("Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor.");

        var userWithSameEmail = _userDal.Get(u => u.Email.ToLower() == normEmail);
        if (userWithSameEmail != null && userWithSameEmail.Id != dto.UserId)
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
        using var sha256 = SHA256.Create();
        var hashed = Convert.ToBase64String(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        return hashed == passwordHash;
    }
    public async Task<IResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var user = _userDal.Get(u => u.Email == dto.Email.Trim().ToLowerInvariant());
         
        if (user == null)
            return new SuccessResult("Eğer bu email kayıtlıysa, sıfırlama kodu gönderildi.");

        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        user.PasswordResetCode = code;
        user.PasswordResetExpiry = DateTime.Now.AddMinutes(15);
        _userDal.Update(user);

        await _emailService.SendPasswordResetEmail(user.Email, code);

        return new SuccessResult("Eğer bu email kayıtlıysa, sıfırlama kodu gönderildi.");
    }

    public IResult ResetPassword(ResetPasswordDto dto)
    {
        var user = _userDal.Get(u => u.Email == dto.Email.Trim().ToLowerInvariant());

        if (user == null || user.PasswordResetCode != dto.Code)
            return new ErrorResult("Geçersiz kod veya e-posta.");

        if (user.PasswordResetExpiry == null || user.PasswordResetExpiry < DateTime.Now)
            return new ErrorResult("Kodun süresi dolmuş. Lütfen yeni kod isteyin.");

        user.PasswordHash = HashHelper.Hash(dto.NewPassword);
        user.PasswordResetCode = null;
        user.PasswordResetExpiry = null;
        _userDal.Update(user);

        return new SuccessResult("Şifreniz başarıyla sıfırlandı.");
    }
}
