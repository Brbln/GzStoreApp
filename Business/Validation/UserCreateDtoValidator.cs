using Business.DTOs.userDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(u => u.UserName)
               .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
               .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalı.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz email.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermeli.") 
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermeli.");
        }
    }
}
